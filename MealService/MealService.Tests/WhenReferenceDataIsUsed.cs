using Moq;
using NUnit.Framework;
using System.IO;

namespace MealService.Tests
{
    [TestFixture]
    [Category("UnitTest")]
    public class WhenReferenceDataIsLoaded : MoqTestBase
    {
        private IReferenceData _referenceData;
        private const string DishData = @"<ArrayOfDishDto>
  <DishDto>
    <DishType>1</DishType>
    <morning>
      <Name>eggs</Name>
      <AllowMultiple>true</AllowMultiple>
    </morning>
    <night>
      <Name>steak</Name>
      <AllowMultiple>false</AllowMultiple>
    </night>
  </DishDto>
</ArrayOfDishDto>";
        private string _tempPath;
        private Mock<IDishFactory> _dishFactory;

        [SetUp]
        public void SetUp()
        {
            _dishFactory = CreateMock<IDishFactory>();
            _referenceData = new ReferenceData(_dishFactory.Object);
            _tempPath = Path.GetTempFileName();
            File.WriteAllText(_tempPath, DishData);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(_tempPath);
        }

        [Test]
        public void XmlCanBeReadThroughDishInterface()
        {
            var mockDish1 = CreateMock<IDish>();
            _dishFactory.Setup(factory => factory.Transform(
                It.Is<IDishDto>(dto => dto.DishType == 1 && dto.morning.Name == "eggs" && dto.morning.AllowMultiple &&
                                                            dto.night.Name == "steak" && !dto.night.AllowMultiple)))
                .Returns(mockDish1.Object);
            
            _referenceData.Load(_tempPath);

            Assert.That(_referenceData.Dishes[1], Is.EqualTo(mockDish1.Object));
        }
    }
}
