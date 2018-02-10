using UnityEngine;
using MyDungeon.Demo;
using UnityEngine.SceneManagement;

namespace MyDungeon.Demo
{
    public class MyPauseMenu : MyDungeon.Demo.PauseMenu
    {
        private GameObject _menu;

        protected override void Pause()
        {
            base.Pause();


            if (SceneManager.GetActiveScene().name != "Town")
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
