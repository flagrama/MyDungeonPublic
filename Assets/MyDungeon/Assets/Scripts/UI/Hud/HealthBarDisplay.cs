using UnityEngine;

namespace MyDungeon.UI.Hud
{
    public class HealthBarDisplay : MonoBehaviour
    {
        protected const int MaxHealth = 1000;
        protected const int BaseHealth = 50;
        protected RectTransform HealthBar;
        protected RectTransform HealthBarBackground;
        protected RectTransform HealthBarForeground;

        // Use this for initialization
        protected virtual void Start()
        {
            HealthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<RectTransform>();
            HealthBarBackground = GameObject.FindGameObjectWithTag("HealthBarBackground").GetComponent<RectTransform>();
            HealthBarForeground = GameObject.FindGameObjectWithTag("HealthBarForeground").GetComponent<RectTransform>();
        }

        public virtual void UpdateHealthBar(int curHealth, int maxHealth)
        {
            HealthBarForeground.sizeDelta =
                new Vector2(
                    curHealth / (float)maxHealth * Mathf.RoundToInt(
                        BaseHealth + HealthBar.rect.width * Mathf.Sin(maxHealth / (float)MaxHealth) / 2),
                    HealthBarForeground.sizeDelta.y);
            HealthBarBackground.sizeDelta = new Vector2(
                Mathf.RoundToInt(BaseHealth + HealthBar.rect.width * Mathf.Sin(maxHealth / (float)MaxHealth) / 2),
                HealthBarForeground.sizeDelta.y);
        }
    }
}
