using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQDataLoader.Code
{
    public class PatcherAction
    {
        public PatcherAction(string patchervalue)
        {
            if (!patchervalue.Contains(GlobalConstants.COMMA_STRING))
            {
                throw new Exception(string.Format("Value '{0}' does not contain a comma separator", patchervalue));
                
            }

            string[] values = patchervalue.Split(GlobalConstants.COMMA_CHAR);

            Property = values[0];
            Value = values[1];
        }

        public string Property { set; get; }

        public string Value { set; get; }

        public string FullValue { get { return Property + GlobalConstants.COMMA_STRING + Value; } }

        public string FullValueWithComma { get { return Property + GlobalConstants.COMMA_STRING + Value + GlobalConstants.COMMA_STRING; } }
    }
}
