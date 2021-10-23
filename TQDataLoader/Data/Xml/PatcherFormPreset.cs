using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TQDataLoader.Data.Xml
{
    [Serializable]
    public class PatcherFormPreset
    {
        [XmlElement]
        public string PresetName { set; get; }

        [XmlElement]
        public string SourceDir { set; get; }

        [XmlElement]
        public string DestinationDir { set; get; }
        
        [XmlElement]
        public string ConfigurationDir { set; get; }
        
        [XmlElement]
        public bool DeleteDestination { set; get; }
    }
}
