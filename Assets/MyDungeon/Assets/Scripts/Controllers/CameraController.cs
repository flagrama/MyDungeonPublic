namespace MyDungeon.Controllers
{
    using UnityEngine;

    /// <summary>
    /// Controller for the Main Camera
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        /// <summary>
        /// The offset of the camera location
        /// </summary>
        protected Vector3 Offset;
        /// <summary>
        /// The game object the camera tracks
        /// </summary>
        protected GameObject Player;

        /// <summary>
        /// Sets the position offset for the camera to the camera starting position
        /// </summary>
        protected virtual void Start()
        {
            Offset = transform.position;
        }

        /// <summary>
        /// Centers the camera on the object tagged as Player
        /// </summary>
        protected virtual void LateUpdate()
        {
            if (Player == null)
                Player = GameObject.FindWithTag("Player");
            else
                transform.position = Player.transform.position + Offset;
        }
    }
}