using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TQDataLoader.Data;

namespace TQDataLoader.Code
{
    public class DescriptionManager
    {
        private ArcFile arcFileData;

        private string[] dirEntries;

        private Dictionary<string, Dictionary<string, string>> descriptionData;

        public DescriptionManager() 
        {
            descriptionData = new Dictionary<string, Dictionary<string, string>>();

            arcFileData = new ArcFile(FileUtils.TextResourcesFile);

            if (ConfigUtils.ExtractTextResources)
            {
                arcFileData.ExtractArcFile(FileUtils.TextResourcesOutputDirectory);
            }

            arcFileData.Read();
            
            dirEntries = arcFileData.GetKeyTable();
        }

        public string GetDescriptionValue(ResourceTypeEnum dirWildcard, string searchingValue)
        {
            var result = string.Empty;
            var possibleValues = GetDescriptions(dirWildcard);
            possibleValues.TryGetValue(searchingValue, out result);
            return result;
        }

        private Dictionary<string, string> GetDescriptions(ResourceTypeEnum dirWildcard)
        {
            const string commonEquipment = "COMMONEQUIPMENT.TXT";
            const string monstersX3 = "TAGS_NONVOICED.TXT";
            const string monstersQuest = "XQUEST.TXT";
            const string npcsX2 = "X2NPC.TXT";
            const string uniquesX2 = "X2UNIQUEEQUIPMENT.TXT";
            const string baseGameX3 = "X3BASEGAME_NONVOICED.TXT";
            const string mainQuestX3 = "X3MAINQUEST_NONVOICED.TXT";
            const string sideQuestsX3 = "X3SIDEQUESTS_NONVOICED.TXT";
            const string relicsX3 = "BASEGAME_NONVOICED.TXT";
            const string itemsX3 = "X3ITEMS_NONVOICED.TXT";

            var textWildcard = Enum.GetName(typeof(ResourceTypeEnum), dirWildcard);
            var entryWildcard = (textWildcard + GlobalConstants.TEXT_FILE_EXTENSION).ToUpper();


            if (!descriptionData.ContainsKey(entryWildcard)) 
            {
                descriptionData[entryWildcard] = GetEntries(entryWildcard);
            }

            if (dirWildcard == ResourceTypeEnum.Monsters)
            {
                AddEntries(commonEquipment, descriptionData[entryWildcard]);
                AddEntries(npcsX2, descriptionData[entryWildcard]);
                AddEntries(uniquesX2, descriptionData[entryWildcard]);
                AddEntries(baseGameX3, descriptionData[entryWildcard]);
                AddEntries(mainQuestX3, descriptionData[entryWildcard]);
                AddEntries(sideQuestsX3, descriptionData[entryWildcard]);
                AddEntries(monstersX3, descriptionData[entryWildcard]);
                AddEntries(monstersQuest, descriptionData[entryWildcard]);
            }

            if (dirWildcard == ResourceTypeEnum.CommonEquipment)
            {
                AddEntries(relicsX3, descriptionData[entryWildcard]);
            }

            if ((dirWildcard == ResourceTypeEnum.CommonEquipment) || (dirWildcard == ResourceTypeEnum.UniqueEquipment))
            {
                AddEntries(itemsX3, descriptionData[entryWildcard]);
            }

            return descriptionData[entryWildcard];
        }

        private Dictionary<string, string> GetEntries(string dirWildcard)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            
            var dirs = dirEntries.Where(x => x.ToUpper().Contains(dirWildcard.ToUpper()));

            foreach (var dir in dirs)
            {
                var values = arcFileData.GetResourceValues(dir).ToList();
                values.ForEach(x => result[x.Key] = UncommentedValue(x.Value));
            }

            return result;
        }

        private void AddEntries(string dirWildcard, Dictionary<string, string> dictionary)
        {
            var dirs = dirEntries.Where(x => x.ToUpper().Contains(dirWildcard.ToUpper()));

            foreach (var dir in dirs)
            {
                var values = arcFileData.GetResourceValues(dir).ToList();
                values.ForEach(x => dictionary[x.Key] = UncommentedValue(x.Value));
            }
        }

        private string UncommentedValue(string originalValue)
        {
            if (originalValue.Contains("//")) 
            {
                return originalValue.Split(new string[] { "//" }, StringSplitOptions.None)[0].TrimEnd();
            }

            return originalValue;
        }
    }
}
