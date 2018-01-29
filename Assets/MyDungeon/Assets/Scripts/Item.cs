using UnityEngine;

public abstract class Item : ScriptableObject
{
    public AudioClip[] useSounds;

    protected Transform player;

	public virtual void UseItem ()
	{
	    PlayerManager.instance.RemoveItem(this);
	    player = GameObject.FindGameObjectWithTag("Player").transform;
	    Use();
	}

    public virtual void ThrowItem()
    {
        PlayerManager.instance.RemoveItem(this);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Throw();
    }
	
    protected abstract void Use();
    protected abstract void Throw();
}
