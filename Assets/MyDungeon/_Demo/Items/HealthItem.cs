using UnityEngine;

namespace MyDungeon.Demo
{
    [CreateAssetMenu(menuName = "Demo/Items/Health Item")]
    public class HealthItem : Item
    {
        public int Recover = 1;

        protected override void Use()
        {
            Target.GetComponent<PlayerController>().RecoverHealth(Recover);
            SoundManager.Instance.RandomizeSfx(UseSounds);
        }
    }
}