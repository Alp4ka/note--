using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace note__
{
    static class Program
    {
        public const string cachedPath = "./cached.dat";
        public const string settingsPath = "./settings.dat";
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Designer.CScheme scheme = ParseScheme(settingsPath);
            int delay = ParseDelay(settingsPath);
            string[] files = ParseFiles(cachedPath);
            var form = new Form1(files, scheme, delay);
            Application.Run(form);

        }
        /// <summary>
        /// Парс файлов.
        /// </summary>
        /// <param name="path"> Путь до файла с данными. </param>
        /// <returns> Массив строк - пути до файлов. </returns>
        private static string[] ParseFiles(string path)
        {
            try
            {
                List<string> result = new List<string>(File.ReadAllLines(path));
                for (int i = result.Count - 1; i >= 0; --i)
                {
                    if (!File.Exists(result[i]))
                    {
                        result.RemoveAt(i);
                    }
                }
                if (result.Count == 0)
                {
                    return null;
                }
                return result.ToArray();
            }
            catch
            {
                return null;
            }


        }

        /// <summary>
        /// Парс задержки автосейва.
        /// </summary>
        /// <param name="path"> Путь до файла с настрйоками. </param>
        /// <returns> Время задержки автосейва. </returns>
        private static int ParseDelay(string path)
        {
            int delay;
            if (!File.Exists(path))
            {
                return Utils.delays[0];
            }
            string[] text = File.ReadAllLines(path);
            if (text.Length == 0)
            {
                return Utils.delays[0];
            }
            try
            {
                if (int.TryParse(text[1], out delay))
                {
                    if (Utils.delays.Contains(delay))
                    {
                        return delay;
                    }
                    else
                    {
                        return Utils.delays[0];
                    }
                }
                else
                {
                    return Utils.delays[0];
                }
            }
            catch
            {
                return Utils.delays[0];
            }
        }
        /// <summary>
        /// Парс цветовой схемы.
        /// </summary>
        /// <param name="path"> Путь до файла с настройками. </param>
        /// <returns> Цветовая схема CScheme. </returns>
        private static Designer.CScheme ParseScheme(string path)
        {
            int scheme;
            if (!File.Exists(path))
            {
                return Designer.CScheme.WB;
            }
            string[] text = File.ReadAllLines(path);
            if (text.Length == 0)
            {
                return Designer.CScheme.WB;
            }
            try
            {
                if (int.TryParse(text[0], out scheme))
                {
                    if (scheme == (int)Designer.CScheme.WB)
                    {
                        return Designer.CScheme.WB;
                    }
                    else
                    {
                        return Designer.CScheme.BW;
                    }
                }
                else
                {
                    return Designer.CScheme.WB;
                }
            }
            catch
            {
                return Designer.CScheme.WB;
            }
        }
    }
}
