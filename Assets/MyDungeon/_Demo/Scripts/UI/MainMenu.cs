using UnityEngine;
using UnityEngine.EventSystems;

namespace MyDungeon.Demo
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject MainMenuPrefab;
        public GameObject FirstSelectedGameObject;
        protected EventSystem EventSystem;
        protected GameObject LastSelected;

        // Use this for initialization
        void Awake()
        {
            Instantiate(MainMenuPrefab);
            GameObject firstSelected = GameObject.Find(FirstSelectedGameObject.name);
            EventSystem = EventSystem.current;
            EventSystem.firstSelectedGameObject = firstSelected;
            LastSelected = EventSystem.firstSelectedGameObject;
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
        }
    }
}
