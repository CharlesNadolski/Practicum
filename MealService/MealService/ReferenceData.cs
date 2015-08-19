using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace MealService
{
    internal class ReferenceData : IReferenceData
    {
        /// <summary>
        /// The text fields of DishDto represent the times of day that we have meals
        /// </summary>
        private static readonly PropertyInfo[] TimeOfDayProperties =
            typeof(DishDto).GetProperties().Where(property => property.PropertyType == typeof(string)).ToArray();

        private Dictionary<int, IDictionary<string, string>> _dishes = new Dictionary<int, IDictionary<string, string>>();

        public void Load(string xmlPath)
        {
            using (var xmlReader = XmlReader.Create(xmlPath))
            {
                var serializer = new XmlSerializer(typeof(DishDto[]));
                var dishDtos = (DishDto[])serializer.Deserialize(xmlReader);
                foreach(var dishDto in dishDtos)
                {
                    IDictionary<string, string> dishEntry;
                    if (!_dishes.TryGetValue(dishDto.DishType, out dishEntry))
                    {
                        dishEntry = new Dictionary<string, string>();
                        _dishes.Add(dishDto.DishType, dishEntry);
                    }

                    foreach (var propertyInfo in TimeOfDayProperties)
                    {
                        var dishTimeValue = (string)propertyInfo.GetValue(dishDto);
                        if (!string.IsNullOrWhiteSpace(dishTimeValue))
                        {
                            dishEntry.Add(propertyInfo.Name, dishTimeValue);
                        }
                    }
                }
            }
        }

        public IDictionary<int, IDictionary<string, string>> Dishes
        {
            get { return _dishes; }
        }
    }
}
