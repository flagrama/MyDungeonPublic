using System;
using System.Collections.Generic;

namespace MyDungeon.Demo
{
    [Serializable]
    public class MySaveData : SaveData
    {
        public string DisplayName = "";
        public List<Item> Inventory = new List<Item>();
        public int MaxHealth = -1;
    }
}