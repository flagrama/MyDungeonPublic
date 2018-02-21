namespace MyDungeon.UI.Hud
{
    using UnityEngine;

    /// <summary>
    /// Hud is the class that instantiates the HUD prefab
    /// </summary>
    public class Hud : MonoBehaviour
    {
        /// <summary>
        /// The HUD prefab
        /// </summary>
        [SerializeField] protected GameObject HudPrefab;

        /// <summary>
        /// Instantiate the HUD prefab
        /// </summary>
        protected virtual void Awake()
        {
            Instantiate(HudPrefab);
        }
    }
}
