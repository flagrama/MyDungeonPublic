using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

namespace MyDungeon.Demo
{
    public class MyMainMenu : MainMenu
    {

        // Use this for initialization
        protected override void Start()
        {
            base.Start();
#if UNITY_WEBGL
            GameObject.Find("QuitButton").SetActive(false);
#endif
        }

        public override void LoadGame()
        {
            string path = Application.persistentDataPath + "/save.sav";
            MySaveData save = gameObject.GetComponent<SaveLoad>().Load<MySaveData>(path);

            if (save != null)
            {
                MyDungeon.GameManager.SaveLoaded = true;
                MyGameManager.Save = save;
            }

            base.LoadGame();
        }
    }
}
