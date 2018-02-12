using UnityEngine;

namespace MyDungeon.Demo
{
    public class MyCreature : Creature
    {
        private Animator _animator;
        private bool _skipMove;
        private Transform _target;

        protected override void Start()
        {
            base.Start();
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            _animator = GetComponent<Animator>();
        }

        protected override void Update()
        {
            base.Update();

            if (CurHealth <= 0)
            {
                GameObject.FindGameObjectWithTag("HudManager").GetComponent<MessageLogDisplay>().AddMessage(DisplayName + " was defeated!");
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
            PlayerDungeon hitPlayerDungeon = component as PlayerDungeon;
            if (hitPlayerDungeon != null) hitPlayerDungeon.LoseHealth(Strength);
            SoundManager.Instance.RandomizeSfx(EnemyAttack1, EnemyAttack2);
            _animator.SetTrigger("enemy1Attack");
        }

        public override void MoveCreature()
        {
            int xDir = 0;
            int yDir = 0;

            if (Mathf.Abs(_target.position.x - transform.position.x) < Mathf.Epsilon)
                yDir = _target.position.y > transform.position.y ? 1 : -1;
            else
                xDir = _target.position.x > transform.position.x ? 1 : -1;

            AttemptMove<PlayerDungeon>(xDir, yDir);
        }

        public override void LoseHealth(int damage)
        {
            base.LoseHealth(damage);
            GameObject.FindGameObjectWithTag("HudManager").GetComponent<MessageLogDisplay>().AddMessage(DisplayName + " took " + damage + " damage");
        }
    }
}
