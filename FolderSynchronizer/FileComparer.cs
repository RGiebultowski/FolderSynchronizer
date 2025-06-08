using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FolderSynchronizer
{
    internal class FileComparer : IFileComparer
    {
        private readonly Logger logger;
        public FileComparer(Logger logger) 
        {
            this.logger = logger;
        }
        public bool AreEqual(string sourceFile, string replicaFile)
        {
            try
            {
                using var md5 = MD5.Create();
                using var sourceStream = File.OpenRead(sourceFile);
                using var replicaStream = File.OpenRead(replicaFile);

                return md5.ComputeHash(sourceStream).SequenceEqual(md5.ComputeHash(replicaStream));
            }
            catch (FileNotFoundException)
            {
                logger.Log("[Warning] Source folder is not same as Backup");
                return false;
            }
            catch (Exception ex)
            {
                logger.Log($"[Error comparing files] {ex.Message}");
                return false;
            }
        }
    }
}
