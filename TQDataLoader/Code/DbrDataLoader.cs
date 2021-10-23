using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TQDataLoader.Data;
using TQDataLoader.Data.Xml;

namespace TQDataLoader.Code
{
    public class DbrDataLoader
    {
        public bool IsValidPath { get; private set; }

        public int FileListCount { get; private set; }

        private string strFolderPath;

        private List<string> fileList;

        private List<string> excludedKeyNames;

        private DescriptionManager descriptionKeys;

        public List<string> uniqueDataEntries;

        public List<DbrDataItem> parsedItemList;

        public DataExtractionConfig ExtractionConfig { get; private set; }

        public DbrDataLoader(DataExtractionConfig extractionConfig, DescriptionManager descManager, List<string> excludedKeys)
        {

            descriptionKeys = descManager;
            ExtractionConfig = extractionConfig;
            excludedKeyNames = excludedKeys;
            fileList = new List<string>();
            uniqueDataEntries = new List<string>();
            parsedItemList = new List<DbrDataItem>();

            foreach (var partialPath in ExtractionConfig.TargetDirectories)
            {
                strFolderPath = FileUtils.GetDbrDirectoryPath(partialPath);
                IsValidPath = checkPathValidity();

                getDbrFileNames(strFolderPath);        
            }

            FileListCount = fileList.Count();

            if (fileList.Count() > 0)
            {
                loadFilesData();
            }
        }

