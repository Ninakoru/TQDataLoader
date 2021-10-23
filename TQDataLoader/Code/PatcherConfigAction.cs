using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQDataLoader.Code
{
    public class PatcherConfigAction
    {
        public PatcherConfigAction()
        {
            TempDirectories = new List<string>();
            TempActions = new List<string>();
            Actions = new List<PatcherAction>();
        }

        public IList<string> TempDirectories { set; get; }

        public IList<string> TempActions  { set; get; }

        public string TargetFile { set; get; }

        public IList<PatcherAction> Actions { set; get; }

        public void LoadActions(IList<string> rawActions)
        {
            foreach (var rawAction in rawActions)
            {
                var actionTemp = new PatcherAction(rawAction);
                if (Actions.Any(x => x.Property == actionTemp.Property))
                { 
                    throw new Exception(string.Format("Error: same property for same file in configuration. Property: '{0}', File: '{1}'.", actionTemp.Property, TargetFile));
                }

                Actions.Add(actionTemp);
            }
        }
    }
}
