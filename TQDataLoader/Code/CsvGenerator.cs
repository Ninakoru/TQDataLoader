using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TQDataLoader.Data;

namespace TQDataLoader.Code
{
    public class CsvGenerator
    {
        private List<string> uniqueDataEntries;

        private List<DbrDataItem> parsedItemList;

        private List<string> csvLines;

        private ResourceTypeEnum resourceType;

        private string CleanedUpValue(string value) 
        {
            return value.Replace(GlobalConstants.CSV_SEPARATOR, GlobalConstants.SEMICOLON_STRING);
        }

        public CsvGenerator(List<string> uniqueEntries, List<DbrDataItem> parsedItems, ResourceTypeEnum resType)
        {
            uniqueDataEntries = uniqueEntries;
            parsedItemList = parsedItems;
            resourceType = resType;
        }

        public void GenerateCsvLines()
        {
            if (resourceType == ResourceTypeEnum.PoolUniqueMonsters)
            {
                GenerateCsvLinesUniqueList();
            }
            else
            {
                GenerateCsvLinesStandard();
            }
        }

        public void GenerateCsvLinesStandard()
        {
            csvLines = new List<string>();

            var uniquePlusFilename = new List<string>();
            uniquePlusFilename.Add(GlobalConstants.GAME_DESCRIPTION_KEY);
            uniquePlusFilename.AddRange(uniqueDataEntries);
            uniquePlusFilename.Add(GlobalConstants.FILE_NAME_KEY);

            var header = String.Join(GlobalConstants.CSV_SEPARATOR, uniquePlusFilename);

            csvLines.Add(header);

            foreach (var dbrItem in parsedItemList)
            {
                var lineValues = new List<string>();

                lineValues.Add(CleanedUpValue(dbrItem.GameDescription));

                foreach (string keyName in uniqueDataEntries)
                {
                    var values = dbrItem.KeyValues;
                    var item = values.FirstOrDefault(x => x.Key == keyName);
                    var isDefault = item.Equals(default(KeyValuePair<string, string>));
                    lineValues.Add(isDefault ? string.Empty : CleanedUpValue(item.Value));
                }

                lineValues.Add(dbrItem.ItemPath);

                var line = String.Join(GlobalConstants.CSV_SEPARATOR, lineValues);

                csvLines.Add(line);
            }
        }

        public void GenerateCsvLinesUniqueList()
        {
            csvLines = new List<string>();

            var uniquePlusFilename = new List<string>();
            uniquePlusFilename.Add(GlobalConstants.PATH_KEY);
            uniquePlusFilename.Add(GlobalConstants.MONSTER_REF_KEY);

            var header = String.Join(GlobalConstants.CSV_SEPARATOR, uniquePlusFilename);

            csvLines.Add(header);

            var pathTypes = new string[] { "proxies boss", "proxies egypt", "proxies greek", "proxies orient", "proxies quest", "xpack\\", "xpack2\\", "xpack3\\" };

            var values = new List<KeyValuePair<string, List<string>>>();

            foreach (var path in pathTypes)
            { 
                values.Add(new KeyValuePair<string,List<string>>(path, new List<string>()));
            }

            foreach (var dbrItem in parsedItemList)
            {
                var currentKey = values.Where(x => dbrItem.ItemPath.StartsWith(x.Key)).FirstOrDefault();

                if (currentKey.Equals(default(KeyValuePair<string, List<string>>)))
                {
                    continue;
                }

                currentKey.Value.AddRange(dbrItem.KeyValues.Select(x => x.Value).ToList());
            }

            foreach (var val in values)
            {
                var monsterList = val.Value.Distinct().ToList();

                monsterList.ForEach(x => csvLines.Add(String.Join(GlobalConstants.CSV_SEPARATOR, val.Key, x)));
            }
        }

        public void SaveFile(string fileName)
        {
            FileUtils.SaveOutputFile(csvLines.ToArray(), fileName);
        }

    }
}
