using NUnit.Framework;
using System.Collections.Generic;
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
    <morning>eggs</morning>
    <night>steak</night>
  </DishDto>
</ArrayOfDishDto>";
        private string _tempPath;

        [SetUp]
        public void SetUp()
        {
            _referenceData = new ReferenceData();
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
            _referenceData.Load(_tempPath);

            Assert.That(_referenceData.Dishes[1]["morning"], Is.EqualTo("eggs"));
            Assert.That(_referenceData.Dishes[1]["night"], Is.EqualTo("steak"));
            Assert.That(_referenceData.Dishes[1], Is.Not.Contains("SomeRandomTime"));
        }
    }
}
