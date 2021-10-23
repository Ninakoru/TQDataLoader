using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TQDataLoader.Data.Xml
{
    [Serializable]
    public class ExcludedKeyList
    {
        [XmlElement("KeyName")]
        public List<string> Items { set; get; }
    }
}
