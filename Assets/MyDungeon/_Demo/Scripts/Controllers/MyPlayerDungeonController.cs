using System.Collections;
using MyDungeon.Controllers;
using MyDungeon.Entities;
using MyDungeon.Items;
using MyDungeon.Managers;
using MyDungeon.UI.Hud;
using MyDungeon.Utilities;
using MyDungeon._Demo.Initialization;
using MyDungeon._Demo.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyDungeon._Demo.Controllers
{
    public class MyPlayerDungeonController : PlayerDungeonController
    {
        public AudioClip[] AttackHitSounds;
        public AudioClip[] AttackSwingSounds;
        public AudioClip[] MoveSounds;
        public AudioClip GameOverSound;
        public Utilities.SceneField TownScene;
        public Utilities.SceneField GameOverScene;


        private GameObject _hudManager;
        private Animator _animator;
        private bool _collided;
        private GameObject _collision;
        private bool _diag;
        private bool _hold;
        private int _horizontal;
        private int _vertical;

        // Use this for initialization
        protected override void Start()
        {
            _animator = GetComponent<Animator>();

            if (GameManager.SaveLoaded)
            {
                MyPlayerManager.Load(MyGameManager.Save);
                MyGameManager.Save = null;
                GameManager.SaveLoaded = false;
            }

            if (MyPlayerManager.Initialized == false)
            {
                MyPlayerManager.InitPlayer(DisplayName, MaxHealth);
                MyPlayerManager.Initialized = true;
            }

            CurHealth = MyPlayerManager.CurrentHealth;
            _hudManager = GameObject.FindGameObjectWithTag("HudManager");

            _hudManager.GetComponent<LevelDisplay>().UpdateLevel(MyPlayerManager.Level);
            UpdateHealthDisplay();

            base.Start();
        }

        private void OnDisable()
        {
            MyPlayerManager.CurrentHealth = CurHealth;
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (!GameManager.PlayersTurn || GameManager.Paused)
                return;

            _horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
            _vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
            _diag = Input.GetButton("Diagonal Lock");
            _hold = Input.GetButton("Fire3");
            if (_collided && _collision != null && !Moving)
            {
                if (_collision.CompareTag("Item") && Mathf.Approximately(transform.position.x, _collision.transform.position.x)
                    && Mathf.Approximately(transform.position.y, _collision.transform.position.y))
                {
                    Item item = _collision.GetComponent<ItemBehaviour>().Item;
                    GetComponent<Inventory>().AddItem(item);
                    _collision.SetActive(false);
                }

                _collision = null;
                _collided = false;
            }
            if (Input.GetButtonDown("Fire1") && !Moving)
                StartCoroutine(Attack());
            if (_horizontal != 0 || _vertical != 0)
                AttemptMove(_horizontal, _vertical);
        }
        protected override bool Move(int xDir, int yDir, out RaycastHit2D hit)
        {
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(xDir, yDir);

            CheckHit(start, end, out hit);

            if (PosX + xDir < 0 || PosX + xDir > DungeonManager.DungeonGenerationSettings.Columns || PosY + yDir < 0 || PosY + yDir > DungeonManager.DungeonGenerationSettings.Rows)
            {
                return false;
            }

            if (hit || Moving) return false;

            PosX += xDir;
            PosY += yDir;
            StartCoroutine(SmoothMovement(end));
            return true;
        }

        protected override void AttemptMove(int xDir, int yDir)
        {
            int x = PosX;
            int y = PosY;

            if (_hold)
            {
                SetAnimation(xDir, yDir);
                return;
            }

            if (_diag)
            {
                if (xDir == 0 || yDir == 0)
                    return;
            }

            base.AttemptMove(xDir, yDir);

            if (PosX != x || PosY != y)
            {
                SoundManager.Instance.RandomizeSfx(MoveSounds);
                SetAnimation(xDir, yDir);
                _animator.SetTrigger("playerMove");
            }
            if (!Moving)
            {
                SetAnimation(xDir, yDir);
            }
        }

        protected override IEnumerator Attack()
        {
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(_animator.GetFloat("MoveX"), _animator.GetFloat("MoveY"));
            RaycastHit2D hit;

            Moving = true;

            CheckHit(start, end, out hit);

            SoundManager.Instance.RandomizeSfx(AttackSwingSounds);
            _animator.SetTrigger("playerAttack");

            if (hit.transform == null)
            {
                GameManager.PlayersTurn = false;
                yield return new WaitForSeconds(MoveTime);
                Moving = false;
                yield break;
            }

            if (hit.transform.CompareTag("Enemy"))
            {
                Creature hitCreature = hit.transform.GetComponent<Creature>();
                SoundManager.Instance.RandomizeSfx(AttackHitSounds);
                hitCreature.LoseHealth(Strength);
            }

            GameManager.PlayersTurn = false;
            yield return new WaitForSeconds(MoveTime);
            Moving = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Item"))
            {
                _collided = true;
                _collision = collision.gameObject;
            }
        }

        private void SetAnimation(int xDir, int yDir)
        {
            _animator.SetFloat("MoveX", xDir);
            _animator.SetFloat("MoveY", yDir);
        }

        private void UpdateHealthDisplay()
        {
            _hudManager.GetComponent<HealthDisplay>().UpdateHealth(CurHealth, MaxHealth);
            _hudManager.GetComponent<HealthBarDisplay>().UpdateHealthBar(CurHealth, MaxHealth);
        }

        public override void RecoverHealth(int recover)
        {
            base.RecoverHealth(recover);

            UpdateHealthDisplay();
        }

        public override void LoseHealth(int damage)
        {
            base.LoseHealth(damage);

            _animator.SetTrigger("playerHit");
            UpdateHealthDisplay();
        }

        protected override void CheckIfGameOver()
        {
            if(CurHealth <= 0)
            {
                SoundManager.Instance.PlaySingle(GameOverSound);
                SoundManager.Instance.MusicSource.Stop();
                GameObject.FindGameObjectWithTag("DungeonManager").GetComponent<MyInitGame>().GameOver();
                this.Invoke(EndGame, 2f);
            }

            base.CheckIfGameOver();
        }

        protected virtual void Restart()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }

        protected virtual void EndGame()
        {
            PlayerManager.Instance.Reset();
            GameManager.Reset();
            SoundManager.Instance.MusicSource.Play();
            SceneManager.LoadScene(GameOverScene.SceneName);
        }
    }
}
