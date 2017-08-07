using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DoozyUI;

public class messageScript : MonoBehaviour {

	public bool opened;
	public GameObject scrollImage;
	public openClueScript openScript;
	public Sprite closedScroll;
	public Sprite openScroll;
	public Button openButton;
	public Text title;
	public Text content;
	public Text date;
	public System.DateTime receivedTime;
	public string sTitle;
	public string sContent;
	public string sDate;


	// Use this for initialization
	void Start () {
		openButton.onClick.AddListener (openMessage);
		openScript = GameObject.Find("OpenClue").GetComponent<openClueScript>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void openMessage(){
		openScript.updateClue (sTitle, sDate, sContent);
		if (!opened) {
			opened = true;
			scrollImage.GetComponent<Image> ().overrideSprite = openScroll;
		}
	}

	public void init(string title, string content){
		UIManager.ShowNotification("MessageNotification", 3f, true, "Ný Vísbending!", title, closedScroll);
		this.receivedTime = System.DateTime.Now;
		this.sTitle = title;
		this.sContent = content;
		this.sDate = this.receivedTime.Hour.ToString () + ":" + this.receivedTime.Minute.ToString () + ", " + this.receivedTime.Day.ToString() + "/" + this.receivedTime.Month.ToString();
		this.title.text = this.sTitle;
		this.content.text = this.sContent;
		this.date.text = this.sDate;
	}
}
