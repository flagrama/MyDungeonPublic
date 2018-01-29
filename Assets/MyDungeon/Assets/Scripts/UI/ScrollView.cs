using UnityEngine;

public class ScrollView : MonoBehaviour
{
    public GameObject Button_Template;

    // Use this for initialization
    public void Populate()
    {
        for (int i = 0; i < PlayerManager.instance.inventory.Count; i++)
        {
            GameObject go = Instantiate(Button_Template);
            go.SetActive(true);
            ScrollButton TB = go.GetComponent<ScrollButton>();
            TB.SetNameAndIndex(PlayerManager.instance.inventory[i].name, i);
            go.transform.SetParent(Button_Template.transform.parent);
        }
    }

    public void Depopulate()
    {
        Transform scrollContent = transform.GetChild(0).transform;
        for (int i = 1; i < scrollContent.childCount; i++)
        {
            Destroy(scrollContent.GetChild(i).gameObject);
        }
    }

    public void ButtonClicked(string str, int i)
    {
        if (str == null)
        {
            MenuManager.instance.Pause();
            GameObject.Find("Player(Clone)").GetComponent<Player>().enabled = true;
            return;
        }
        PlayerManager.instance.inventory[i].UseItem();
        GameManager.instance.playersTurn = false;
        MenuManager.instance.Pause();
    }
}
