using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSynchronizer
{
    internal class Synchronizer
    {
        private readonly string sourcePath;
        private readonly string replicaPath;
        private readonly Logger logger;

        public Synchronizer(string sourcePath, string replicaPath, Logger logger)
        {
            this.sourcePath = sourcePath;
            this.replicaPath = replicaPath;
            this.logger = logger;
        }

        public void Synchronize() 
        {
            CreateDirectory(sourcePath);
            CreateDirectory(replicaPath);
        }

        public void CreateDirectory(string path)
        {
            var message = Directory.Exists(path) ?
                $"Source Folder Exists: {path}" :
                $"Created folder: {Directory.CreateDirectory(path).FullName}";

            logger.Log($"[Logger Task] {message}");
        }
    }
}
