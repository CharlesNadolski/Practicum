using NUnit.Framework;

namespace MealService.Tests
{
    [TestFixture]
    [Category("UnitTest")]
    public class WhenOrderIsCreated : MoqTestBase
    {
        private IOrderFactory _orderFactory;

        [SetUp]
        public void SetUp()
        {
            _orderFactory = new OrderFactory();
        }

        [Test]
        public void InputStringIsParseIntoOrder()
        {
            const string input = "morning, 1, 2, 3";

            var actual = _orderFactory.Parse(input);

            Assert.That(actual.TimeOfDay, Is.EqualTo("morning"));
            Assert.That(actual.DishTypes, Is.EqualTo(new[] { 1, 2, 3 }));
        }

        [Test]
        public void InputStringIsParseIntoOrderWhileSortingDishType()
        {
            const string input = "morning, 2, 1, 3";

            var actual = _orderFactory.Parse(input);

            Assert.That(actual.TimeOfDay, Is.EqualTo("morning"));
            Assert.That(actual.DishTypes, Is.EqualTo(new[] { 1, 2, 3 }));
        }
    }
}
