using UnityEngine;

[CreateAssetMenu (menuName="Items/Health Item")]
public class HealthItem : Item
{
    public int recover = 1;

    protected override void Use()
    {
        player.GetComponent<Player>().RecoverHealth(recover);
        SoundManager.instance.RandomizeSfx(useSounds);
    }

    protected override void Throw()
    {
        throw new System.NotImplementedException();
    }
}
