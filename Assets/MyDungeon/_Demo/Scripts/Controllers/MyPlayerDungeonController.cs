﻿using UnityEngine;
using System.Collections;

namespace MyDungeon.Demo
{
    public class MyPlayerDungeonController : PlayerDungeonController
    {
        private GameObject _hudManager;
        private bool _collided;
        private GameObject _collision;
        private bool _diag;
        private bool _hold;
        private int _horizontal;
        private int _vertical;

        // Use this for initialization
        protected override void Start()
        {
            Animator = GetComponent<Animator>();

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
                StartAttack();
            if (_horizontal != 0 || _vertical != 0)
                AttemptMove<Creature>(_horizontal, _vertical);
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
                Animator.SetTrigger("playerMove");
            }
            if (!Moving)
            {
                SetAnimation(xDir, yDir);
            }
        }

        protected override IEnumerator Attack()
        {
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(Animator.GetFloat("MoveX"), Animator.GetFloat("MoveY"));
            RaycastHit2D hit;

            Moving = true;

            CheckHit(start, end, out hit);

            SoundManager.Instance.RandomizeSfx(SwingSound1, SwingSound2);
            Animator.SetTrigger("playerAttack");

            if (hit.transform == null)
            {
                GameManager.PlayersTurn = false;
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

            GameManager.PlayersTurn = false;
            yield return new WaitForSeconds(MoveTime);
            Moving = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Item")
            {
                _collided = true;
                _collision = collision.gameObject;
            }
        }

        private void UpdateHealthDisplay()
        {
            _hudManager.GetComponent<HealthDisplay>().UpdateHealth(CurHealth, MaxHealth);
            _hudManager.GetComponent<HealthBarDisplay>().UpdateHealthBar(CurHealth, MaxHealth);
        }

        public override void RecoverHealth(int recover)
        {
            UpdateHealthDisplay();

            base.RecoverHealth(recover);
        }

        public override void LoseHealth(int damage)
        {
            Animator.SetTrigger("playerHit");
            UpdateHealthDisplay();

            base.LoseHealth(damage);
        }

        protected override void CheckIfGameOver()
        {
            if(CurHealth <= 0)
            {
                SoundManager.Instance.PlaySingle(GameOverSound);
                SoundManager.Instance.MusicSource.Stop();
                GameObject.FindGameObjectWithTag("DungeonManager").GetComponent<MyInitGame>().GameOver();
            }

            base.CheckIfGameOver();
        }
    }
}