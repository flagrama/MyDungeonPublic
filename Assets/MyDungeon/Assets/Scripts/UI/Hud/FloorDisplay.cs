using UnityEngine;
using System.Collections;

namespace MyDungeon
{
    public class FloorDisplay : MonoBehaviour
    {
        private UnityEngine.UI.Text _floorText;

        // Use this for initialization
        void Start()
        {
            _floorText = GameObject.FindGameObjectWithTag("FloorText").GetComponent<UnityEngine.UI.Text>();
        }

        public void UpdateFloor(int floor)
        {
            _floorText.text = string.Format("{0,3}F", floor);
        }
    }
}
