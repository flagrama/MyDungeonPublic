namespace MyDungeon.Managers
{
    using System.Collections.Generic;
    using Items;
    using UnityEngine;

    /// <summary>
    /// PlayerManager holds player specific properties that must be retained between scene transitions
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        /// <summary>
        /// The current PlayerManager instance
        /// </summary>
        public static PlayerManager Instance;
        /// <summary>
        /// List of items in the player's inventory
        /// </summary>
        public static List<Item> Inventory = new List<Item>();

        /// <summary>
        /// Initializes the PlayerManager instance
        /// </summary>
        protected virtual void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Resets all PlayerManager properties to default values
        /// </summary>
        public virtual void Reset()
        {
            Inventory = new List<Item>();
        }
    }
}