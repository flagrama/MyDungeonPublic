using MyDungeon.Managers;
using MyDungeon.Saving;
using MyDungeon.UI.Menu;
using MyDungeon._Demo.Managers;
using MyDungeon._Demo.Saving;
using UnityEngine;

namespace MyDungeon._Demo.UI.Menus
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
                GameManager.SaveLoaded = true;
                MyGameManager.Save = save;
            }

            base.LoadGame();
        }
    }
}
