using UnityEngine;
using System.Collections;

namespace MyDungeon.Demo
{
    public class HealthDisplay : MonoBehaviour
    {
        private UnityEngine.UI.Text _healthText;

        // Use this for initialization
        void Start()
        {
            _healthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<UnityEngine.UI.Text>();
        }

        public void UpdateHealth(int curHealth, int maxHealth)
        {
            _healthText.text = string.Format("HP:{0,3}/{1,3}", curHealth, maxHealth);
        }
    }
}
