using UnityEngine;

namespace MyDungeon.UI.Hud
{
    public class MessageLogDisplay : MonoBehaviour
    {
        protected UnityEngine.UI.Text MessageLogText;

        // Use this for initialization
        protected virtual void Start()
        {
            MessageLogText = GameObject.FindGameObjectWithTag("MessageLog").GetComponent<UnityEngine.UI.Text>();
        }

        public virtual void AddMessage(string message)
        {
            MessageLogText.text = message + "\n" + MessageLogText.text;
        }

        public virtual void AddDebugMessage(string message)
        {
            MessageLogText.text = "<color='yellow'>DEBUG: " + message + "</color>" + "\n" + MessageLogText.text;
        }
    }
}
