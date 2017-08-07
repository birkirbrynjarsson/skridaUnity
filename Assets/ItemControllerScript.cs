using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemControllerScript : MonoBehaviour {

	public static System.Random rand;
	public Button addItemButton;
	public Transform itemsParent;

	private List<ItemScript> items;

	// Use this for initialization
	void Start () {
		items = new List<ItemScript> ();
		foreach (Transform child in itemsParent) {
			items.Add (child.GetComponent<ItemScript>());
		}
		addItemButton.onClick.AddListener (updateItem);
		rand = new System.Random((int)System.DateTime.Now.Ticks & 0x0000FFFF);
	}
	
	// Update is called once per frame
	void Update () {
		
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
		}
	}
}
