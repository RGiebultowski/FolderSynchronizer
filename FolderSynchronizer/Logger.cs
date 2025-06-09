using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSynchronizer
{
    internal class Logger
    {
        private readonly string loggerFilePath;

        public Logger(string loggerFilePath) 
        {
            string? logDirectory = Path.GetDirectoryName(loggerFilePath);
            string logFileName = Path.GetFileNameWithoutExtension(loggerFilePath);

            if (string.IsNullOrWhiteSpace(logDirectory))
                throw new ArgumentException("Wrong log file path.");

            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);

            string time = DateTime.Now.ToString("yyyy-MM-dd");
            this.loggerFilePath = Path.Combine(logDirectory, $"{logFileName}_{time}.log");
        }

        public void Log(string message)
        {
            string consoleLog = $"[{DateTime.Now}]: {message}";
            Console.WriteLine(consoleLog);
            try
            {
                File.AppendAllText(loggerFilePath, message + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Could not write to log file: {ex.Message}");
            }
        }
    }
}
