﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DoozyUI;

public class ItemScript : MonoBehaviour {

	public string itemName;
    [TextArea]
	public string about;
	public string location;
	public string date;
    public int itemIndex;

	public Button openItemButton;
	public Image backgroundImage;
	public Sprite backgroundOpen;
	public Image lockImage;
	public Image itemImage;
	public Sprite item0;
	public Sprite item1;
	public Sprite item2;
	public Sprite item3;
	public Image starSlot1;
	public Image starSlot2;
	public Image starSlot3;
	public Image star1;
	public Image star2;
	public Image star3;

	public PlayMakerFSM fsm;

	public OpenItemScript openItemScript;

	private bool isFound;
	public int currentLevel;

	// Use this for initialization
	void Start () {
		isFound = false;
		currentLevel = 0;
		backgroundImage.enabled = true;
		lockImage.enabled = true;
		itemImage.enabled = false;
		starSlot1.enabled = false;
		starSlot2.enabled = false;
		starSlot3.enabled = false;
		star1.enabled = false;
		star2.enabled = false;
		star3.enabled = false;

		openItemButton.onClick.AddListener (openItem);
		openItemScript = GameObject.Find("OpenItem").GetComponent<OpenItemScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void itemFound(){
		UIManager.ShowNotification("ItemNotification", 10f, true, "Fjársjóðsfundur!", itemName, item0);
		isFound = true;
		lockImage.enabled = false;
		backgroundImage.sprite = backgroundOpen;
		starSlot1.enabled = true;
		starSlot2.enabled = true;
		starSlot3.enabled = true;
		itemImage.enabled = true;
		itemImage.sprite = item0;

		System.DateTime receivedTime = System.DateTime.Now;
		string minute = receivedTime.Minute < 10 ? "0" + receivedTime.Minute.ToString () : receivedTime.Minute.ToString ();
		date = receivedTime.Hour.ToString () + ":" + minute + ", " + receivedTime.Day.ToString() + "/" + receivedTime.Month.ToString();
	}

	public void levelUp(){
		if (!isFound) {
			itemFound ();
		} else {
			currentLevel++;
			switch (currentLevel) {
			case 1:
				level1 ();
				break;
			case 2:
				level2 ();
				break;
			case 3:
				level3 ();
				break;
			default:
				currentLevel = 3;
				break;
			}
		}
	}

	private void level1(){
		star1.enabled = true;
		itemImage.sprite = item1;
	}

	private void level2(){
		star2.enabled = true;
		itemImage.sprite = item2;
	}

	private void level3(){
		star3.enabled = true;
		itemImage.sprite = item3;
	}

	public void openItem(){
		if (isFound) {
            openItemScript.updateClue (itemName, date, location, about, itemImage.sprite, currentLevel, itemIndex);
			fsm.SetState ("OpenItem");
		}
	}
}