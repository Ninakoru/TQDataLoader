using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TQDataLoader.Data.DbrTemplates;

namespace TQDataLoader.Code
{
    public class DbrTemplateManager
    {
        private string sourcedir;
        private DbrTemplateParser parser;
        private IList<TemplateGroup> templates;
        private IList<string> allVariableNames;

        public DbrTemplateManager(string templatesDirectory)
        {
            sourcedir = templatesDirectory;
            templates = new List<TemplateGroup>();
            parser = new DbrTemplateParser(templatesDirectory);
        }

        public bool ParseTemplates(IList<string> targetTemplates)
        {
            foreach (var templateSource in targetTemplates)
            {
                templates.Add(parser.ParseTemplate(templateSource));
            }

            GenerateAllVariables();

            return true;
        }

        public bool IsValidPropertyName(string name)
        {
            return allVariableNames.Contains(name);
        }

        private void GenerateAllVariables()
        {
            allVariableNames = CreateNewVariableList();

            foreach (var group in templates)
            {
                AddNameRescursive(group.ChildObjects, ref allVariableNames);
            }
        }

        private void AddNameRescursive(IList<ITemplateObject> list, ref IList<string> currentList)
        {
            foreach (var child in list)
            {
                if (child is TemplateVariable)
                {
                    var name = (child as TemplateVariable).Name;

                    if (!currentList.Contains(name))
                    {
                        currentList.Add(name);
                    }                        
                }
                if (child is TemplateGroup)
                {
                    AddNameRescursive((child as TemplateGroup).ChildObjects, ref currentList);
                }
            }
        }

        private IList<string> CreateNewVariableList()
        {
            // Valid property but not found on templates.
            return new List<string> { GlobalConstants.TEMPLATE_KEYNAME };
        }

    }
}
