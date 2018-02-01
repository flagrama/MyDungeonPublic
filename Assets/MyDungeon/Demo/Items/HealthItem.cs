using UnityEngine;

namespace MyDungeon.Demo
{
    [CreateAssetMenu(menuName = "Demo/Items/Health Item")]
    public class HealthItem : Item
    {
        public int recover = 1;

        protected override void Use(Transform target)
        {
            target.GetComponent<Player>().RecoverHealth(recover);
            SoundManager.instance.RandomizeSfx(useSounds);
        }
    }
}
