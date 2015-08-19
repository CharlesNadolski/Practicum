using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace MealService
{
    internal class ReferenceData : IReferenceData
    {
        private DishDto[] _dishes;

        public void Load(string xmlPath)
        {
            using (var xmlReader = XmlReader.Create(xmlPath))
            {
                var serializer = new XmlSerializer(typeof(DishDto[]));
                _dishes = (DishDto[])serializer.Deserialize(xmlReader);
            }
        }

        public IReadOnlyCollection<IDishDto> Dishes
        {
            get { return _dishes; }
        }
    }
}
