using UnityEngine;

namespace MyDungeon
{
    public class CameraController : MonoBehaviour
    {

        GameObject _player;
        Vector3 _offset;

        // Use this for initialization
        void Start()
        {
            _offset = transform.position;
        }

        void LateUpdate()
        {
            if (_player == null)
                _player = GameObject.FindWithTag("Player");
            else
                transform.position = _player.transform.position + _offset;
        }
    }
}
