using Moq;
using NUnit.Framework;

namespace MealService.Tests
{
    /// <summary>
    /// Helper base class for unit tests which help eliminate some boiler plate.
    /// It allows for mocks to be automatically verified, to avoid the need for extra mocking and extra verifies.
    /// </summary>
    public abstract class MoqTestBase
    {
        private MockRepository _mockRepository;

        [SetUp]
        public void SetUpMockRepository()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
        }

        [TearDown]
        public void VerifyAllMocks()
        {
            _mockRepository.VerifyAll();
        }

        protected Mock<T> CreateMock<T>() where T : class
        {
            return _mockRepository.Create<T>();
        }
    }
}
