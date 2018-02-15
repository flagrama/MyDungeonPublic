namespace MyDungeon
{
    using System.Collections.Generic;

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
