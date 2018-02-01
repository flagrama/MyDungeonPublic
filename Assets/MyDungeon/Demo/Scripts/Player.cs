using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyDungeon.Demo
{
    public class Player : MovingObject
    {

        public float restartLevelDelay = 1f;
        public AudioClip moveSound1;
        public AudioClip moveSound2;
        public AudioClip chopSound1;
        public AudioClip chopSound2;
        public AudioClip swingSound1;
        public AudioClip swingSound2;
        public AudioClip gameOverSound;

        private Animator animator;
        private int horizontal;
        private int vertical;
        private bool hold;
        private bool diag;

        // Use this for initialization
        protected override void Start()
        {
            if (!MenuManager.instance.inMainMenu)
            {
                animator = GetComponent<Animator>();

                if (PlayerManager.instance.initialized == false)
                {
                    PlayerManager.instance.InitPlayer(displayName, maxHealth);
                    PlayerManager.instance.initialized = true;
                }

                curHealth = PlayerManager.instance.curHealth;
                HudManager.instance.UpdateLevel(PlayerManager.instance.level);
                UpdateHealth();
                base.Start();
            }
        }

        void OnDisable()
        {
            PlayerManager.instance.curHealth = curHealth;
        }

        // Update is called once per frame
        void Update()
        {
            if (!GameManager.instance.playersTurn || GameManager.instance.paused || MenuManager.instance.inMainMenu)
                return;

            horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
            vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
            if (SceneManager.GetActiveScene().name == "Dungeon")
            {
                diag = Input.GetButton("Diagonal Lock");
                hold = Input.GetButton("Fire3");
                if (Input.GetButtonDown("Fire1") && !moving)
                    StartCoroutine(Attack());
                if (horizontal != 0 || vertical != 0)
                    AttemptMove<Creature>(horizontal, vertical);
            }

            if (SceneManager.GetActiveScene().name == "Town")
            {
                if (horizontal != 0 || vertical != 0)
                {
                    transform.GetComponent<Rigidbody2D>().isKinematic = false;
                    transform.position += new Vector3(horizontal, vertical) * Time.deltaTime * 5;
                    SetAnimation(horizontal, vertical);
                }
                if (Input.GetButtonDown("Fire1"))
                {
                    Interact();
                }
            }

            enabled = !MenuManager.instance.inMenu;
        }

        protected override void AttemptMove<T>(int xDir, int yDir)
        {
            int x = posX;
            int y = posY;

            if (hold)
            {
                SetAnimation(xDir, yDir);
                return;
            }

            if (diag)
            {
                if (xDir == 0 || yDir == 0)
                    return;
            }

            base.AttemptMove<T>(xDir, yDir);

            if (posX != x || posY != y)
            {
                SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
                SetAnimation(xDir, yDir);
                animator.SetTrigger("playerMove");
                GameManager.instance.playersTurn = false;
            }
            if (!moving)
            {
                SetAnimation(xDir, yDir);
            }
        }

        protected override void OnCantMove<T>(T component)
        {
        }

        private void SetAnimation(int xDir, int yDir)
        {
            animator.SetFloat("MoveX", xDir);
            animator.SetFloat("MoveY", yDir);
        }

        private IEnumerator Attack()
        {
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(animator.GetFloat("MoveX"), animator.GetFloat("MoveY"));
            RaycastHit2D hit;

            moving = true;

            CheckHit(start, end, out hit);

            SoundManager.instance.RandomizeSfx(swingSound1, swingSound2);
            animator.SetTrigger("playerAttack");

            if (hit.transform == null)
            {
                GameManager.instance.playersTurn = false;
                yield return new WaitForSeconds(moveTime);
                moving = false;
                yield break;
            }

            if (hit.transform.tag == "Enemy")
            {
                Creature hitCreature = hit.transform.GetComponent<Creature>();
                SoundManager.instance.RandomizeSfx(chopSound1, chopSound2);
                hitCreature.LoseHealth(strength);
            }

            GameManager.instance.playersTurn = false;
            yield return new WaitForSeconds(moveTime);
            moving = false;
        }

        private void Interact()
        {
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(animator.GetFloat("MoveX"), animator.GetFloat("MoveY"));
            RaycastHit2D hit;

            CheckHit(start, end, out hit);

            if (hit.transform == null)
            {
                return;
            }

            if (hit.transform.tag == "SaveNPC")
            {
                GameManager.instance.SaveGame();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Exit"
                && (int) Mathf.Round(collision.gameObject.transform.position.x) ==
                (int) Mathf.Round(transform.position.x)
                && (int) Mathf.Round(collision.gameObject.transform.position.y) ==
                (int) Mathf.Round(transform.position.y))
            {
                enabled = false;
                MenuManager.instance.ContinueOrExitMenu();
            }

            if (collision.tag == "Item"
                && (int) Mathf.Round(collision.gameObject.transform.position.x) ==
                (int) Mathf.Round(transform.position.x)
                && (int) Mathf.Round(collision.gameObject.transform.position.y) ==
                (int) Mathf.Round(transform.position.y))
            {
                Item item = collision.gameObject.GetComponent<ItemBehaviour>().item;
                PlayerManager.instance.AddItem(item);
                collision.gameObject.SetActive(false);
            }

            if (collision.tag == "Dungeon")
            {
                SceneManager.LoadScene("Dungeon");
            }
        }

        public void Continue()
        {
            Invoke("Restart", restartLevelDelay);
        }

        private void Restart()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }

        public override void LoseHealth(int damage)
        {
            base.LoseHealth(damage);
            animator.SetTrigger("playerHit");
            UpdateHealth();
        }

        public void RecoverHealth(int recover)
        {
            curHealth += recover;
            UpdateHealth();
            HudManager.instance.AddMessage(displayName + " recovered " + recover + " health");
        }

        void CheckIfGameOver()
        {
            if (curHealth <= 0)
            {
                SoundManager.instance.PlaySingle(gameOverSound);
                SoundManager.instance.musicSource.Stop();
                GameManager.instance.GameOver();
            }
        }

        void UpdateHealth()
        {
            if (curHealth > maxHealth)
                curHealth = maxHealth;
            if (curHealth < 0)
                curHealth = 0;

            HudManager.instance.UpdateHealth(curHealth, maxHealth);
            CheckIfGameOver();
        }
    }
}
