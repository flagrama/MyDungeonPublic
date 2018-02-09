using UnityEngine;
using System.Collections;

namespace MyDungeon
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private GameObject _hudPrefab;

        // Use this for initialization
        void Awake()
        {
            Instantiate(_hudPrefab);
        }
    }
}
