using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Item = MyDungeon.Item;

namespace MyDungeon
{
    public class Inventory : MonoBehaviour
    {
        public List<Item> InventoryItems;

        // Use this for initialization
        protected virtual void Start()
        {
            InventoryItems = MyDungeon.GameManager.Inventory;
        }

        protected virtual void OnDestroy()
        {
            MyDungeon.GameManager.Inventory = InventoryItems;
        }

        public virtual void AddItem(Item item)
        {
            InventoryItems.Add(item);
        }

        public virtual void RemoveItem(Item item)
        {
            InventoryItems.Remove(item);
        }
    }
}
