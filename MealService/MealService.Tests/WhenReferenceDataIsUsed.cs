using NUnit.Framework;
using System.IO;
using System.Linq;

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
    <MorningDish>eggs</MorningDish>
    <EveningDish>steak</EveningDish>
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

            var dish = _referenceData.Dishes.Single();

            Assert.That(dish.DishType, Is.EqualTo(1));
            Assert.That(dish.MorningDish, Is.EqualTo("eggs"));
            Assert.That(dish.EveningDish, Is.EqualTo("steak"));
        }
    }
}
