using UnityEngine;

namespace MyDungeon
{
    public class Creature : MovingObject
    {
        private Animator _animator;
        private CreatureController _creatureController;
        private bool _skipMove;

        public AudioClip EnemyAttack1;
        public AudioClip EnemyAttack2;
        public int XpValue;

        private void Awake()
        {
            _creatureController = GameObject.FindGameObjectWithTag("DungeonManager").GetComponent<CreatureController>();
        }

        // Use this for initialization
        protected override void Start()
        {
            _creatureController.AddCreatureToList(this);
            _animator = GetComponent<Animator>();
            CurHealth = MaxHealth;
            base.Start();
        }

        private void Update()
        {
            if (CurHealth <= 0)
            {
                _creatureController.RemoveCreatureFromList(this);
                GameObject.FindGameObjectWithTag("HudManager").GetComponent<MessageLogDisplay>().AddMessage(DisplayName + " was defeated!");
                Destroy(gameObject);
            }
        }

        protected override void AttemptMove<T>(int xDir, int yDir)
        {
            if (_skipMove)
            {
                _skipMove = false;
                return;
            }

            base.AttemptMove<T>(xDir, yDir);
            _skipMove = true;
        }

        protected override void OnCantMove<T>(T component)
        {
            Player hitPlayer = component as Player;
            if (hitPlayer != null) hitPlayer.LoseHealth(Strength);
            _animator.SetTrigger("enemy1Attack");
            SoundManager.Instance.RandomizeSfx(EnemyAttack1, EnemyAttack2);
        }

        public virtual void MoveCreature()
        {
            return;
        }
    }
}