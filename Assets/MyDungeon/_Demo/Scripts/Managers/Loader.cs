using UnityEngine;

namespace MyDungeon.Demo
{
    public class Loader : MonoBehaviour
    {
        public GameObject GameManager;
        public GameObject PlayerManager;
        public GameObject SoundManager;

        private void Awake()
        {
            if (Demo.SoundManager.Instance == null)
            {
                Instantiate(SoundManager);
            }

            if (Demo.PlayerManager.Instance == null)
            {
                Instantiate(PlayerManager);
            }

            if (Demo.GameManager.Instance == null)
            {
                Instantiate(GameManager);
            }
        }
    }
}