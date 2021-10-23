using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TQDataLoader.Data.Xml
{
    [Serializable]
    public class DataExtractionConfigList
    {
        [XmlElement("DataExtractionConfig")]
        public List<DataExtractionConfig> Items { set; get; }

        public DataExtractionConfigList()
        {
            Items = new List<DataExtractionConfig>();
        }
    }
}
