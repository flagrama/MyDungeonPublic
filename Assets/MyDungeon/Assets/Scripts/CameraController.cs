using UnityEngine;

namespace MyDungeon
{
    public class CameraController : MonoBehaviour
    {
        private Vector3 _offset;
        private GameObject _player;

        // Use this for initialization
        private void Start()
        {
            _offset = transform.position;
        }

        private void LateUpdate()
        {
            if (_player == null)
                _player = GameObject.FindWithTag("Player");
            else
                transform.position = _player.transform.position + _offset;
        }
    }
}