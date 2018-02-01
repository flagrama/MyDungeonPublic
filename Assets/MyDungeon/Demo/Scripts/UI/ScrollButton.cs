using UnityEngine;
using UnityEngine.UI;

namespace MyDungeon.Demo
{
    public class ScrollButton : MonoBehaviour
    {
        private int _index;
        private string _name;
        public Text ButtonText;
        public ScrollView ScrollView;

        public void SetNameAndIndex(string itemName, int i)
        {
            _index = i;
            _name = itemName;
            ButtonText.text = itemName;
        }

        public void Button_Click()
        {
            ScrollView.ButtonClicked(_name, _index);
        }
    }
}