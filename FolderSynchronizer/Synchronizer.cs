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
        private readonly IFileComparer fileComparer;

        public Synchronizer(string sourcePath, string replicaPath, Logger logger, IFileComparer fileComparer)
        {
            this.sourcePath = sourcePath;
            this.replicaPath = replicaPath;
            this.logger = logger;
            this.fileComparer = fileComparer;
        }

        public void Synchronize() 
        {
            CreateDirectory(sourcePath);
            CreateDirectory(replicaPath);
            UpdateFiles();
            DeleteFiles();
        }

        private void CreateDirectory(string path)
        {
            var folderName = new DirectoryInfo(path).Name;

            var message = Directory.Exists(path) ?
                $"Folder {folderName} Exists: {path}" :
                $"Created folder: {Directory.CreateDirectory(path).FullName}";

            logger.Log($"[Logger Task] {message}");
        }

        private void UpdateFiles()
        {
            foreach(var sourceFile in Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories))
            {
                var relativePath = Path.GetRelativePath(sourcePath, sourceFile);
                var replicaFile = Path.Combine(replicaPath, relativePath);

                if (!fileComparer.AreEqual(sourceFile, replicaFile))
                {
                    File.Copy(sourceFile, replicaFile, true);
                    logger.Log($"[Logger task] Copied/updated: {relativePath}");
                }
            }
        }

        private void DeleteFiles()
        {
            foreach (var sourceFile in Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories))
            {
                var relativePath = Path.GetRelativePath(sourcePath, sourceFile);
                var replicaFile = Path.Combine(replicaPath, relativePath);

                if (!File.Exists(sourceFile))
                {
                    File.Delete(replicaFile);
                    logger.Log($"[Logger task] Deleted: {relativePath}");
                }
            }
        }
    }
}
