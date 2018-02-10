using UnityEngine;
using UnityEngine.EventSystems;

namespace MyDungeon
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject PauseMenuPrefab;
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

        protected virtual void Pause()
        {
            GameManager.Paused = !GameManager.Paused;

            Time.timeScale = Mathf.Approximately(Time.timeScale, 0f) ? 1 : 0;
        }
    }
}
