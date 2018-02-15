using MyDungeon.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace MyDungeon.UI.Menu
{
    public class ExitMenu : MonoBehaviour
    {
        public GameObject ExitMenuPrefab;
        public Utilities.SceneField ExitScene;
        public Utilities.SceneField ContinueScene;
        protected EventSystem EventSystem;
        protected GameObject LastSelected;

        // Use this for initialization
        protected virtual void Start()
        {
            EventSystem = EventSystem.current;
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            int horizontal = Mathf.RoundToInt(ControlManager.MenuHorizontal);
            int vertical = Mathf.RoundToInt(ControlManager.MenuVertical);
            if (horizontal != 0 || vertical != 0)
            {
                if (EventSystem.currentSelectedGameObject != LastSelected)
                {
                    if (EventSystem.currentSelectedGameObject == null)
                        EventSystem.SetSelectedGameObject(LastSelected);
                    else
                        LastSelected = EventSystem.currentSelectedGameObject;
                }
            }
        }

        protected virtual void ShowMenu()
        {
            Instantiate(ExitMenuPrefab);
            GameObject firstSelected = GameObject.FindGameObjectWithTag("UIFirstSelected");
            EventSystem = EventSystem.current;
            EventSystem.firstSelectedGameObject = firstSelected;
            EventSystem.SetSelectedGameObject(firstSelected);
            LastSelected = EventSystem.firstSelectedGameObject;
        }

        public virtual void Continue()
        {
            SceneManager.LoadSceneAsync(ContinueScene.SceneName, LoadSceneMode.Single);
        }

        public virtual void Exit()
        {
            SceneManager.LoadScene(ExitScene.SceneName);
        }
    }
}
