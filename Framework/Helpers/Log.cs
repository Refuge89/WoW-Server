using System;
using System.Collections.Generic;

namespace Framework.Helpers
{
    public enum LogType
    {
        Debug,
        Framework,
        Packet,
        Error,
        Warning,
        Database,
        Status,
        Map,
        Script
    }

    public class Log
    {               
        public static readonly Dictionary<LogType, ConsoleColor> TypeColour = new Dictionary<LogType, ConsoleColor>()
        {
            { LogType.Debug,     ConsoleColor.DarkMagenta },
            { LogType.Framework, ConsoleColor.Green },
            { LogType.Error,     ConsoleColor.Red },
            { LogType.Status,    ConsoleColor.Blue },
            { LogType.Database,  ConsoleColor.Magenta },
            { LogType.Map,       ConsoleColor.Cyan },
            { LogType.Packet,    ConsoleColor.Cyan },
            { LogType.Warning,   ConsoleColor.Yellow },
            { LogType.Script,    ConsoleColor.Cyan },
        };

        public static void Print(LogType type, object obj)
        {
            Console.ForegroundColor = TypeColour[type];
            Console.Write($"{DateTime.Now:hh:mm:ss} [{type}] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(obj.ToString());
        }

        public static void Print(string subject, object obj, ConsoleColor colour)
        {
            Console.Write($"{DateTime.Now:hh:mm:ss} [{subject}] ");
            Console.ForegroundColor = colour;
            Console.WriteLine(obj.ToString());
        }

        public static void Print(object obj)
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss} [Framework] {obj}");
        }
    }
}
