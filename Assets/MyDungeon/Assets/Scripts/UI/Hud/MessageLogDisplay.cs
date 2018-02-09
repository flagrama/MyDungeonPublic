using UnityEngine;
using System.Collections;

namespace MyDungeon
{
    public class MessageLogDisplay : MonoBehaviour
    {
        private UnityEngine.UI.Text _messageLogText;

        // Use this for initialization
        void Start()
        {
            _messageLogText = GameObject.FindGameObjectWithTag("MessageLog").GetComponent<UnityEngine.UI.Text>();
        }

        public void AddMessage(string message)
        {
            _messageLogText.text = message + "\n" + _messageLogText.text;
        }

        public void AddDebugMessage(string message)
        {
            _messageLogText.text = "<color='yellow'>DEBUG: " + message + "</color>" + "\n" + _messageLogText.text;
        }
    }
}
