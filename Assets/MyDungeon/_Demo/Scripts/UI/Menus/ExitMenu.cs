using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace MyDungeon.Demo
{
    public class ExitMenu : MonoBehaviour
    {
        public GameObject ExitMenuPrefab;
        public Utilities.SceneField ExitScene;
        public GameObject FirstSelectedGameObject;
        protected EventSystem EventSystem;
        protected GameObject LastSelected;
        private bool _collided;
        private GameObject _collision;

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

            if (_collided && Mathf.Approximately(transform.position.x, _collision.transform.position.x)
                && Mathf.Approximately(transform.position.y, _collision.transform.position.y))
            {
                _collision.GetComponent<PlayerController>().enabled = false;
                ShowMenu();
                _collided = false;
            }
        }

        protected virtual void ShowMenu()
        {
            Instantiate(ExitMenuPrefab);
            GameObject firstSelected = GameObject.Find(FirstSelectedGameObject.name);
            EventSystem = EventSystem.current;
            EventSystem.firstSelectedGameObject = firstSelected;
            EventSystem.SetSelectedGameObject(firstSelected);
            LastSelected = EventSystem.firstSelectedGameObject;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                _collided = true;
                _collision = collision.gameObject;
            }
        }

        public void Continue()
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Continue();
        }

        public void Exit()
        {
            SceneManager.LoadScene(ExitScene.SceneName);
        }
    }
}
