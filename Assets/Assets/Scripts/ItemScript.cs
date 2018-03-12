using System.Collections;
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
	public Sprite backgroundClosed;
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
		InitItem();
	}

	public void InitItem(){
		isFound = false;
		currentLevel = 0;
		backgroundImage.enabled = true;
		backgroundImage.sprite = backgroundClosed;
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

	public void itemFound(bool showNotification = true){
		if(showNotification){
			UIManager.ShowNotification("ItemNotification", -1, true, "Fjársjóðsfundur!", itemName, item0);
		}
		isFound = true;
		lockImage.enabled = false;
		backgroundImage.sprite = backgroundOpen;
		starSlot1.enabled = true;
		starSlot2.enabled = true;
		starSlot3.enabled = true;
		itemImage.enabled = true;
		itemImage.sprite = item0;
		currentLevel = 0;

		if(date == null){
			setItemDate(System.DateTime.Now);
		}
	}

	public void setItemDate(System.DateTime receivedTime){
		string minute = receivedTime.Minute < 10 ? "0" + receivedTime.Minute.ToString () : receivedTime.Minute.ToString ();
		date = receivedTime.Hour.ToString () + ":" + minute + ", " + receivedTime.Day.ToString() + "/" + receivedTime.Month.ToString();
	}

	public void levelUp(bool showNotification = true){
		if (!isFound) {
			itemFound (showNotification);
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

	public void setLevel(int level){
		while(currentLevel < level && level < 4){
			levelUp(false);
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
