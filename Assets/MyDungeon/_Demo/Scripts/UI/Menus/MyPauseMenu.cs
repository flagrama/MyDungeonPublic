using UnityEngine;
using MyDungeon.Utilities;
using UnityEngine.SceneManagement;

namespace MyDungeon.Demo
{
    public class MyPauseMenu : PauseMenu
    {
        public SceneField NoInventoryScene;
        private GameObject _menu;

        protected override void Update()
        {
            if (Input.GetButtonDown("Cancel") && GameManager.PlayersTurn)
            {
                PauseGame();
            }
        }

        public virtual void PauseGame()
        {
            Pause();
        }

        protected override void Pause()
        {
            base.Pause();
            GameObject.FindGameObjectWithTag("Player").GetComponent<MyPlayerDungeonController>().enabled =
                !GameObject.FindGameObjectWithTag("Player").GetComponent<MyPlayerDungeonController>().enabled;


            if (SceneManager.GetActiveScene().name != NoInventoryScene.SceneName)
            {
                if (GameManager.Paused)
                {
                    _menu = Instantiate(PauseMenuPrefab);
                    GameObject firstSelected = GameObject.FindGameObjectWithTag("UIFirstSelected");
                    EventSystem.firstSelectedGameObject = firstSelected;
                    EventSystem.SetSelectedGameObject(firstSelected);
                    LastSelected = EventSystem.firstSelectedGameObject;

                    ScrollView sv = GameObject.Find("ScrollView").GetComponent<ScrollView>();
                    sv.Populate();
                }
                else
                {
                    Destroy(_menu);
                }
            }
        }
    }
}
