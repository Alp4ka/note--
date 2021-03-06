﻿using System.Windows.Forms;

namespace note__
{
    public static class Utils
    {
        public static string[] blueWords = {
            "bool", "byte", "sbyte", "short", "ushort", "int", "uint", "long", "ulong", "double", "float", "decimal",
            "string", "char", "void", "object", "typeof", "sizeof", "null", "true", "false",
            "lock", "public", "private ", "internal ", "var", " get", " set",
            "protected", "static", "readonly", "sealed", "const", "fixed", "stackalloc", "volatile", "new", "override", "abstract", "virtual",
            "event", "extern", "ref", "out", " is ", " as ", "params", "arglist", "makeref", "reftype", "refvalue", "this", "base",
            "namespace", "using", "checked", "unchecked", "unsafe", "implicit", "explicit"
        };
        public static string[] magentaWords = {
            "try", "for", " in ", "case", "while", "if", "else", "foreach", " do", " switch", " throw ",
            "catch", "finally", "goto", "break;" , "default", "continue", "return"
        };
        public static string[] greenWords = { "class", "struct", "interface", "enum", "delegate", "operator", "-", "+", "=", "*", "&", "%", "/", "$", "|", "^" };
        public static readonly int[] delays = { 30000, 60000, 300000 };
        /// <summary>
        /// Ограничитель
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ReturnEdged(int l, int r, int value)
        {
            if (value < l)
            {
                return l;
            }
            if (value > r)
            {
                return r;
            }
            return value;

        }
        /// <summary>
        /// Button.Name -> int value.
        /// </summary>
        /// <param name="item"> Нажатая кнопка. </param>
        /// <returns> Целое число - задержка в миллисекундах. </returns>
        public static int GetMsByName(ToolStripMenuItem item)
        {
            switch (item.Name)
            {
                case "thirtySecButton":
                    return 30000;
                case "minuteButton":
                    return 60000;
                case "fiveMinuteButton":
                    return 60000 * 5;
                default:
                    return 30000;
            }
        }
    }
}
