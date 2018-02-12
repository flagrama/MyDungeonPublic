using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace MyDungeon.Demo
{
    public class MyPlayerOverworldController : PlayerOverworldController
    {
        private int _horizontal;
        private int _vertical;

        // Update is called once per frame
        void Update()
        {
            _horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
            _vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));

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

        private void Interact()
        {
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(Animator.GetFloat("MoveX"), Animator.GetFloat("MoveY"));
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

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.tag == "Dungeon")
            {
                SceneManager.LoadScene(DungeonScene.SceneName);
            }
        }
    }
}
