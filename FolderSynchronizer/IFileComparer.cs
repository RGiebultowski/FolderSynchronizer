using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSynchronizer
{
    internal interface IFileComparer
    {
        bool AreEqual(string file1, string file2);
    }
}
