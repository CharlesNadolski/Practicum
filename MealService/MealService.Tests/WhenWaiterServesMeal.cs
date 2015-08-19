using Moq;
using NUnit.Framework;
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
        private IDictionary<int, IDish> _dishes;
        private Mock<IDish> _mockDish1;
        private Mock<IDish> _mockDish2;
        private Mock<IDish> _mockDish3;
        private Mock<IDictionary<string, string>> _mockNameAtMealTime1;
        private Mock<IDictionary<string, string>> _mockNameAtMealTime2;
        private Mock<IDictionary<string, string>> _mockNameAtMealTime3;

        [SetUp]
        public void SetUp()
        {
            _mockReferenceData = CreateMock<IReferenceData>();
            _mockReferenceData.Setup(references => references.Load("ReferenceData.xml"));
            _mockOrderFactory = CreateMock<IOrderFactory>();
            //Must be a real dictionary because mocks cannot be used for out parameters.
            _dishes = new Dictionary<int, IDish>();
            _mockReferenceData.Setup(references => references.Dishes).Returns(_dishes);
            _mockDish1 = CreateMock<IDish>();
            _mockDish2 = CreateMock<IDish>();
            _mockDish3 = CreateMock<IDish>();
            _mockNameAtMealTime1 = CreateMock<IDictionary<string, string>>();
            _mockNameAtMealTime2 = CreateMock<IDictionary<string, string>>();
            _mockNameAtMealTime3 = CreateMock<IDictionary<string, string>>();
            _mockDish1.SetupGet(dish => dish.NameAtMealTime).Returns(_mockNameAtMealTime1.Object);
            _mockDish2.SetupGet(dish => dish.NameAtMealTime).Returns(_mockNameAtMealTime2.Object);
            _mockDish3.SetupGet(dish => dish.NameAtMealTime).Returns(_mockNameAtMealTime3.Object);
            _dishes[1] = _mockDish1.Object;
            _dishes[2] = _mockDish2.Object;
            _dishes[3] = _mockDish3.Object;

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

            string eggs = "eggs";
            _mockNameAtMealTime1.Setup(dic => dic.TryGetValue("morning", out eggs)).Returns(true);
            string toast = "toast";
            _mockNameAtMealTime2.Setup(dic => dic.TryGetValue("morning", out toast)).Returns(true);
            string coffee = "coffee";
            _mockNameAtMealTime3.Setup(dic => dic.TryGetValue("morning", out coffee)).Returns(true);


            var actual = _waiter.Serve(inputOrder);

            Assert.That(actual, Is.EqualTo(expectation));
        }

        [Test]
        public void InvalidMealTimeForDishReturnsError()
        {
            const string inputOrder = "morning, 1, 2, 3, 4";
            const string expectation = "eggs, toast, coffee, error";

            var mockOrder = CreateMock<IOrder>();
            _mockOrderFactory.Setup(factory => factory.Parse(inputOrder)).Returns(mockOrder.Object);
            mockOrder.SetupGet(order => order.TimeOfDay).Returns("morning");
            mockOrder.SetupGet(order => order.DishTypes).Returns(new[] { 1, 2, 3, 4 });

            string eggs = "eggs";
            _mockNameAtMealTime1.Setup(dic => dic.TryGetValue("morning", out eggs)).Returns(true);
            string toast = "toast";
            _mockNameAtMealTime2.Setup(dic => dic.TryGetValue("morning", out toast)).Returns(true);
            string coffee = "coffee";
            _mockNameAtMealTime3.Setup(dic => dic.TryGetValue("morning", out coffee)).Returns(true);
            var mockDish4 = CreateMock<IDish>();
            var mockNameAtMealTime4 = CreateMock<IDictionary<string, string>>();
            mockDish4.SetupGet(dish => dish.NameAtMealTime).Returns(mockNameAtMealTime4.Object);
            string notApplicable = null;
            mockNameAtMealTime4.Setup(dic => dic.TryGetValue("morning", out notApplicable)).Returns(false);
            _dishes[4] = mockDish4.Object;

            var actual = _waiter.Serve(inputOrder);

            Assert.That(actual, Is.EqualTo(expectation));
        }


        [Test]
        public void InvalidDishTypeReturnsError()
        {
            const string inputOrder = "night, 1, 2, 3, 5";
            const string expectation = "steak, potato, wine, error";

            var mockOrder = CreateMock<IOrder>();
            _mockOrderFactory.Setup(factory => factory.Parse(inputOrder)).Returns(mockOrder.Object);
            mockOrder.SetupGet(order => order.TimeOfDay).Returns("night");
            mockOrder.SetupGet(order => order.DishTypes).Returns(new[] { 1, 2, 3, 5 });

            string steak = "steak";
            _mockNameAtMealTime1.Setup(dic => dic.TryGetValue("night", out steak)).Returns(true);
            string potato = "potato";
            _mockNameAtMealTime2.Setup(dic => dic.TryGetValue("night", out potato)).Returns(true);
            string wine = "wine";
            _mockNameAtMealTime3.Setup(dic => dic.TryGetValue("night", out wine)).Returns(true);

            var actual = _waiter.Serve(inputOrder);

            Assert.That(actual, Is.EqualTo(expectation));
        }
    }
}
