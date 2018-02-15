using System;
using System.Collections.Generic;
using MyDungeon.Items;
using MyDungeon.Saving;

namespace MyDungeon._Demo.Saving
{
    [Serializable]
    public class MySaveData : SaveData
    {
        public string DisplayName = "";
        public List<Item> Inventory = new List<Item>();
        public int MaxHealth = -1;
    }
}