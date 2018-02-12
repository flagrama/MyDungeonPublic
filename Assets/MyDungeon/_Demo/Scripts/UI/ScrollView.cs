using System.Collections.Generic;
using UnityEngine;

namespace MyDungeon.Demo
{
    public class ScrollView : MonoBehaviour
    {
        public GameObject ButtonTemplate;

        // Use this for initialization
        public void Populate()
        {
            List<Item> items = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().InventoryItems;

            for (int i = 0; i < items.Count; i++)
            {
                GameObject go = Instantiate(ButtonTemplate);
                go.SetActive(true);
                ScrollButton tb = go.GetComponent<ScrollButton>();
                tb.SetNameAndIndex(items[i].name, i);
                go.transform.SetParent(ButtonTemplate.transform.parent);
            }
        }

        public void ButtonClicked(string str, int i)
        {
            PlayerDungeonController playerDungeon = GameObject.FindGameObjectWithTag("Player").GetComponent<MyPlayerDungeonController>();
            if (str == null)
            {
                playerDungeon.GetComponent<MyPauseMenu>().PauseGame();
                return;
            }

            Inventory inventory = playerDungeon.GetComponent<Inventory>();
            inventory.InventoryItems[i].UseItem(playerDungeon.transform);
            inventory.InventoryItems.Remove(inventory.InventoryItems[i]);
            GameManager.PlayersTurn = false;
            playerDungeon.GetComponent<MyPauseMenu>().PauseGame();
        }
    }
}