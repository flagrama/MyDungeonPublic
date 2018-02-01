using UnityEngine;

namespace MyDungeon
{
    /// <summary>
    /// Base class for Items
    /// </summary>
    public abstract class Item : ScriptableObject
    {
        protected Transform Target;
        public AudioClip[] UseSounds;

        /// <summary>
        /// Sets <c>Target</c> and calls <c>Use()</c> for inheriting classes
        /// </summary>
        /// <param name="target">Target transform to be passed to inheriting class</param>
        public virtual void UseItem(Transform target)
        {
            Target = target;
            Use();
        }

        /// <summary>
        /// Implemntation method for <c>UseItem</c>
        /// </summary>
        protected abstract void Use();
    }
}