namespace MyDungeon.Entities
{
    using Managers;
    using UnityEngine;

    /// <inheritdoc />
    /// <summary>
    /// Base class for entities other than the player in the dungoen
    /// </summary>
    public abstract class Creature : MovingDungeonObject
    {
        /// <summary>
        /// Add the creature to the creature list and sets its health value
        /// </summary>
        protected override void Start()
        {
            base.Start();

            AddCreatureToList(this);
            CurHealth = MaxHealth;
        }

        /// <summary>
        /// Checks if a creature has lost all its HP and removes it from the list if so
        /// </summary>
        protected virtual void Update()
        {
            if (CurHealth > 0) return;

            RemoveCreatureFromList(this);
            Destroy(gameObject);
        }

        /// <summary>
        /// Adds a creature to the list
        /// </summary>
        /// <param name="script">The creature instance</param>
        public virtual void AddCreatureToList(Creature script)
        {
            DungeonManager.Creatures.Add(script);
        }

        /// <summary>
        /// Removes a creature from the list
        /// </summary>
        /// <param name="script">The creature instance</param>
        public virtual void RemoveCreatureFromList(Creature script)
        {
            DungeonManager.Creatures.Remove(script);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">A type that inherits from Component</typeparam>
        /// <param name="xDir">The X direction the creature is attempting to move in</param>
        /// <param name="yDir">The Y direction the creature is attempting to move in</param>
        protected virtual void AttemptMove<T>(int xDir, int yDir)
            where T : Component
        {
            RaycastHit2D hit;
            bool canMove = Move(xDir, yDir, out hit);

            if (hit.transform == null)
                return;

            T hitComponent = hit.transform.GetComponent<T>();

            if (canMove || hitComponent == null) return;

            OnCantMove(hitComponent);
            StartCoroutine(WaitForTurnEnd());
        }

        protected abstract void OnCantMove<T>(T component);

        public abstract void MoveCreature();
    }
}