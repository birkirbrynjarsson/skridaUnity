﻿using System.Collections;
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
	public Text location;
	public Text content;
	public Text date;
	public System.DateTime receivedTime;
	public string sTitle;
	public string sLocation;
	public string sContent;
	public string sDate;
	public Sprite sSprite;
	public PlayerControllerScript playerScript;
	public ClueControllerScript clueScript;


	// Use this for initialization
	void Start () {
		openButton.onClick.AddListener (openMessage);
		openScript = GameObject.Find("OpenClue").GetComponent<openClueScript>();
		playerScript = GameObject.Find ("PlayerGameController").GetComponent<PlayerControllerScript>();
		clueScript = GameObject.Find ("ClueGameController").GetComponent<ClueControllerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void openMessage(){
		openScript.updateClue (sTitle, sDate, sLocation, sContent, sSprite);
		if (!opened) {
			playerScript.addXpValue (135);
			opened = true;
			scrollImage.GetComponent<Image> ().overrideSprite = openScroll;
			clueScript.updateNotification (-1);
		}
	}

	public void init(string title, string location, string content, Sprite image, bool notification = true){
		if (notification) {
			UIManager.ShowNotification("MessageNotification", 3f, true, "Ný Vísbending!", title, closedScroll);
		}
		receivedTime = System.DateTime.Now;
		sTitle = title;
		string minute = this.receivedTime.Minute < 10 ? "0" + this.receivedTime.Minute.ToString () : this.receivedTime.Minute.ToString ();
		sDate = receivedTime.Hour.ToString () + ":" + minute + ", " + this.receivedTime.Day.ToString() + "/" + this.receivedTime.Month.ToString();
		sLocation = location;
		sContent = content;
		sSprite = image;
		this.title.text = sTitle;
		this.content.text = sContent;
		this.date.text = sDate;
	}
}