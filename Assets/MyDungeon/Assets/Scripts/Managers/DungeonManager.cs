using System.Collections.Generic;
using UnityEngine;

namespace MyDungeon
{
    public static class DungeonManager
    {
        /// <summary>
        /// The list of all the creatures in the current map
        /// </summary>
        public static List<Creature> Creatures;

        public static GridGenerator DungeonGenerationSettings;
    }
}
