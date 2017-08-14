using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ItemControllerScript : MonoBehaviour {

	public static System.Random rand;
	public Button addItemButton;
	public Transform itemsParent;
	public PlayerControllerScript playerScript;

	public int totalItems;
	public Text playerMenuTotalItems;

	private List<ItemScript> items;

	// Use this for initialization
	void Start () {
		items = new List<ItemScript> ();
		foreach (Transform child in itemsParent) {
			items.Add (child.GetComponent<ItemScript>());
		}
		addItemButton.onClick.AddListener (updateItem);
		rand = new System.Random((int)System.DateTime.Now.Ticks & 0x0000FFFF);
		ChestTrackableScript.onChestPickup += findItem;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void findItem(int itemNr){
		if (-1 < itemNr && itemNr < items.Count) {
			items [itemNr].levelUp ();
			playerScript.addXpValue (850);
		} else {
			Debug.Log ("Item number out of range");
		}
		updateCount ();
	}

	void updateItem(){
		int tries = 0;
		int i = rand.Next (items.Count);
		while (items [i].currentLevel == 3 && tries < 10) {
			i = rand.Next (items.Count);
			tries++;
		}
		if (tries < 10) {
			items [i].levelUp ();
			if (items [i].currentLevel == 0) {
				playerScript.addXpValue (850);
				updateCount ();
			} else  {
				playerScript.addXpValue (450);
			}
		}
	}

	void updateCount(){
		totalItems++;
		playerMenuTotalItems.text = totalItems.ToString () + " / " + items.Count.ToString();
	}
}
