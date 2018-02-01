using UnityEngine;

namespace MyDungeon
{
    /// <summary>
    /// Controller for the Main Camera
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        private Vector3 _offset;
        private GameObject _player;

        /// <summary>
        /// Sets the position offset for the camera
        /// </summary>
        private void Start()
        {
            _offset = transform.position;
        }

        /// <summary>
        /// Centers the camera on the object tagged as <c>Player</c>
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