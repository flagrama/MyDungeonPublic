namespace MyDungeon.Items
{
    using UnityEngine;

    /// <summary>
    /// ItemBheaviour is a component intended to be attached to item prefabs to attach ScriptableObjects that derive from the type Item to them
    /// </summary>
    public class ItemBehaviour : MonoBehaviour
    {
        /// <summary>
        /// A ScriptableObject asset of type Item
        /// </summary>
        public Item Item;
    }
}