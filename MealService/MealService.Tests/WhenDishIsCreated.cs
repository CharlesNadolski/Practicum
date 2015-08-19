using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealService.Tests
{
    [TestFixture]
    [Category("UnitTest")]
    public class WhenDishIsCreated : MoqTestBase
    {
        private DishFactory _dishFactory;

        [SetUp]
        public void SetUp()
        {
            _dishFactory = new DishFactory();
        }

        [Test]
        public void ItIsTransformedFromADtoWithOnlyMorningDefined()
        {
            const string morningDishName = "duck confit";
            //Need to create a real DTO as reflection does not play nicely with MOQ due to dynamic castle proxy.
            var dishDto = new DishDto { morning = new DishDescripionDto { AllowMultiple = true, Name = morningDishName } };

            var actual = _dishFactory.Transform(dishDto);

            Assert.That(actual.NameAtMealTime["morning"], Is.EqualTo(morningDishName));
            Assert.That(actual.MultipleAtMealTime["morning"], Is.True);
            Assert.That(actual.NameAtMealTime, Is.Not.Contains("night"));
            Assert.That(actual.MultipleAtMealTime, Is.Not.Contains("night"));
        }

        [Test]
        public void ItIsTransformedFromADto()
        {
            const string morningDishName = "duck confit";
            const string nightDishName = "squirrel stew";
            //Need to create a real DTO as reflection does not play nicely with MOQ due to dynamic castle proxy.
            var dishDto = new DishDto
            {
                morning = new DishDescripionDto { AllowMultiple = true, Name = morningDishName },
                night = new DishDescripionDto { AllowMultiple = true, Name = nightDishName }
            };

            var actual = _dishFactory.Transform(dishDto);

            Assert.That(actual.NameAtMealTime["morning"], Is.EqualTo(morningDishName));
            Assert.That(actual.MultipleAtMealTime["morning"], Is.True);
            Assert.That(actual.NameAtMealTime["night"], Is.EqualTo(nightDishName));
            Assert.That(actual.MultipleAtMealTime["night"], Is.True);
        }

    }
}
