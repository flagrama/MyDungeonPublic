using UnityEngine;
using System.Collections.Generic;

namespace MyDungeon
{
    public class Inventory : MonoBehaviour
    {
        public List<Item> InventoryItems;

        // Use this for initialization
        protected virtual void Start()
        {
            InventoryItems = GameManager.Inventory;
        }

        protected virtual void OnDestroy()
        {
            GameManager.Inventory = InventoryItems;
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
