using UnityEngine;

namespace MyDungeon.Items
{
    /// <summary>
    /// Item is the base class for Items
    /// </summary>
    public abstract class Item : ScriptableObject
    {
        /// <summary>
        /// The target game object to affect with an item
        /// </summary>
        protected Transform Target;
        /// <summary>
        /// An array of sounds an item can choose from when an item is used
        /// </summary>
        public AudioClip[] UseSounds;

        /// <summary>
        /// Sets Target and calls Use() for inheriting classes
        /// </summary>
        /// <param name="target">Target transform to be passed to inheriting class</param>
        public virtual void UseItem(Transform target)
        {
            Target = target;
            Use();
        }
        
        protected abstract void Use();
    }
}