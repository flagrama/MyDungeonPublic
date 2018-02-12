using System.Collections;
using MyDungeon.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyDungeon
{
    public class PlayerDungeonController : PlayerDungeon
    {
        public AudioClip ChopSound1;
        public AudioClip ChopSound2;
        public AudioClip GameOverSound;
        public AudioClip MoveSound1;
        public AudioClip MoveSound2;
        public float RestartLevelDelay = 1f;
        public AudioClip SwingSound1;
        public AudioClip SwingSound2;
        public SceneField TownScene;

        // Use this for initialization
        protected override void Start()
        {
            Animator = GetComponent<Animator>();

            UpdateHealth();

            base.Start();
        }

        protected virtual void StartAttack()
        {
            StartCoroutine(Attack());
        }

        protected override void AttemptMove<T>(int xDir, int yDir)
        {
            int x = PosX;
            int y = PosY;

            base.AttemptMove<T>(xDir, yDir);

            if (PosX != x || PosY != y)
            {
                GameManager.PlayersTurn = false;
            }
        }

        protected override void OnCantMove<T>(T component)
        {
        }

        protected virtual IEnumerator Attack()
        {
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(Animator.GetFloat("MoveX"), Animator.GetFloat("MoveY"));
            RaycastHit2D hit;

            Moving = true;

            CheckHit(start, end, out hit);

            if (hit.transform == null)
            {
                GameManager.PlayersTurn = false;
                yield return new WaitForSeconds(MoveTime);
                Moving = false;
                yield break;
            }

            GameManager.PlayersTurn = false;
            yield return new WaitForSeconds(MoveTime);
            Moving = false;
        }

        protected virtual void Restart()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }

        public override void LoseHealth(int damage)
        {
            UpdateHealth();

            base.LoseHealth(damage);
        }

        public override void RecoverHealth(int recover)
        {
            UpdateHealth();

            base.RecoverHealth(recover);
        }

        protected virtual void CheckIfGameOver()
        {
            if (CurHealth <= 0) enabled = false;
        }

        protected virtual void UpdateHealth()
        {
            if (CurHealth > MaxHealth)
                CurHealth = MaxHealth;
            if (CurHealth < 0)
                CurHealth = 0;

            CheckIfGameOver();
        }
    }
}