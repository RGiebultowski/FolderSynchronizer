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
                using var sha256_Source = SHA256.Create();
                using var sha256_Replica = SHA256.Create();
                using var sourceStream = File.OpenRead(sourceFile);
                using var replicaStream = File.OpenRead(replicaFile);

                var sourceHash = sha256_Source.ComputeHash(sourceStream);
                var replicaHash = sha256_Replica.ComputeHash(replicaStream);

                return sourceHash.SequenceEqual(replicaHash);
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
