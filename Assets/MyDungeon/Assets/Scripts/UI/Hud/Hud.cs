using UnityEngine;

namespace MyDungeon
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] protected GameObject HudPrefab;

        // Use this for initialization
        protected virtual void Awake()
        {
            Instantiate(HudPrefab);
        }
    }
}
