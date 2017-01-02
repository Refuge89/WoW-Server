using Framework.Contants.Character;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Framework.Database
{
    public class XmlManager
    {
        public static Xml.race getRaceStats(RaceID value)
        {
            Console.WriteLine(value);
            Xml.race RaceStats = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Xml.race));
            StreamReader reader = new StreamReader($"../../stats/race_{value}.xml");
            RaceStats = (Xml.race)serializer.Deserialize(reader);
            reader.Close();

            return RaceStats;
        }

        public static Xml.classe getClassStats(ClassID value)
        {
            Console.WriteLine(value);
            Xml.classe ClasseStats = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Xml.classe));
            StreamReader reader = new StreamReader($"../../stats/class_{value}.xml");
            ClasseStats = (Xml.classe)serializer.Deserialize(reader);
            reader.Close();

            return ClasseStats;
        }
    }
}
