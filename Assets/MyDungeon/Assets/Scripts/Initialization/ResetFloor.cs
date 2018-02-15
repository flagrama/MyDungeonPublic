using MyDungeon.Managers;
using UnityEngine;

namespace MyDungeon.Initialization
{
    /// <summary>
    /// ResetFloor resets the current floor in GameManager to 0
    /// </summary>
    public class ResetFloor : MonoBehaviour
    {

        /// <summary>
        /// Resets the current floor in GameManager to 0
        /// </summary>
        protected virtual void Start()
        {
            GameManager.Floor = 0;
        }
    }
}
