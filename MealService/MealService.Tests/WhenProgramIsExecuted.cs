using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    [Ignore("Ignored until implementation is complete")]
    public class WhenProgramIsExecuted
    {
        /// <summary>
        /// Even a second to run this thing is too generous.
        /// </summary>
        private const int MillisecondTimeout = 1000;

        private static string RunProgramAndReturnOutput(string input)
        {
            var mealService = new Process { StartInfo = new ProcessStartInfo("MealService.exe", input) };
            var output = mealService.StandardOutput;

            mealService.Start();

            Assert.That(mealService.WaitForExit(MillisecondTimeout), Is.True);
            return output.ReadToEnd();
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
        public void DinnerIsReturnedWithExtraSpuds()
        {
            var meal = RunProgramAndReturnOutput("night, 1, 2, 2, 4");
            Assert.That(meal, Is.EqualTo("steak, potato(x2), cake"));
        }

        [Test]
        public void DinnerIsReturnedWithErrorDueToUnknownDish()
        {
            var meal = RunProgramAndReturnOutput("night, 1, 2, 3, 5");
            Assert.That(meal, Is.EqualTo("steak, potato, wine, error"));
        }

        [Test]
        public void DinnerIsReturnedWithErrorDueToTooMuchSteak()
        {
            var meal = RunProgramAndReturnOutput("night, 1, 1, 2, 3, 5");
            Assert.That(meal, Is.EqualTo("steak, error"));
        }
    }
}
