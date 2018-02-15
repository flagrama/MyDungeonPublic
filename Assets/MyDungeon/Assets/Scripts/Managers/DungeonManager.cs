using System.Collections.Generic;
using MyDungeon.DungeonGeneration.GridBasedGenerator;
using MyDungeon.Entities;

namespace MyDungeon.Managers
{
    /// <summary>
    /// DungeonManager holds information of a dungeon
    /// </summary>
    public static class DungeonManager
    {
        /// <summary>
        /// The list of all the creatures in the current dungeon
        /// </summary>
        public static List<Creature> Creatures;

        /// <summary>
        /// The generation settings of the current dungeon
        /// </summary>
        public static GridGenerator DungeonGenerationSettings;
    }
}
