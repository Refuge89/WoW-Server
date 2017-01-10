using Framework.Contants.Character;
using System.IO;
using System.Xml.Serialization;
using Framework.Database.Xml;
using Framework.Database.XML;

namespace Framework.Database
{
    public class XmlManager
    {
        public static race GetRaceStats(RaceID value)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(race));
            StreamReader reader = new StreamReader($"../../stats/race_{value}.xml");
            var raceStats = (race)serializer.Deserialize(reader);
            reader.Close();

            return raceStats;
        }

        public static classe GetClassStats(ClassID value)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(classe));
            StreamReader reader = new StreamReader($"../../stats/class_{value}.xml");
            var classeStats = (classe)serializer.Deserialize(reader);
            reader.Close();

            return classeStats;
        }

        public static itemsItem GetItem(uint value)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(items));
            StreamReader reader = new StreamReader($"../../stats/items.xml");
            var retorno = (items)serializer.Deserialize(reader);
            reader.Close();

            foreach (itemsItem itemId in retorno.item)
            {
                if (itemId.id == value)
                    return itemId;
            }

            return null;
        }
    }
}
