using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TQDataLoader.Data.DbrTemplates;

namespace TQDataLoader.Code
{
    public class DbrTemplateParser
    {
        private const string TEMPLATE_SOURCE_PATH = "database\\templates\\";
        private const string TEMPLATE_DIR_VARIABLE = "%template_dir%";

        private const string GROUP_KEYNAME = "Group";
        private const string VARIABLE_KEYNAME = "Variable";

        private const string NAME_KEYNAME = "name";
        private const string TYPE_KEYNAME = "type";
        private const string CLASS_KEYNAME = "class";
        private const string DESCRIPTION_KEYNAME = "description";
        private const string VALUE_KEYNAME = "value";
        private const string DEFAULTVALUE_KEYNAME = "defaultValue";

        private string sourcedir;

        public DbrTemplateParser(string templatesDirectory)
        {
            sourcedir = templatesDirectory;
        }

        public TemplateGroup ParseTemplate(string templateName)
        {
            var templateFile = FileUtils.GetCombinedPath(sourcedir, GetTemplateName(templateName));

            var templateContents = FileUtils.ReadFile(templateFile);

            return ParseContents(templateContents);
        }

        private TemplateGroup ParseContents(string templateContents)
        {
            TemplateGroup result = null;
            ITemplateObject currentElement = null;

            var content = templateContents.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None)
                .Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            foreach (var line in content)
            {
                if (IsGroup(line))
                {
                    ITemplateObject newGroup = new TemplateGroup();
                    if (currentElement != null)
                    {
                        newGroup.Parent = currentElement;
                        currentElement.ChildObjects.Add(newGroup);
                    }

                    currentElement = newGroup;

                    if (result == null)
                    {
                        result = currentElement as TemplateGroup;
                    }

                    continue;
                }

                if (IsVariable(line))
                {
                    ITemplateObject newVariable = new TemplateVariable();
                    newVariable.Parent = currentElement;
                    currentElement.ChildObjects.Add(newVariable);
                    currentElement = newVariable;

                    continue;
                }

                if (IsName(line))
                {
                    if (currentElement is TemplateGroup)
                    {
                        var groupTemp = currentElement as TemplateGroup;

                        groupTemp.Name = GetQuotedValue(line);
                    }
                    else if (currentElement is TemplateVariable)
                    {
                        var varTemp = currentElement as TemplateVariable;

                        varTemp.Name = GetQuotedValue(line);
                    }
                    else 
                    {
                        ThrownUnexpectedTypeOrNullException();
                    }

                    continue;
                }

                if (IsType(line))
                {
                    if (currentElement is TemplateGroup)
                    {
                        var groupTemp = currentElement as TemplateGroup;

                        groupTemp.Type = GetQuotedValue(line);
                    }
                    else if (currentElement is TemplateVariable)
                    {
                        var varTemp = currentElement as TemplateVariable;

                        varTemp.Type = GetQuotedValue(line);
                    }
                    else 
                    {
                        ThrownUnexpectedTypeOrNullException();
                    }

                    continue;
                }

                if (IsClass(line))
                {
                    if (currentElement is TemplateVariable)
                    {
                        var varTemp = currentElement as TemplateVariable;

                        varTemp.Class = GetQuotedValue(line);
                    }
                    else
                    {
                        ThrownUnexpectedTypeOrNullException();
                    }

                    continue;
                }

                if (IsDescription(line))
                {
                    if (currentElement is TemplateVariable)
                    {
                        var varTemp = currentElement as TemplateVariable;

                        varTemp.Description = GetQuotedValue(line);
                    }
                    else
                    {
                        ThrownUnexpectedTypeOrNullException();
                    }

                    continue;
                }

                if (IsValue(line))
                {
                    if (currentElement is TemplateVariable)
                    {
                        var varTemp = currentElement as TemplateVariable;

                        varTemp.Value = GetQuotedValue(line);
                    }
                    else
                    {
                        ThrownUnexpectedTypeOrNullException();
                    }

                    continue;
                }

                if (IsDefaultValue(line))
                {
                    if (currentElement is TemplateVariable)
                    {
                        var varTemp = currentElement as TemplateVariable;

                        varTemp.DefaultValue = GetQuotedValue(line);
                    }
                    else
                    {
                        ThrownUnexpectedTypeOrNullException();
                    }

                    continue;
                }

                if (IsOpeningBrackets(line))
                {
                    continue;
                }

                if (IsClosingBrackets(line))
                {
                    if (currentElement != null)
                    {
                        currentElement = currentElement.Parent;
                    }

                    continue;
                }
            }

            return ParseIncludesRecursive(result);
        }

        private TemplateGroup ParseIncludesRecursive(TemplateGroup initialGroup)
        {
            var result = initialGroup;

            if (result.ChildObjects.Any())
            {
                for (int i = 0; i < result.ChildObjects.Count; ++i)
                {
                    var currentElement = result.ChildObjects[i] as TemplateVariable;

                    if ((currentElement != null) && (currentElement.Type == "include"))
                    {
                        result.ChildObjects[i] = ParseTemplate(currentElement.DefaultValue);
                    }
                }
            }

            return result;
        }

        private void ThrownUnexpectedTypeOrNullException()
        {
            throw new Exception("Current Element of unexcepted type or null.");
        }

        private bool IsOpeningBrackets(string line)
        {
            return line == GlobalConstants.OPENING_BRACKET_STRING;
        }

        private bool IsClosingBrackets(string line)
        {
            return line == GlobalConstants.CLOSING_BRACKET_STRING;
        }

        private bool IsGroup(string line)
        {
            return line == GROUP_KEYNAME;
        }

        private bool IsVariable(string line)
        {
            return line == VARIABLE_KEYNAME;
        }

        private bool IsName(string line)
        {
            return line.StartsWith(NAME_KEYNAME);
        }

        private bool IsType(string line)
        {
            return line.StartsWith(TYPE_KEYNAME);
        }

        private bool IsClass(string line)
        {
            return line.StartsWith(CLASS_KEYNAME);
        }

        private bool IsDescription(string line)
        {
            return line.StartsWith(DESCRIPTION_KEYNAME);
        }

        private bool IsValue(string line)
        {
            return line.StartsWith(VALUE_KEYNAME);
        }

        private bool IsDefaultValue(string line)
        {
            return line.StartsWith(DEFAULTVALUE_KEYNAME);
        }

        private string GetQuotedValue(string line)
        {
            var value = line.Substring(line.IndexOf(GlobalConstants.QUOTE_CHAR) + 1);
            value = value.Substring(0, value.IndexOf(GlobalConstants.QUOTE_CHAR));
            return value;
        }

        private string GetTemplateName(string template)
        {
            return template.ToLower().Replace(TEMPLATE_SOURCE_PATH, string.Empty).Replace(TEMPLATE_DIR_VARIABLE, string.Empty);
        }
    }
}
