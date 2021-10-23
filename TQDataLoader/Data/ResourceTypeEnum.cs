using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQDataLoader.Data
{
    /// <summary>
    /// Must match the ending name of the file(s) without extension if converted to uppercase.
    /// </summary>
    public enum ResourceTypeEnum
    {
        Monsters,
        CommonEquipment,
        UniqueEquipment,
        Skills,
        LootTableWeights,
        LootTableIds,
        Pools,
        PoolUniqueMonsters,
        Other
    }
}
