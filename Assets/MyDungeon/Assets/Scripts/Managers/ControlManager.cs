using UnityEngine;

namespace MyDungeon
{
    public class ControlManager : MonoBehaviour
    {
        public static ControlManager Instance;
        public static float MenuHorizontal;
        public static float MenuVertical;

        // Use this for initialization
        protected virtual void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }
    }
}
