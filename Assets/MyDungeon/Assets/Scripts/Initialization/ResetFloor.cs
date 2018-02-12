using UnityEngine;

namespace MyDungeon
{
    public class ResetFloor : MonoBehaviour
    {

        // Use this for initialization
        protected virtual void Start()
        {
            GameManager.Floor = 0;
        }
    }
}
