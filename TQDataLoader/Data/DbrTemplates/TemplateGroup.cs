using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQDataLoader.Data.DbrTemplates
{
    public class TemplateGroup : ITemplateObject
    {
        public ITemplateObject Parent { get; set; }

        public IList<ITemplateObject> ChildObjects { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public IList<TemplateGroup> ChildGroups;

        public IList<TemplateVariable> Variables;

        public TemplateGroup()
        {
            ChildObjects = new List<ITemplateObject>();

            ChildGroups = new List<TemplateGroup>();
            Variables = new List<TemplateVariable>();
        }
    }
}
