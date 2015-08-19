using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MealService.Tests
{
    [TestFixture]
    [Category("UnitTest")]
    public class WhenWaiterServesMeal : MoqTestBase
    {
        private IWaiter _waiter;
        private Mock<IReferenceData> _mockReferenceData;
        private Mock<IOrderFactory> _mockOrderFactory;

        [SetUp]
        public void SetUp()
        {
            _mockReferenceData = CreateMock<IReferenceData>();
            _mockReferenceData.Setup(references => references.Load("ReferenceData.xml"));
            _mockOrderFactory = CreateMock<IOrderFactory>();
            
            _waiter = new Waiter(_mockReferenceData.Object, _mockOrderFactory.Object);
        }

        [Test]
        public void OrderIsTakenAndMealIsReturned()
        {
            const string inputOrder = "morning, 1, 2, 3";
            const string expectation = "eggs, toast, coffee";

            var mockOrder = CreateMock<IOrder>();
            _mockOrderFactory.Setup(factory => factory.Parse(inputOrder)).Returns(mockOrder.Object);
            mockOrder.SetupGet(order => order.TimeOfDay).Returns("morning");
            mockOrder.SetupGet(order => order.DishTypes).Returns(new[] { 1, 2, 3 });

            var mockDishes = CreateMock<IDictionary<int, IDictionary<string, string>>>();
            _mockReferenceData.Setup(references => references.Dishes).Returns(mockDishes.Object);

            var mockDish1 = CreateMock<IDictionary<string, string>>();
            string eggs = "eggs";
            mockDish1.Setup(dish => dish.TryGetValue("morning", out eggs)).Returns(true);
            var mockDish2 = CreateMock<IDictionary<string, string>>();
            string toast = "toast";
            mockDish2.Setup(dish => dish.TryGetValue("morning", out toast)).Returns(true);
            var mockDish3 = CreateMock<IDictionary<string, string>>();
            string coffee = "coffee";
            mockDish3.Setup(dish => dish.TryGetValue("morning", out coffee)).Returns(true);
            mockDishes.Setup(dishes => dishes[1]).Returns(mockDish1.Object);
            mockDishes.Setup(dishes => dishes[2]).Returns(mockDish2.Object);
            mockDishes.Setup(dishes => dishes[3]).Returns(mockDish3.Object);

            var actual = _waiter.Serve(inputOrder);

            Assert.That(actual, Is.EqualTo(expectation));
        }
    }
}
