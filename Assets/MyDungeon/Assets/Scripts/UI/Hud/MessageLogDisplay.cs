namespace MyDungeon.UI.Hud
{
    using UnityEngine;

    /// <summary>
    /// MessageLogDisplay is the class that manages updating the message log in the HUD
    /// </summary>
    public class MessageLogDisplay : MonoBehaviour
    {
        /// <summary>
        /// The UI Text object displaying the message log
        /// </summary>
        protected UnityEngine.UI.Text MessageLogText;

        /// <summary>
        /// Finds the UI Text object that displays the message log in the HUD
        /// </summary>
        protected virtual void Start()
        {
            MessageLogText = GameObject.FindGameObjectWithTag("MessageLog").GetComponent<UnityEngine.UI.Text>();
        }

        /// <summary>
        /// Adds a line to the message log
        /// </summary>
        /// <param name="message">Message to display in the message log</param>
        public virtual void AddMessage(string message)
        {
            MessageLogText.text = message + "\n" + MessageLogText.text;
        }

        /// <summary>
        /// Adds a debug line to the message log
        /// </summary>
        /// <param name="message">Message to display in the message log</param>
        public virtual void AddDebugMessage(string message)
        {
            MessageLogText.text = "<color='yellow'>DEBUG: " + message + "</color>" + "\n" + MessageLogText.text;
        }
    }
}
