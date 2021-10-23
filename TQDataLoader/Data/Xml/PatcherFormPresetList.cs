using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TQDataLoader.Data.Xml
{
    [Serializable]
    public class PatcherFormPresetList
    {
        [XmlElement("PatcherFormPreset")]
        public List<PatcherFormPreset> Items { set; get; }

        public PatcherFormPresetList()
        {
            Items = new List<PatcherFormPreset>();
        }
    }
}
