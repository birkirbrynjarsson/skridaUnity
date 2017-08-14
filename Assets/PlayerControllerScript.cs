﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerScript : MonoBehaviour {

	public static System.Random rand;

	public RectTransform XpProgressBar;
	private float progressWidth;
	private float progressHeight;
	public float progressMaxWidth;

	// Player Menu
	public string playerName;
	public Text playerNameText;
	public InputField playerNameInput;
	public Text playerMenuLvlText;
	public Text playerMenuXpText;
	public Text playerMenuTotalXpText;

	public string sex;
	public Button maleButton;
	public Image maleImage;
	public Sprite maleBtnInactive;
	public Sprite maleBtnActive;
	public Button femaleButton;
	public Image femaleImage;
	public Sprite femaleBtnInactive;
	public Sprite femaleBtnActive;


	// Common info
	public int level;
	public int totalXp;
	public int currentXp;
	public int levelXp;

	// Player HUD
	public Button addXpButton;
	public GameObject lvlStar;
	public GameObject lvlFlash;
	public Text lvlText;
	public Text xpText;
	public GameObject XPFirework1;

	// Use this for initialization
	void Start () {
		progressHeight = XpProgressBar.rect.height;
		rand = new System.Random((int)System.DateTime.Now.Ticks & 0x0000FFFF);
		addXpButton.onClick.AddListener (addXp);
		playerNameInput.onValueChanged.AddListener (updatePlayerName);
		maleButton.onClick.AddListener (maleClicked);
		femaleButton.onClick.AddListener (femaleClicked);

		lvlText.text = level.ToString ();

		// Event listeners to gain XP
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void addXp(){
		addXpValue (rand.Next (1000));
//		currentXp += rand.Next (1000);
//		while (currentXp > levelXp) {
//			goUpLevel ();
//			currentXp -= levelXp;
//		}
//		xpText.text = currentXp.ToString () + " / " + levelXp.ToString () + " XP";
//		updateProgressBar ();
	}

	public void addXpValue(int newXp){
		if (currentXp == 0) {
			XpProgressBar.sizeDelta = new Vector2 (0f, progressHeight);
			XpProgressBar.GetComponent<Image> ().color = new Color32(185,221,51,255);
		}
		if (currentXp + newXp >= levelXp) {
			
			totalXp += levelXp - currentXp;
			currentXp += newXp;
			newXp = currentXp - levelXp;
			currentXp = 0;

			XpProgressBar.GetComponent<Image> ().color = new Color32(255,223,0,255);
			xpText.text = currentXp.ToString () + " / " + levelXp.ToString () + " XP";

			Vector2 targetPosition = new Vector2(progressMaxWidth, progressHeight);
			iTween.ValueTo (XpProgressBar.gameObject, iTween.Hash(
				"from", XpProgressBar.sizeDelta,
				"to", targetPosition,
				"time", 1f,
				"onupdatetarget", this.gameObject, 
				"onupdate", "moveGuiElement",
				"oncomplete", "addXpValue",
				"oncompletetarget", this.gameObject,
				"oncompleteparams", newXp
			));

			goUpLevel ();

		} else {

			totalXp += newXp;
			currentXp += newXp;

			xpText.text = currentXp.ToString () + " / " + levelXp.ToString () + " XP";
			XPFirework1.SetActive (false);
			XPFirework1.SetActive (true);

			progressWidth = (float)currentXp / levelXp * progressMaxWidth;
			Vector2 targetPosition = new Vector2(progressWidth, progressHeight);
			iTween.ValueTo (XpProgressBar.gameObject, iTween.Hash(
				"from", XpProgressBar.sizeDelta,
				"to", targetPosition,
				"time", 1f,
				"onupdatetarget", this.gameObject, 
				"onupdate", "moveGuiElement"
			));
		}
		playerMenuXpText.text = currentXp.ToString () + " / " + levelXp.ToString () + " XP";
		playerMenuTotalXpText.text = totalXp.ToString () + " XP";
	}

	void updateProgressBar(){
		progressWidth = (float)currentXp / levelXp * progressMaxWidth;
		Vector2 targetPosition = new Vector2(progressWidth, progressHeight);
		iTween.ValueTo (XpProgressBar.gameObject, iTween.Hash(
			"from", XpProgressBar.sizeDelta,
			"to", targetPosition,
			"time", 0.4f,
			"onupdatetarget", this.gameObject, 
			"onupdate", "moveGuiElement"
		));

		//XpProgressBar.sizeDelta = new Vector2 (progressWidth, progressHeight);
	}

	public void moveGuiElement(Vector2 position){
		XpProgressBar.sizeDelta = position;
	}

	void goUpLevel(){
		lvlStar.transform.localScale = new Vector3 (2.4f, 2.4f, 1f);
		Vector3 scaleTo = new Vector3 (5f, 5f, 1f);
		iTween.PunchScale (lvlStar, iTween.Hash(
			"amount", scaleTo,
			"time", 2f,
			"oncomplete", "resetStarScale",
			"oncompletetarget", this.gameObject
		));
		level++;
		lvlText.text = level.ToString ();
		playerMenuLvlText.text = level.ToString ();
		lvlFlash.SetActive (false);
		lvlFlash.SetActive (true);
	}

	void resetStarScale(){
		Vector3 rotateTo = new Vector3 (0f, 0f, 720f);	
		iTween.PunchRotation (lvlStar, rotateTo, 1f);
		Vector3 scaleTo = new Vector3 (1f,1f,1f);
		iTween.ScaleTo (lvlStar, scaleTo, 1f);
	}

	void updatePlayerName(string value){
		playerName = value;
		playerNameText.text = playerName;
	}

	void maleClicked(){
		sex = "male";
		maleImage.sprite = maleBtnActive;
		femaleImage.sprite = femaleBtnInactive;
	}

	void femaleClicked(){
		sex = "female";
		femaleImage.sprite = femaleBtnActive;
		maleImage.sprite = maleBtnInactive;
	}
}
