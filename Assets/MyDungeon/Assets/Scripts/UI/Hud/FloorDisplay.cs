using UnityEngine;

namespace MyDungeon.UI.Hud
{
    public class FloorDisplay : MonoBehaviour
    {
        protected UnityEngine.UI.Text FloorText;

        // Use this for initialization
        protected virtual void Start()
        {
            FloorText = GameObject.FindGameObjectWithTag("FloorText").GetComponent<UnityEngine.UI.Text>();
        }

        public virtual void UpdateFloor(int floor)
        {
            FloorText.text = string.Format("{0,3}F", floor);
        }
    }
}
