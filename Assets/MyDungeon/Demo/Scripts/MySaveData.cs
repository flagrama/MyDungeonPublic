using System.Collections.Generic;

namespace MyDungeon.Demo
{
    public class MySaveData : SaveData
    {
        public List<Item> inventory = new List<Item>();
        public string displayName = "";
        public int maxHealth = -1;
    }
}
