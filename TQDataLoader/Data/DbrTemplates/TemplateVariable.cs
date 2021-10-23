using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQDataLoader.Data.DbrTemplates
{
    public class TemplateVariable : ITemplateObject
    {
        public ITemplateObject Parent { get; set; }

        public IList<ITemplateObject> ChildObjects { get; set; }


        public string Name { get; set; }

        public string Class { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }

        public string DefaultValue { get; set; }

        public TemplateVariable()
        {
            ChildObjects = new List<ITemplateObject>();
        }
    }
}
