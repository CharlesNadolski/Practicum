using Moq;
using NUnit.Framework;
using System;

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
            const string order = "morning, 1, 2, 3";
            const string expectation = "eggs, toast, coffee";

            var mockOrder = CreateMock<IOrder>();
            _mockOrderFactory.Setup(factory => factory.Parse(order)).Returns(mockOrder.Object);

            //We don't have a full implementation yet!
            //var actual = _waiter.Serve(order);
            //Assert.That(actual, Is.EqualTo(expectation));

            Assert.That(() => _waiter.Serve(order), Throws.InstanceOf<NotImplementedException>());
        }
    }
}
