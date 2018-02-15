namespace MyDungeon
{
    using UnityEngine;

    /// <summary>
    /// ControlManager is used to check if directional input is recieved while in a menu
    /// </summary>
    public class ControlManager : MonoBehaviour
    {
        /// <summary>
        /// The current ControlManager instance
        /// </summary>
        public static ControlManager Instance;
        /// <summary>
        /// The Horizontal value while in a menu
        /// </summary>
        public static float MenuHorizontal;
        /// <summary>
        /// The Vertical value while in a menu
        /// </summary>
        public static float MenuVertical;

        /// <summary>
        /// Initializes the ControlManager
        /// </summary>
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
