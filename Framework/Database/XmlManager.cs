using Framework.Contants.Character;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Framework.Database
{
    public class XmlManager
    {
        public static Xml.race GetRaceStats(RaceID value)
        {
            Console.WriteLine(value);
            Xml.race raceStats = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Xml.race));
            StreamReader reader = new StreamReader($"../../stats/race_{value}.xml");
            raceStats = (Xml.race)serializer.Deserialize(reader);
            reader.Close();

            return raceStats;
        }

        public static Xml.classe GetClassStats(ClassID value)
        {
            Console.WriteLine(value);
            Xml.classe classeStats = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Xml.classe));
            StreamReader reader = new StreamReader($"../../stats/class_{value}.xml");
            classeStats = (Xml.classe)serializer.Deserialize(reader);
            reader.Close();

            return classeStats;
        }
    }
}
