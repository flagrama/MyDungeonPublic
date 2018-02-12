using System.Collections.Generic;

namespace MyDungeon
{
    public static class GameManager
    {
        public static bool SaveLoaded;
        public static int Floor;
        public static bool Paused = false;
        public static bool PlayersTurn = true;
        public static List<Item> Inventory = new List<Item>();
    }
}