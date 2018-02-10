using UnityEngine;

namespace MyDungeon
{
    /// <summary>
    /// Component intended to be attached to item prefabs to attach ScriptableObjects that derive fromt he type Item to them
    /// </summary>
    public class ItemBehaviour : MonoBehaviour
    {
        /// <summary>
        /// A ScriptableObject asset of type Item
        /// </summary>
        public Item Item;
    }
}