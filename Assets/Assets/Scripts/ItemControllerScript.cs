using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ItemControllerScript : MonoBehaviour
{

    public static System.Random rand;
    public Button addItemButton;
    public Transform itemsParent;
    public PlayerControllerScript playerScript;

    public int totalItems;
    public Text playerMenuTotalItems;

    private List<ItemScript> items;

    // Use this for initialization
    void Start()
    {
        items = new List<ItemScript>();
        foreach (Transform child in itemsParent)
        {
            items.Add(child.GetComponent<ItemScript>());
        }
        addItemButton.onClick.AddListener(updateItem);
        rand = new System.Random((int)System.DateTime.Now.Ticks & 0x0000FFFF);
        ChestTrackableScript.onChestPickup += findItem;
		StartCoroutine(loadFoundTreasures());
    }

    public IEnumerator loadFoundTreasures()
    {
		yield return new WaitForSeconds(0.3f);
		foreach(var item in items){
			item.InitItem();
		}
		foreach(var treasure in playerScript.player.foundTreasures){
			if(treasure.level >= 0){
				items[treasure.treasureId].itemFound(false);
				items[treasure.treasureId].setLevel(treasure.level);
				items[treasure.treasureId].setItemDate(treasure.time);
			}
		}
		setCount();
    }
    void findItem(int itemNr)
    {
		if(playerScript.player.foundTreasures.Find(x => x.treasureId == itemNr).level >= 0){
			return;
		}
        if (-1 < itemNr && itemNr < items.Count)
        {
			var treasure = playerScript.player.foundTreasures.Find(x => x.treasureId == itemNr);
			treasure.level = 0;
			treasure.time = System.DateTime.Now;
            items[itemNr].levelUp();
			items[itemNr].setItemDate(treasure.time);
            playerScript.addXpValue(850);
        }
        else
        {
            Debug.Log("Item number out of range");
        }
        updateCount();
		playerScript.savePlayer();
    }

    public void findAllItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            findItem(i);
        }
    }

    void updateItem()
    {
        int tries = 0;
        int i = rand.Next(items.Count);
        while (items[i].currentLevel == 3 && tries < 10)
        {
            i = rand.Next(items.Count);
            tries++;
        }
        if (tries < 10)
        {
            items[i].levelUp();
            if (items[i].currentLevel == 0)
            {
                playerScript.addXpValue(850);
                updateCount();
            }
            else
            {
                playerScript.addXpValue(450);
            }
        }
    }

    public void updateItem(int index)
    {
        items[index].levelUp();
		playerScript.player.foundTreasures.Find(x => x.treasureId == index).level++;
        playerScript.addXpValue(450);
		playerScript.savePlayer();
    }

    void updateCount()
    {
        totalItems++;
		playerScript.player.totalTreasuresFound++;
        playerMenuTotalItems.text = totalItems.ToString() + " / " + items.Count.ToString();
    }

	void setCount(){
		totalItems = 0;
		foreach(var treasure in playerScript.player.foundTreasures){
			if(treasure.level >= 0){
				totalItems ++;
			}
		}
		playerScript.player.totalTreasuresFound = totalItems;
		playerMenuTotalItems.text = totalItems.ToString() + " / " + items.Count.ToString();
	}
}
