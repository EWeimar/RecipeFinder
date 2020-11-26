using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DevTools.Util
{
    public class Logger
    {
        private static string LogFileName = "debug.txt";

        public static string getLogPath()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            return String.Format("{0}\\RecipeFinder.DevTools\\Logs\\{1}", projectDirectory, LogFileName);
        }

        public static void Log(string message)
        {
            string logMessage = String.Format("{0} {1}", DateTime.Now.ToString(), message);

            appendLine(logMessage);
        }

        private static void appendLine(string text)
        {
            using (StreamWriter sw = File.AppendText(getLogPath()))
            {
                sw.WriteLine(text);
            }
        }
    }
}
