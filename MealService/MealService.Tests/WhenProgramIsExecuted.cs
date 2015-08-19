using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealService.Tests
{
    /// <summary>
    /// Verifies from end-to-end that acceptance criteria are met.
    /// </summary>
    [TestFixture]
    [Category("IntegrationTest")]
    public class WhenProgramIsExecuted
    {
        /// <summary>
        /// Even a second to run this thing is too generous.
        /// </summary>
        private const int MillisecondTimeout = 1000;

        private static string RunProgramAndReturnOutput(string input)
        {
            //Emulate user input at the console by redirecting console input and output
            var startInfo = new ProcessStartInfo("MealService.exe")
            {
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            var mealService = new Process { StartInfo = startInfo };

            mealService.Start();

            mealService.StandardInput.WriteLine(input);
            //Terminate the console as the user would
            mealService.StandardInput.WriteLine("exit");
            var output = mealService.StandardOutput.ReadToEnd();

            //Sanity check
            Assert.That(mealService.WaitForExit(MillisecondTimeout), Is.True);

            //Remove the instruction and echo output
            var lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return lines[2];
        }

        [Test]
        public void BreakfastIsReturned()
        {
            var meal = RunProgramAndReturnOutput("morning, 1, 2, 3");
            Assert.That(meal, Is.EqualTo("eggs, toast, coffee"));
        }

        [Test]
        public void SameBreakfastIsReturnedEvenIfOutOfOrder()
        {
            var meal = RunProgramAndReturnOutput("morning, 2, 1, 3");
            Assert.That(meal, Is.EqualTo("eggs, toast, coffee"));
        }

        [Test]
        public void BreakfastIsReturnedWithErrorDueToUnknownDish()
        {
            var meal = RunProgramAndReturnOutput("morning, 1, 2, 3, 4");
            Assert.That(meal, Is.EqualTo("eggs, toast, coffee, error"));
        }

        [Test]
        [Ignore("Ignored until implementation is complete")]
        public void BreakfastIsReturnedWithExtraCoffee()
        {
            var meal = RunProgramAndReturnOutput("morning, 1, 2, 3, 3, 3");
            Assert.That(meal, Is.EqualTo("eggs, toast, coffee(x3)"));
        }

        [Test]
        public void DinnerIsReturned()
        {
            var meal = RunProgramAndReturnOutput("night, 1, 2, 3, 4");
            Assert.That(meal, Is.EqualTo("steak, potato, wine, cake"));
        }

        [Test]
        [Ignore("Ignored until implementation is complete")]
        public void DinnerIsReturnedWithExtraSpuds()
        {
            var meal = RunProgramAndReturnOutput("night, 1, 2, 2, 4");
            Assert.That(meal, Is.EqualTo("steak, potato(x2), cake"));
        }

        [Test]
        [Ignore("Ignored until implementation is complete")]
        public void DinnerIsReturnedWithErrorDueToUnknownDish()
        {
            var meal = RunProgramAndReturnOutput("night, 1, 2, 3, 5");
            Assert.That(meal, Is.EqualTo("steak, potato, wine, error"));
        }

        [Test]
        [Ignore("Ignored until implementation is complete")]
        public void DinnerIsReturnedWithErrorDueToTooMuchSteak()
        {
            var meal = RunProgramAndReturnOutput("night, 1, 1, 2, 3, 5");
            Assert.That(meal, Is.EqualTo("steak, error"));
        }
    }
}