        private bool checkPathValidity()
        {
            if (string.IsNullOrWhiteSpace(strFolderPath)) { 
                return false;
            }

            try
            {
                var directories = FileUtils.GetDirectories(strFolderPath);
                var dbrItems = FileUtils.GetDbrFiles(strFolderPath, ExtractionConfig.FileSearchMask);
                return directories.Length > 0;            
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void getDbrFileNames(string sourceDirectory, List<string> currentFiles = null)
        {            

            if (currentFiles == null){
                currentFiles = fileList;
            }

            var dbrItems = FileUtils.GetDbrFiles(sourceDirectory, ExtractionConfig.FileSearchMask);

            if (dbrItems.Length > 0)
            {
                currentFiles.AddRange(dbrItems);
            }

            if (ExtractionConfig.IncludeSubfolders) 
            {
                var directories = FileUtils.GetDirectories(sourceDirectory);

                if (directories.Length > 0)
                {

                    foreach (string dir in directories)
                    {
                        getDbrFileNames(dir, currentFiles);
                    }
                }            
            }
        }


        private bool IsValidMOnsterFile(DbrDataItem item)
        {
            const string CLASS_KEY = "Class";

            var validClasses = new string[] {
                "Cerberus",
                "Hades",
                "Megalesios",
                "Monster",
                "Ormenos",
                "Pet",
                "SpiritHost",
                "Typhon2"
            };

            var classKv = item.KeyValues.FirstOrDefault(x => x.Key == CLASS_KEY);

            if (classKv.Equals(default(KeyValuePair<string, string>)))
            {
                return false;
            }

            if (validClasses.Contains(classKv.Value))
            {
                return true;
            }

            return false;
        }

        private void loadFilesData() {
            foreach (var file in fileList) {

                switch (ExtractionConfig.ResourceType)
                {
                    case ResourceTypeEnum.LootTableWeights:
                        parsedItemList.Add(parseDataLootTableWeights(file, false));
                        break;
                    case ResourceTypeEnum.LootTableIds:
                        parsedItemList.Add(parseDataLootTableWeights(file, true));
                        break;
                    case ResourceTypeEnum.Monsters:
                        var dbrFile = parseData(file);
                        if (IsValidMOnsterFile(dbrFile))
                        {
                            parsedItemList.Add(dbrFile);                        
                        }
                        break;
                    default:
                        parsedItemList.Add(parseData(file));
                        break;
                }
            }
            if ((ExtractionConfig.ResourceType == ResourceTypeEnum.LootTableWeights) || (ExtractionConfig.ResourceType == ResourceTypeEnum.LootTableIds))
            {
                uniqueDataEntries = uniqueDataEntries.OrderBy(x => x).ToList();
                uniqueDataEntries = uniqueDataEntries.OrderBy(x => x.Split('\\').Last()).ToList();
                uniqueDataEntries = uniqueDataEntries.OrderBy(x => (x.Contains("rare_")
                    || x.Contains("damage%lifeleech")
                    || x.Contains("defensive_stunresist")
                    || (x.Contains("skillmastery") && x.Contains("_02"))
                    || (x.Contains("offensive_+%") && !x.Contains("%damage"))
                    || (x.Contains("petbonus_") && x.Contains("_06"))
                    || (x.Contains("petbonus_") && x.Contains("_07")))).ToList();

                parsedItemList = parsedItemList.OrderBy(x => x.ItemPath.Contains("\\suffix\\")).ThenBy(x => x.ItemPath.Split('\\').Last()).ToList();
            }

            if ((ExtractionConfig.ResourceType == ResourceTypeEnum.Other) 
                || (ExtractionConfig.ResourceType == ResourceTypeEnum.Pools)
                || (ExtractionConfig.ResourceType == ResourceTypeEnum.PoolUniqueMonsters))
            {
                uniqueDataEntries = uniqueDataEntries.OrderBy(x => x).ToList();
            }
        }

        private DbrDataItem parseData(string fileName)
        {
            var result = new DbrDataItem(fileName);
            var descriptionValues = new Dictionary<string, string>();

            ExtractionConfig.ResourceTranslatedValues.ForEach(x => descriptionValues[x] = x);

            var lineArray = FileUtils.ReadAllLines(fileName);

            foreach (var dataLine in lineArray) { 
            
                var splitData = dataLine.Split(GlobalConstants.COMMA_CHAR);

                if (splitData.Length < 2) continue;

                var dataEntry = GetDataEntry(splitData[0]);
                var dataValue = splitData[1];

                if (!string.IsNullOrEmpty(dataEntry) && HasValue(dataValue) && !excludedKeyNames.Contains(dataEntry))
                {
                    if (ExtractionConfig.ResourceTranslatedValues.Contains(dataEntry))
                    {
                        var descriptionValue = descriptionKeys.GetDescriptionValue(ExtractionConfig.ResourceType, dataValue);
                        descriptionValues[dataEntry] = string.IsNullOrWhiteSpace(descriptionValue) ? dataValue : descriptionValue;
                    }
                    else 
                    {
                        result.KeyValues.Add(new KeyValuePair<string, string>(dataEntry, dataValue));
                    }
                }
            }

            result.KeyValues = CleanUpValues(result.KeyValues);

            foreach (var entry in result.KeyValues.Select(x => x.Key).Distinct())
            {
                if (!uniqueDataEntries.Contains(entry))
                {
                    uniqueDataEntries.Add(entry);
                }                
            }

            result.GameDescription = string.Join(GlobalConstants.SPACE_STRING, descriptionValues.ToArray().Select(x => x.Value));

            return result;
        }

        private List<KeyValuePair<string, string>> CleanUpValues(List<KeyValuePair<string, string>> list)
        {
            if (ExtractionConfig.ResourceType == ResourceTypeEnum.PoolUniqueMonsters)
            {
                var newList = new List<KeyValuePair<string, string>>();

                newList.AddRange(AddMonsters(list, false));

                newList.AddRange(AddMonsters(list, true));

                return newList;
            }

            return list;
        }

        private List<KeyValuePair<string, string>> AddMonsters(List<KeyValuePair<string, string>> list, bool isChampion)
        {
            bool isValid = false;
            const string CHAMP_MIN = "championMin";
            var newList = new List<KeyValuePair<string, string>>();

            string numberName = isChampion ? "championChance" : "spawnMax";
            string nameNext = isChampion ? "Champion" : string.Empty;

            var monsterChance = list.FirstOrDefault(x => x.Key == numberName);

            if (isChampion)
            {
                // Parecer que si champMin es 1, podría salir el boss igual.
                var championMin = list.FirstOrDefault(x => x.Key == CHAMP_MIN);
                isValid = isValidNumberKv(championMin) || isValidNumberKv(monsterChance);
            }
            else
            {
                isValid = isValidNumberKv(monsterChance);
            }


            if (isValid)
            {
                for (var num = 1; num < 31; num++)
                {
                    var monsterName = list.Where(x => x.Key.EndsWith("-name" + nameNext + num.ToString())).FirstOrDefault();
                    var monsterWheight = list.Where(x => x.Key.EndsWith("-weight" + nameNext + num.ToString())).FirstOrDefault();

                    if (!monsterName.Equals(default(KeyValuePair<string, string>)) && !monsterWheight.Equals(default(KeyValuePair<string, string>)))
                    {
                        int weight = string.IsNullOrWhiteSpace(monsterWheight.Value) ? 0 : int.Parse(monsterWheight.Value);
                        if (weight > 0)
                        {
                            newList.Add(new KeyValuePair<string, string>(monsterName.Key, monsterName.Value.Trim().ToLower()));
                        }
                    }
                }
            }

            return newList;            
        }

        private bool isValidNumberKv(KeyValuePair<string, string> kv)
        {
            var notValid = kv.Equals(default(KeyValuePair<string, string>)) || string.IsNullOrWhiteSpace(kv.Value) || (decimal.Parse(kv.Value) <= 0);

            return !notValid;
        }

        private string GetDataEntry(string entryName)
        {

            string result = entryName;

            if (ExtractionConfig.ResourceType == ResourceTypeEnum.Pools)
            {
                var names = new string[] { "name", "weight", "difficulty", "difficultyCutoff", "limit", "alwaysSpawn" };

                for (var num = 1; num < 31; num++)
                {
                    foreach (var curName in names)
                    {
                        if (entryName == (curName + num.ToString()))
                        {
                            return string.Format("n-{0}-{1}", num.ToString("00"), entryName);
                        }
                        if (entryName == (curName + "Champion" + num.ToString()))
                        {
                            return string.Format("c-{0}-{1}", num.ToString("00"), entryName);
                        }
                    }
                }
            }

            if (ExtractionConfig.ResourceType == ResourceTypeEnum.PoolUniqueMonsters)
            {
                result = string.Empty;

                var otherValid = new string[] { "spawnMax", "championChance" };

                if (otherValid.Contains(entryName))
                {
                    return entryName;
                }

                var names = new string[] { "name", "weight" };

                for (var num = 1; num < 31; num++)
                {
                    foreach (var curName in names)
                    {
                        if (entryName == (curName + num.ToString()))
                        {
                            return string.Format("n-{0}-{1}", num.ToString("00"), entryName);
                        }
                        if (entryName == (curName + "Champion" + num.ToString()))
                        {
                            return string.Format("c-{0}-{1}", num.ToString("00"), entryName);
                        }
                    }
                }
            }

            return result;
        }


        private DbrDataItem parseDataLootTableWeights(string fileName, bool isOnlyNumber)
        {
            const string rngName = "randomizerName";
            const string rngWeight = "randomizerWeight";
            var result = new DbrDataItem(fileName);

            var lineArray = FileUtils.ReadAllLines(fileName);

            var tempNames = new List<KeyValuePair<string, string>>();
            var tempWeights = new List<KeyValuePair<string, string>>();

            foreach (var dataLine in lineArray)
            {
                var splitData = dataLine.Split(GlobalConstants.COMMA_CHAR);

                if (splitData.Length < 2) continue;

                var dataEntry = splitData[0];
                var dataValue = splitData[1];

                if (HasValue(dataValue) && !excludedKeyNames.Contains(dataEntry))
                {
                    if (dataEntry.Contains(rngName))
                    {
                        var name = dataValue.Trim().ToLower();
                        var nameNumber = dataEntry.Replace(rngName, string.Empty).Trim();

                        tempNames.Add(new KeyValuePair<string, string>(name, nameNumber));
                    }

                    if (dataEntry.Contains(rngWeight))
                    {
                        var wheightNumber = dataEntry.Replace(rngWeight, string.Empty).Trim();
                        var weightValue = dataValue;

                        tempWeights.Add(new KeyValuePair<string, string>(weightValue, wheightNumber));
                    }
                }
            }

            foreach (var nameEntry in tempNames)
            {
                var weightEntry = tempWeights.FirstOrDefault(x => x.Value == nameEntry.Value);
                if (!weightEntry.Equals(default(KeyValuePair<string, string>)))
                {
                    var value = isOnlyNumber ? weightEntry.Value : weightEntry.Key;
                    result.KeyValues.Add(new KeyValuePair<string, string>(nameEntry.Key, value));

                    if (!uniqueDataEntries.Contains(nameEntry.Key))
                    {
                        uniqueDataEntries.Add(nameEntry.Key);
                    }
                }
            }

            result.GameDescription = ExtractionConfig.ResourceType.ToString();

            return result;
        }

        private bool HasValue(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            decimal outVal = decimal.MinValue;
            var conversionSuccess = decimal.TryParse(value, out outVal);
            
            // Must be a string so it has value.
            if (!conversionSuccess) return true;

            return (outVal != decimal.MinValue) && (outVal != decimal.Zero);
        }

    }
}
