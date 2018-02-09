using UnityEngine;
using UnityEngine.EventSystems;

namespace MyDungeon.Demo
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject PauseMenuPrefab;
        public GameObject FirstSelectedGameObject;
        protected EventSystem EventSystem;
        protected GameObject LastSelected;

        // Use this for initialization
        void Awake()
        {
            EventSystem = EventSystem.current;
        }

        // Update is called once per frame
        void Update()
        {
            int horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
            int vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
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

            if (Input.GetButtonDown("Cancel") && GameManager.Instance.PlayersTurn)
            {
                PauseGame();
            }
        }

        public void PauseGame()
        {
            Pause();
        }

        protected virtual void Pause()
        {
            GameManager.Instance.Paused = !GameManager.Instance.Paused;

            Time.timeScale = Mathf.Approximately(Time.timeScale, 0f) ? 1 : 0;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled =
                !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled;
        }
    }
}
