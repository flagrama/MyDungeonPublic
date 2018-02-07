using UnityEngine;
using System.Collections;

namespace MyDungeon.Demo
{
    public class MainMenu : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            MenuManager.Instance.InitMainMenu();
        }

        // Update is called once per frame
        void OnDestroy()
        {
            MenuManager.Instance.DestroyMainMenu();
        }
    }
}
