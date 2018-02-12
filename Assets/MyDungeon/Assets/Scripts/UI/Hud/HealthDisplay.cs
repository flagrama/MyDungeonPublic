using UnityEngine;

namespace MyDungeon
{
    public class HealthDisplay : MonoBehaviour
    {
        protected UnityEngine.UI.Text HealthText;

        // Use this for initialization
        protected virtual void Start()
        {
            HealthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<UnityEngine.UI.Text>();
        }

        public virtual void UpdateHealth(int curHealth, int maxHealth)
        {
            HealthText.text = string.Format("HP:{0,3}/{1,3}", curHealth, maxHealth);
        }
    }
}
