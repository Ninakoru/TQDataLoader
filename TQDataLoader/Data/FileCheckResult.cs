using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQDataLoader.Data
{
    public class FileCheckResult
    {
        public int FileCount { get; private set; }

        public int DirectoryCount { get; private set; }

        public bool IsValid { get; private set; }

        public FileCheckResult(int numFiles, int numDirectories, bool valid)
        {
            FileCount = numFiles;
            DirectoryCount = numDirectories;
            IsValid = valid;
        }
    }
}
