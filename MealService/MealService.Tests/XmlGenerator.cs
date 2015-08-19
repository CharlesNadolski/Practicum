using NUnit.Framework;
using System;
using System.IO;
using System.Xml.Serialization;

namespace MealService.Tests
{
    /// <summary>
    /// This isn't really a test per se, but a convenient way to regenerate the dish xml data for testing,
    /// while also keeping the xml generation logic separate from core logic.
    /// </summary>
    [TestFixture]
    [Category("XmlGenerator")]
    public class XmlGenerator
    {
        [Test]
        public void GenerateDishData()
        {
            var dishes = new DishDto[]
            {
                new DishDto
                {
                    DishType = 1,
                    morning = new DishDescripionDto { Name = "eggs", AllowMultiple = false },
                    night =  new DishDescripionDto { Name = "steak", AllowMultiple = false }
                },
                new DishDto
                {
                    DishType = 2,
                    morning = new DishDescripionDto { Name = "toast", AllowMultiple = false },
                    night = new DishDescripionDto { Name = "potato", AllowMultiple = true }
                },
                new DishDto
                {
                    DishType = 3,
                    morning = new DishDescripionDto { Name = "coffee", AllowMultiple = true },
                    night = new DishDescripionDto { Name = "wine", AllowMultiple = false }
                },
                new DishDto
                {
                    DishType = 4,
                    morning = null,
                    night = new DishDescripionDto { Name = "cake", AllowMultiple = false }
                }
            };

            var stringWriter = new StringWriter();
            var serializer = new XmlSerializer(typeof(DishDto[]));
            serializer.Serialize(stringWriter, dishes);
            Console.Write(stringWriter.ToString());
        }
    }
}
