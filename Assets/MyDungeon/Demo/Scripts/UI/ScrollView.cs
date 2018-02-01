using UnityEngine;

namespace MyDungeon.Demo
{
    public class ScrollView : MonoBehaviour
    {
        public GameObject ButtonTemplate;

        // Use this for initialization
        public void Populate()
        {
            for (int i = 0; i < PlayerManager.Instance.Inventory.Count; i++)
            {
                GameObject go = Instantiate(ButtonTemplate);
                go.SetActive(true);
                ScrollButton tb = go.GetComponent<ScrollButton>();
                tb.SetNameAndIndex(PlayerManager.Instance.Inventory[i].name, i);
                go.transform.SetParent(ButtonTemplate.transform.parent);
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
            Player player = GameObject.Find("Player(Clone)").GetComponent<Player>();
            if (str == null)
            {
                MenuManager.Instance.Pause();
                player.enabled = true;
                return;
            }

            PlayerManager.Instance.Inventory[i].UseItem(player.transform);
            PlayerManager.Instance.Inventory.Remove(PlayerManager.Instance.Inventory[i]);
            GameManager.Instance.PlayersTurn = false;
            MenuManager.Instance.Pause();
        }
    }
}