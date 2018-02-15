using UnityEngine;

namespace MyDungeon.UI.Hud
{
    public class LevelDisplay : MonoBehaviour
    {
        protected UnityEngine.UI.Text LevelText;

        // Use this for initialization
        protected virtual void Start()
        {
            LevelText = GameObject.FindGameObjectWithTag("LevelText").GetComponent<UnityEngine.UI.Text>();
        }

        public virtual void UpdateLevel(int level)
        {
            LevelText.text = string.Format("Lv{0,3}", level);
        }
    }
}
