using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace MealService
{
    internal class ReferenceData : IReferenceData
    {
        private Dictionary<int, IDish> _dishes = new Dictionary<int, IDish>();
        private IDishFactory _dishFactory;

        public ReferenceData(IDishFactory dishFactory)
        {
            _dishFactory = dishFactory;
        }

        public void Load(string xmlPath)
        {
            using (var xmlReader = XmlReader.Create(xmlPath))
            {
                var serializer = new XmlSerializer(typeof(DishDto[]));
                var dishDtos = (DishDto[])serializer.Deserialize(xmlReader);
                _dishes = dishDtos.ToDictionary(dto => dto.DishType, _dishFactory.Transform);
            }
        }

        public IDictionary<int, IDish> Dishes
        {
            get { return _dishes; }
        }
    }
}
