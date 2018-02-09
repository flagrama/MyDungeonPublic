using UnityEngine;
using System.Collections;

namespace MyDungeon.Demo
{
    public class HealthBarDisplay : MonoBehaviour
    {
        private const int MaxHealth = 1000;
        private const int BaseHealth = 50;
        private RectTransform _healthBar;
        private RectTransform _healthBarBackground;
        private RectTransform _healthBarForeground;

        // Use this for initialization
        void Start()
        {
            _healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<RectTransform>();
            _healthBarBackground = GameObject.FindGameObjectWithTag("HealthBarBackground").GetComponent<RectTransform>();
            _healthBarForeground = GameObject.FindGameObjectWithTag("HealthBarForeground").GetComponent<RectTransform>();
        }

        public void UpdateHealthBar(int curHealth, int maxHealth)
        {
            _healthBarForeground.sizeDelta =
                new Vector2(
                    curHealth / (float)maxHealth * Mathf.RoundToInt(
                        BaseHealth + _healthBar.rect.width * Mathf.Sin(maxHealth / (float)MaxHealth) / 2),
                    _healthBarForeground.sizeDelta.y);
            _healthBarBackground.sizeDelta = new Vector2(
                Mathf.RoundToInt(BaseHealth + _healthBar.rect.width * Mathf.Sin(maxHealth / (float)MaxHealth) / 2),
                _healthBarForeground.sizeDelta.y);
        }
    }
}
