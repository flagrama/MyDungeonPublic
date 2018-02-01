using UnityEngine;

namespace MyDungeon
{
    public abstract class Item : ScriptableObject
    {
        public AudioClip[] useSounds;

        protected Transform _target;

        public virtual void UseItem(Transform target)
        {
            _target = target;
            Use(target);
        }

        protected abstract void Use(Transform target);
    }
}
