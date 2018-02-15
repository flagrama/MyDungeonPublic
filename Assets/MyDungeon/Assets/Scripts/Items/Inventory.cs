namespace MyDungeon
{
    using UnityEngine;
    using System.Collections.Generic;

    /// <summary>
    /// Inventory holds a list of items the player currently possesses
    /// </summary>
    public class Inventory : MonoBehaviour
    {
        /// <summary>
        /// List of items in inventory
        /// </summary>
        public List<Item> InventoryItems;

        /// <summary>
        /// Grabs inventory from GameManager
        /// </summary>
        protected virtual void Start()
        {
            InventoryItems = PlayerManager.Inventory;
        }

        /// <summary>
        /// Stores inventory in GameManager to retain during scene transitions
        /// </summary>
        protected virtual void OnDestroy()
        {
            PlayerManager.Inventory = InventoryItems;
        }

        /// <summary>
        /// Adds item to inventory
        /// </summary>
        /// <param name="item"></param>
        public virtual void AddItem(Item item)
        {
            InventoryItems.Add(item);
        }

        /// <summary>
        /// Removes item from inventory
        /// </summary>
        /// <param name="item"></param>
        public virtual void RemoveItem(Item item)
        {
            InventoryItems.Remove(item);
        }
    }
}
