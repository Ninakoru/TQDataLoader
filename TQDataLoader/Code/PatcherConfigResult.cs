using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQDataLoader.Code
{
    public class PatcherConfigResult
    {
        public PatcherConfigResult()
        { 
            FailureMessages = new List<string>();
        }

        public bool LoadSuccess { get; set; }

        public IList<string> FailureMessages { get; set; }


        public bool HasFailures { get { return FailureMessages.Any(); } }

        public void AddFailureMessage(string message)
        {
            FailureMessages.Add(message);
        }
    }
}
