using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSynchronizer
{
    internal class FolderSynchrinizer
    {
        private readonly string sourcePath;
        private readonly string replicaPath;

        public FolderSynchrinizer(string sourcePath, string replicaPath)
        {
            this.sourcePath = sourcePath;
            this.replicaPath = replicaPath;
        }
    }
}
