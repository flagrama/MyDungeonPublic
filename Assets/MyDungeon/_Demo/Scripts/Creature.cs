using UnityEngine;

namespace MyDungeon.Demo
{
    public class Creature : MovingObject
    {
        private Animator _animator;
        private CreatureController _creatureController;
        private bool _skipMove;
        private Transform _target;

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
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            CurHealth = MaxHealth;
            base.Start();
        }

        private void Update()
        {
            if (CurHealth <= 0)
            {
                _creatureController.RemoveCreatureFromList(this);
                GameObject.FindGameObjectWithTag("DungeonManager").GetComponent<MessageLogDisplay>().AddMessage(DisplayName + " was defeated!");
                PlayerManager.Instance.GainXp(XpValue);
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

        public void MoveCreature()
        {
            int xDir = 0;
            int yDir = 0;

            if (Mathf.Abs(_target.position.x - transform.position.x) < Mathf.Epsilon)
                yDir = _target.position.y > transform.position.y ? 1 : -1;
            else
                xDir = _target.position.x > transform.position.x ? 1 : -1;

            AttemptMove<PlayerController>(xDir, yDir);
        }

        protected override void OnCantMove<T>(T component)
        {
            PlayerController hitPlayer = component as PlayerController;
            if (hitPlayer != null) hitPlayer.LoseHealth(Strength);
            _animator.SetTrigger("enemy1Attack");
            SoundManager.Instance.RandomizeSfx(EnemyAttack1, EnemyAttack2);
        }
    }
}