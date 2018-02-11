using UnityEngine;

namespace MyDungeon
{
    public class Creature : MovingDungeonObject
    {
        public AudioClip EnemyAttack1;
        public AudioClip EnemyAttack2;

        protected CreatureController CreatureController;

        // Use this for initialization
        protected override void Start()
        {
            base.Start();

            try
            {
                CreatureController = DungeonManager.GetComponent<CreatureController>();
            }
            catch
            {
                if(DungeonManager != null)
                    Utilities.MyDungeonErrors.CreatureControllerNotFound(DungeonManager.name);
                else
                    Utilities.MyDungeonErrors.DungeonManagerNotFound();
            }

            CreatureController.AddCreatureToList(this);
            CurHealth = MaxHealth;
        }

        protected virtual void Update()
        {
            if (CurHealth > 0) return;

            CreatureController.RemoveCreatureFromList(this);
            Destroy(gameObject);
        }

        protected override void OnCantMove<T>(T component)
        { }

        public virtual void MoveCreature()
        { }
    }
}