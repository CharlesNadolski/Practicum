using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealService;

namespace MealService.Tests
{
    [TestFixture]
    [Category("UnitTest")]
    public class WhenWaiterServesMeal
    {
        private IWaiter _waiter;

        [SetUp]
        public void SetUp()
        {
            _waiter = new Waiter();
        }

        [Test]
        public void OrderIsTakenAndMealIsReturned()
        {
            const string order = "morning, 1, 2, 3";
            const string expectation = "eggs, toast, coffee";

            var actual = _waiter.Serve(order);

            Assert.That(actual, Is.EqualTo(expectation));
        }
    }
}
