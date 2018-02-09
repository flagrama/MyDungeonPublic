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

        public void ButtonClicked(string str, int i)
        {
            PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            if (str == null)
            {
                player.GetComponent<MyPauseMenu>().PauseGame();
                return;
            }

            PlayerManager.Instance.Inventory[i].UseItem(player.transform);
            PlayerManager.Instance.Inventory.Remove(PlayerManager.Instance.Inventory[i]);
            GameManager.Instance.PlayersTurn = false;
            player.GetComponent<MyPauseMenu>().PauseGame();
        }
    }
}