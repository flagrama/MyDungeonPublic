using UnityEngine;

namespace MyDungeon
{
    /// <summary>
    /// Component intended to be attached to item prefabs to attach <c>Item</c> <c>ScriptableObjects</c> to them
    /// </summary>
    public class ItemBehaviour : MonoBehaviour
    {
        /// <summary>
        /// A <c>ScriptableObject</c> asset of type <c>Item</c>
        /// </summary>
        public Item Item;
    }
}