﻿using System;
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
            { LogType.Debug,    ConsoleColor.DarkMagenta },
            { LogType.Framework,   ConsoleColor.Green },
            { LogType.Error,    ConsoleColor.Red },
            { LogType.Status,   ConsoleColor.Blue },
            { LogType.Database, ConsoleColor.Magenta },
            { LogType.Map,      ConsoleColor.Cyan },
            { LogType.Packet,   ConsoleColor.Cyan },
            { LogType.Warning,  ConsoleColor.Yellow },
            { LogType.Script,   ConsoleColor.Cyan }
        };

        public static void Print(LogType _type, object _obj)
        {
            Console.ForegroundColor = TypeColour[_type];

            Console.Write("[" + _type.ToString() + "] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(_obj.ToString());
        }

        public static void Print(string _subject, object _obj, ConsoleColor _colour)
        {
            Console.Write("[" + _subject + "] ");
            Console.ForegroundColor = _colour;
            Console.WriteLine(_obj.ToString());
        }

        public static void Print(object obj)
        {
            Console.WriteLine("[Framework] " + obj.ToString());
        }

    }
}
