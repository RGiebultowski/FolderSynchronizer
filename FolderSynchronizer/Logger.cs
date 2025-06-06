using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSynchronizer
{
    internal class Logger
    {
        private readonly string _loggerFile;

        public Logger(string _loggerFile) 
        {
            this._loggerFile = _loggerFile;
        }

        public void Log(string message)
        {
            string logTime = $"[{DateTime.Now}]: {message}";
            File.AppendAllText(_loggerFile, message);
        }
    }
}
