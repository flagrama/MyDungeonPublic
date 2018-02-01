using UnityEngine;

namespace MyDungeon
{
    public abstract class Item : ScriptableObject
    {
        protected Transform Target;
        public AudioClip[] UseSounds;

        public virtual void UseItem(Transform target)
        {
            Target = target;
            Use();
        }

        protected abstract void Use();
    }
}