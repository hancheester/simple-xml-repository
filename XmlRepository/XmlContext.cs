using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XmlRepository
{
    public class XmlContext
    {
        private readonly string _xmlFileLocation;

        public XmlContext(Settings settings)
        {
            _xmlFileLocation = settings.DataFileLocation;
        }

        public void SaveChanges<T>(IList<T> entities)
        {
            if (entities == null) return;

            string filePath = Path.Combine(_xmlFileLocation, $"{typeof(T).Name}.xml");
            using (var writer = new StreamWriter(filePath))            
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                serializer.Serialize(writer, entities);
                writer.Flush();
            }
        }

        public IList<T> Set<T>()
        {
            string filePath = Path.Combine(_xmlFileLocation, $"{typeof(T).Name}.xml");
            IList<T> entities = new List<T>();

            if (File.Exists(filePath))
            {
                using (var stream = File.OpenRead(filePath))
                {
                    var serializer = new XmlSerializer(typeof(List<T>));
                    entities = serializer.Deserialize(stream) as List<T>;
                }
            }

            return entities;
        }
    }
}
