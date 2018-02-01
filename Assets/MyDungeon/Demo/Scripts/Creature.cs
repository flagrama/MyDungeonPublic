using UnityEngine;

namespace MyDungeon.Demo
{
    public class Creature : MovingObject
    {
        public int XpValue;
        public AudioClip enemyAttack1;
        public AudioClip enemyAttack2;

        Transform target;
        Animator animator;
        bool skipMove;

        // Use this for initialization
        protected override void Start()
        {
            GameManager.instance.AddCreatureToList(this);
            animator = GetComponent<Animator>();
            target = GameObject.FindGameObjectWithTag("Player").transform;
            curHealth = maxHealth;
            base.Start();
        }

        private void Update()
        {
            if (curHealth <= 0)
            {
                GameManager.instance.RemoveCreatureFromList(this);
                GameManager.instance.board[posX, posY] = GridGenerator.TileType.Floor;
                HudManager.instance.AddMessage(displayName + " was defeated!");
                PlayerManager.instance.GainXp(XpValue);
                Destroy(gameObject);
            }
        }

        protected override void AttemptMove<T>(int xDir, int yDir)
        {
            if (skipMove)
            {
                skipMove = false;
                return;
            }

            base.AttemptMove<T>(xDir, yDir);
            skipMove = true;
        }

        public void MoveCreature()
        {
            int xDir = 0;
            int yDir = 0;

            if (Mathf.Abs(target.position.x - transform.position.x) < Mathf.Epsilon)
                yDir = target.position.y > transform.position.y ? 1 : -1;
            else
                xDir = target.position.x > transform.position.x ? 1 : -1;

            AttemptMove<Player>(xDir, yDir);
        }

        protected override void OnCantMove<T>(T Component)
        {
            Player hitPlayer = Component as Player;
            hitPlayer.LoseHealth(strength);
            animator.SetTrigger("enemy1Attack");
            SoundManager.instance.RandomizeSfx(enemyAttack1, enemyAttack2);
        }
    }
}
