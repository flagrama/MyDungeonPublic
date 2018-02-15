using UnityEngine;

namespace MyDungeon._Demo.Managers
{
    public class Loader : MonoBehaviour
    {
        public GameObject ControlManager;
        public GameObject PlayerManager;
        public GameObject SoundManager;

        private void Awake()
        {
            if (MyDungeon.Managers.ControlManager.Instance == null)
            {
                Instantiate(ControlManager);
            }

            if (MyDungeon.Managers.SoundManager.Instance == null)
            {
                Instantiate(SoundManager);
            }

            if (MyDungeon.Managers.PlayerManager.Instance == null)
            {
                Instantiate(PlayerManager);
            }
        }
    }
}