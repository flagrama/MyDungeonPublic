using UnityEngine;

namespace MyDungeon
{
    /// <summary>
    /// Controller for the Main Camera
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        /// <summary>
        /// The offset of the camera location
        /// </summary>
        private Vector3 _offset;
        /// <summary>
        /// The game object the camera tracks
        /// </summary>
        private GameObject _player;

        /// <summary>
        /// Sets the position offset for the camera to the camera starting position
        /// </summary>
        private void Start()
        {
            _offset = transform.position;
        }

        /// <summary>
        /// Centers the camera on the object tagged as Player
        /// </summary>
        private void LateUpdate()
        {
            if (_player == null)
                _player = GameObject.FindWithTag("Player");
            else
                transform.position = _player.transform.position + _offset;
        }
    }
}