using NUnit.Framework;
using System;

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

            //We don't have an implementation of even an abstraction yet!
            //var actual = _waiter.Serve(order);
            //Assert.That(actual, Is.EqualTo(expectation));

            Assert.That(() => _waiter.Serve(order), Throws.InstanceOf<NotImplementedException>());
        }
    }
}
