using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQDataLoader.Data
{
    public class DbrDataItem
    {
        public string GameDescription { get; set; }

        public string ItemPath { get; set; }

        public List<KeyValuePair<string, string>> KeyValues { get; set; }

        public DbrDataItem(string itemPath) {
            ItemPath = itemPath.Replace(Code.ConfigUtils.DbrRecordsSourcePath + Code.GlobalConstants.DIRECTORY_SEPARATOR, string.Empty);

            KeyValues = new List<KeyValuePair<string, string>>();
        }
    }
}
