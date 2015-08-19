using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
                new DishDto { DishType = 1, MorningDish = "eggs", EveningDish = "steak" },
                new DishDto { DishType = 2, MorningDish = "Toast", EveningDish = "potato" },
                new DishDto { DishType = 3, MorningDish = "coffee", EveningDish = "wine" },
                new DishDto { DishType = 4, MorningDish = null, EveningDish = "cake" }
            };

            var stringWriter = new StringWriter();
            var serializer = new XmlSerializer(typeof(DishDto[]));
            serializer.Serialize(stringWriter, dishes);
            Console.Write(stringWriter.ToString());
        }
    }
}
