using UnityEngine;
using System.Collections;

namespace MyDungeon.Demo
{
    public class ResetFloor : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            GameManager.Instance.Floor = 0;
        }
    }
}
