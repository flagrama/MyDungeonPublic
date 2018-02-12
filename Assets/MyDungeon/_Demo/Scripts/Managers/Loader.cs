using UnityEngine;

namespace MyDungeon.Demo
{
    public class Loader : MonoBehaviour
    {
        public GameObject ControlManager;
        public GameObject PlayerManager;
        public GameObject SoundManager;

        private void Awake()
        {
            if (MyDungeon.ControlManager.Instance == null)
            {
                Instantiate(ControlManager);
            }

            if (MyDungeon.SoundManager.Instance == null)
            {
                Instantiate(SoundManager);
            }

            if (MyDungeon.PlayerManager.Instance == null)
            {
                Instantiate(PlayerManager);
            }
        }
    }
}