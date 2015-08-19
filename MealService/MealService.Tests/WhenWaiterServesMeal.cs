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
        private IDictionary<int, IDictionary<string, string>> _dishes;
        private Mock<IDictionary<string, string>> _mockDish1;
        private Mock<IDictionary<string, string>> _mockDish2;
        private Mock<IDictionary<string, string>> _mockDish3;

        [SetUp]
        public void SetUp()
        {
            _mockReferenceData = CreateMock<IReferenceData>();
            _mockReferenceData.Setup(references => references.Load("ReferenceData.xml"));
            _mockOrderFactory = CreateMock<IOrderFactory>();
            //Must be a real dictionary because mocks cannot be used for out parameters.
            _dishes = new Dictionary<int, IDictionary<string, string>>();
            _mockReferenceData.Setup(references => references.Dishes).Returns(_dishes);
            _mockDish1 = CreateMock<IDictionary<string, string>>();
            _mockDish2 = CreateMock<IDictionary<string, string>>();
            _mockDish3 = CreateMock<IDictionary<string, string>>();
    
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
            _mockDish1.Setup(dish => dish.TryGetValue("morning", out eggs)).Returns(true);
            string toast = "toast";
            _mockDish2.Setup(dish => dish.TryGetValue("morning", out toast)).Returns(true);
            string coffee = "coffee";
            _mockDish3.Setup(dish => dish.TryGetValue("morning", out coffee)).Returns(true);

            _dishes[1] = _mockDish1.Object;
            _dishes[2] = _mockDish2.Object;
            _dishes[3] = _mockDish3.Object;

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
            _mockDish1.Setup(dish => dish.TryGetValue("morning", out eggs)).Returns(true);
            string toast = "toast";
            _mockDish2.Setup(dish => dish.TryGetValue("morning", out toast)).Returns(true);
            string coffee = "coffee";
            _mockDish3.Setup(dish => dish.TryGetValue("morning", out coffee)).Returns(true);
            var mockDish4 = CreateMock<IDictionary<string, string>>();
            string notApplicable = null;
            mockDish4.Setup(dish => dish.TryGetValue("morning", out notApplicable)).Returns(false);
            _dishes[1] = _mockDish1.Object;
            _dishes[2] = _mockDish2.Object;
            _dishes[3] = _mockDish3.Object;
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
            _mockDish1.Setup(dish => dish.TryGetValue("night", out steak)).Returns(true);
            string potato = "potato";
            _mockDish2.Setup(dish => dish.TryGetValue("night", out potato)).Returns(true);
            string wine = "wine";
            _mockDish3.Setup(dish => dish.TryGetValue("night", out wine)).Returns(true);
            _dishes[1] = _mockDish1.Object;
            _dishes[2] = _mockDish2.Object;
            _dishes[3] = _mockDish3.Object;

            var actual = _waiter.Serve(inputOrder);

            Assert.That(actual, Is.EqualTo(expectation));
        }
    }
}
