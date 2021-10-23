using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TQDataLoader.Data.Xml
{
    [Serializable]
    public class DataExtractionConfig
    {
        [XmlElement]
        public string Description { set; get; }

        [XmlElement("TargetDirectory")]
        public List<string> TargetDirectories { set; get; }

        [XmlElement]
        public bool IncludeSubfolders { set; get; }

        [XmlElement]
        public string FileSearchMask { set; get; }

        [XmlElement]
        public ResourceTypeEnum ResourceType { set; get; }

        [XmlElement("ResourceTranslatedValue")]
        public List<string> ResourceTranslatedValues { set; get; }

        public DataExtractionConfig()        
        {
            TargetDirectories = new List<string>();
            ResourceTranslatedValues = new List<string>();
        }
    }
}
