using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyDungeon.Demo
{
    public class MyPlayerOverworldController : PlayerOverworld
    {
        public Utilities.SceneField DungeonScene;
        
        private Animator _animator;
        private int _horizontal;
        private int _vertical;

        protected override void  Start()
        {
            _animator = GetComponent<Animator>();

            base.Start();
        }

        // Update is called once per frame
        void Update()
        {
            _horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
            _vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));

            if (_horizontal != 0 || _vertical != 0)
            {
                RaycastHit2D hit;
                CheckHit(transform.position, transform.position + new Vector3(_horizontal, _vertical, 0), out hit);
                SetAnimation(_horizontal, _vertical);

                if (hit.transform == null || 1 << hit.transform.gameObject.layer != BlockingLayer.value)
                {
                    transform.position += new Vector3(_horizontal, _vertical) * Time.deltaTime * 5;
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Interact();
            }
        }

        protected override void Interact()
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
                    DisplayName = MyPlayerManager.PlayerName,
                    MaxHealth = MyPlayerManager.MaxHealth
                };

                GetComponent<SaveMenu>().SaveGame(saveData, Application.persistentDataPath + "/save.sav");
            }
        }

        private void SetAnimation(int xDir, int yDir)
        {
            _animator.SetFloat("MoveX", xDir);
            _animator.SetFloat("MoveY", yDir);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.tag == "Dungeon")
            {
                SceneManager.LoadScene(DungeonScene.SceneName);
            }
        }
    }
}
