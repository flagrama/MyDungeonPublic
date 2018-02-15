using MyDungeon.Items;
using MyDungeon.Managers;
using MyDungeon._Demo.Controllers;
using UnityEngine;

namespace MyDungeon._Demo.Items
{
    [CreateAssetMenu(menuName = "Demo/Items/Health Item")]
    public class HealthItem : Item
    {
        public int Recover = 1;

        protected override void Use()
        {
            Target.GetComponent<MyPlayerDungeonController>().RecoverHealth(Recover);
            SoundManager.Instance.RandomizeSfx(UseSounds);
        }
    }
}