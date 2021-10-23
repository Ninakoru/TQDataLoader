using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQDataLoader.Data.DbrTemplates
{
    public interface ITemplateObject
    {
        ITemplateObject Parent { get; set; }

        IList<ITemplateObject> ChildObjects { get; set; }
    }
}
