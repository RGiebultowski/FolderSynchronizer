using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSynchronizer
{
    internal class Logger
    {
        private readonly string _loggerFilePath;
        private readonly string _logDirectory= @"C:\Logs";

        public Logger(string loggerFile) 
        {
            loggerFile = _loggerFilePath;
            
            if(!Directory.Exists(_logDirectory))
                Directory.CreateDirectory(_logDirectory);

            string time = DateTime.Now.ToString("yyyy-MM-dd");
            _loggerFilePath = Path.Combine(_logDirectory, $"log_{time}.log");
        }

        public void Log(string message)
        {
            string consoleLog = $"[{DateTime.Now}]: {message}";
            Console.WriteLine(consoleLog);
            try
            {
                File.AppendAllText(_loggerFilePath, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Logger Error] Could not write to log file: {ex.Message}");
            }
        }
    }
}
