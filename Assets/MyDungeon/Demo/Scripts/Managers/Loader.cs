using UnityEngine;

namespace MyDungeon.Demo
{
    public class Loader : MonoBehaviour
    {
        public GameObject GameManager;
        public GameObject HudManager;
        public GameObject MenuManager;
        public GameObject PlayerManager;
        public GameObject SoundManager;

        private void Awake()
        {
            if (Demo.SoundManager.Instance == null)
            {
                Instantiate(SoundManager);
            }

            if (Demo.HudManager.Instance == null)
            {
                Instantiate(HudManager);
            }

            if (Demo.PlayerManager.Instance == null)
            {
                Instantiate(PlayerManager);
            }

            if (Demo.MenuManager.Instance == null)
            {
                Instantiate(MenuManager);
            }

            if (Demo.GameManager.Instance == null)
            {
                Instantiate(GameManager);
            }
        }
    }
}