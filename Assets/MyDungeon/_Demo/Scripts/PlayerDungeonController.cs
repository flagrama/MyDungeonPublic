using System.Collections;
using MyDungeon.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyDungeon.Demo
{
    public class PlayerDungeonController : MyDungeon.Player
    {
        private Animator _animator;
        private bool _collided;
        private GameObject _collision;
        private bool _diag;
        private bool _hold;
        private int _horizontal;
        private GameObject _hudManager;
        private int _vertical;
        public AudioClip ChopSound1;
        public AudioClip ChopSound2;
        public AudioClip GameOverSound;
        public AudioClip MoveSound1;
        public AudioClip MoveSound2;
        public float RestartLevelDelay = 1f;
        public AudioClip SwingSound1;
        public AudioClip SwingSound2;
        public SceneField DungeonScene;
        public SceneField TownScene;

        // Use this for initialization
        protected override void Start()
        {
            _animator = GetComponent<Animator>();

            if (MyDungeon.GameManager.SaveLoaded)
            {
                PlayerManager.Instance.Load(MyGameManager.Save);
                MyGameManager.Save = null;
                MyDungeon.GameManager.SaveLoaded = false;
            }

            if (PlayerManager.Instance.Initialized == false)
            {
                PlayerManager.Instance.InitPlayer(DisplayName, MaxHealth);
                PlayerManager.Instance.Initialized = true;
            }

            CurHealth = PlayerManager.Instance.CurHealth;
            UpdateHealth();

            if (SceneManager.GetActiveScene().name == DungeonScene.SceneName)
            {
                _hudManager = GameObject.FindGameObjectWithTag("HudManager");
                
                _hudManager.GetComponent<LevelDisplay>().UpdateLevel(PlayerManager.Instance.Level);
                UpdateHealthDisplay();
            }

            base.Start();
        }

        private void OnDisable()
        {
            PlayerManager.Instance.CurHealth = CurHealth;
        }

        // Update is called once per frame
        private void Update()
        {
            if (!MyDungeon.GameManager.PlayersTurn || MyDungeon.GameManager.Paused)
                return;

            _horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
            _vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
            if (SceneManager.GetActiveScene().name == DungeonScene.SceneName)
            {
                _diag = Input.GetButton("Diagonal Lock");
                _hold = Input.GetButton("Fire3");
                if (_collided && _collision != null && !Moving)
                {
                    if (_collision.tag == "Item" && Mathf.Approximately(transform.position.x, _collision.transform.position.x)
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
                    AttemptMove<Creature>(_horizontal, _vertical);
            }

            if (SceneManager.GetActiveScene().name == TownScene.SceneName)
            {
                if (_horizontal != 0 || _vertical != 0)
                {
                    transform.GetComponent<Rigidbody2D>().isKinematic = false;
                    transform.position += new Vector3(_horizontal, _vertical) * Time.deltaTime * 5;
                    SetAnimation(_horizontal, _vertical);
                }
                if (Input.GetButtonDown("Fire1"))
                {
                    Interact();
                }
            }
        }

        protected override void AttemptMove<T>(int xDir, int yDir)
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

            base.AttemptMove<T>(xDir, yDir);

            if (PosX != x || PosY != y)
            {
                SoundManager.Instance.RandomizeSfx(MoveSound1, MoveSound2);
                SetAnimation(xDir, yDir);
                _animator.SetTrigger("playerMove");
                MyDungeon.GameManager.PlayersTurn = false;
            }
            if (!Moving)
            {
                SetAnimation(xDir, yDir);
            }
        }

        protected override void OnCantMove<T>(T component)
        {
        }

        private void SetAnimation(int xDir, int yDir)
        {
            _animator.SetFloat("MoveX", xDir);
            _animator.SetFloat("MoveY", yDir);
        }

        private IEnumerator Attack()
        {
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(_animator.GetFloat("MoveX"), _animator.GetFloat("MoveY"));
            RaycastHit2D hit;

            Moving = true;

            CheckHit(start, end, out hit);

            SoundManager.Instance.RandomizeSfx(SwingSound1, SwingSound2);
            _animator.SetTrigger("playerAttack");

            if (hit.transform == null)
            {
                MyDungeon.GameManager.PlayersTurn = false;
                yield return new WaitForSeconds(MoveTime);
                Moving = false;
                yield break;
            }

            if (hit.transform.tag == "Enemy")
            {
                Creature hitCreature = hit.transform.GetComponent<Creature>();
                SoundManager.Instance.RandomizeSfx(ChopSound1, ChopSound2);
                hitCreature.LoseHealth(Strength);
            }

            MyDungeon.GameManager.PlayersTurn = false;
            yield return new WaitForSeconds(MoveTime);
            Moving = false;
        }

        private void Interact()
        {
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(_animator.GetFloat("MoveX"), _animator.GetFloat("MoveY"));
            RaycastHit2D hit;

            CheckHit(start, end, out hit);

            if (hit.transform == null)
            {
                return;
            }

            if (hit.transform.tag == "SaveNPC")
            {
                MySaveData saveData = new MySaveData
                {
                    Inventory = GetComponent<Inventory>().InventoryItems,
                    DisplayName = PlayerManager.Instance.PlayerName,
                    MaxHealth = PlayerManager.Instance.MaxHealth
                };

                GetComponent<SaveMenu>().SaveGame(saveData, Application.persistentDataPath + "/save.sav");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Item")
            {
                _collided = true;
                _collision = collision.gameObject;
            }

            if (collision.tag == "Dungeon")
            {
                SceneManager.LoadScene(DungeonScene.SceneName);
            }
        }

        private void Restart()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }

        public override void LoseHealth(int damage)
        {
            base.LoseHealth(damage);
            _animator.SetTrigger("playerHit");
            UpdateHealth();
            UpdateHealthDisplay();
        }

        public void RecoverHealth(int recover)
        {
            CurHealth += recover;
            UpdateHealth();
            UpdateHealthDisplay();
        }

        private void CheckIfGameOver()
        {
            if (CurHealth <= 0)
            {
                enabled = false;
                SoundManager.Instance.PlaySingle(GameOverSound);
                SoundManager.Instance.MusicSource.Stop();
                GameObject.FindGameObjectWithTag("DungeonManager").GetComponent<MyInitGame>().GameOver();
            }
        }

        private void UpdateHealth()
        {
            if (CurHealth > MaxHealth)
                CurHealth = MaxHealth;
            if (CurHealth < 0)
                CurHealth = 0;

            CheckIfGameOver();
        }

        private void UpdateHealthDisplay()
        {
            _hudManager.GetComponent<HealthDisplay>().UpdateHealth(CurHealth, MaxHealth);
            _hudManager.GetComponent<HealthBarDisplay>().UpdateHealthBar(CurHealth, MaxHealth);
        }
    }
}