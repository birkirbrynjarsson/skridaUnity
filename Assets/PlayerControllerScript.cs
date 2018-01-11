﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DoozyUI;

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
	public Text playerTitle;
	public Text playerMenuLvlText;
	public Text playerMenuXpText;
	public Text playerMenuTotalXpText;
    public Sprite levelUpNotificationStar;
    public Sprite titleMedal;

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
    public int newTitleLevels;

	// Player HUD
	public Button addXpButton;
	public GameObject lvlStar;
	public GameObject lvlFlash;
	public Text lvlText;
	public Text xpText;
	public GameObject XPFirework1;

    private string[,] titles = {
        {"Vinnumaður", "Nemandi", "Munkur", "Prestvígður Munkur", "Príor", "Ábóti", "Skálholtsbiskup"},
        {"Vinnukona", "Nemandi", "Nunna", "Prestvígð Nunna", "Príorinna", "Abbadís", "Skálholtsbiskup"}
    };

    private string[] titleText = {"brumtiss", "barabadibbidibb", "jejeje"};

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

			goUpLevel (newXp);

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

	void goUpLevel(int addXp){
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
        UIManager.ShowNotification("LevelNotification", 10f, true, level.ToString(), "Til lukku!\nÞú hefur farið upp um stig (level)\nHeildar stigafjöldi: " + (totalXp + addXp).ToString() + " XP", levelUpNotificationStar);
        if(level % newTitleLevels == 0){
            updateTitle ();
        }
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
		playerTitle.text = titles[0, level/newTitleLevels];
	}

	void femaleClicked(){
		sex = "female";
		femaleImage.sprite = femaleBtnActive;
		maleImage.sprite = maleBtnInactive;
        playerTitle.text = titles[1, level/newTitleLevels];
	}

	void updateTitle(){
		if (sex == "female") {
            if(level/newTitleLevels < titles.GetLength(0)){
                playerTitle.text = titles[0, level/newTitleLevels];
                UIManager.ShowNotification("TitleNotification", 10f, true, titles[0, level/newTitleLevels], titleText[level/newTitleLevels], titleMedal);
            }
			// if (level >= 24) {
			// 	playerTitle.text = "Skálholtsbiskup";
			// } else if (level >= 20) {
			// 	playerTitle.text = "Abbadís";
			// } else if (level >= 16) {
			// 	playerTitle.text = "Príorinna";
			// } else if (level >= 12) {
			// 	playerTitle.text = "Prestvígð Nunna";
			// } else if (level >= 8) {
			// 	playerTitle.text = "Nunna / Reglusystir";
			// } else if (level >= 4) {
			// 	playerTitle.text = "Nemandi";
			// } else {
			// 	playerTitle.text = "Vinnukona";
			// }
		} else {
            if(level/newTitleLevels < titles.GetLength(1)){
                playerTitle.text = titles[1, level/newTitleLevels];
                UIManager.ShowNotification("TitleNotification", 10f, true, titles[1, level/newTitleLevels], titleText[level/newTitleLevels], titleMedal);
            }
			// if (level >= 24) {
			// 	playerTitle.text = "Skálholtsbiskup";
			// } else if (level >= 20) {
			// 	playerTitle.text = "Ábóti";
			// } else if (level >= 16) {
			// 	playerTitle.text = "Príor";
			// } else if (level >= 12) {
			// 	playerTitle.text = "Prestvígður Munkur";
			// } else if (level >= 8) {
			// 	playerTitle.text = "Munkur / Reglubróðir";
			// } else if (level >= 4) {
			// 	playerTitle.text = "Nemandi";
			// } else {
			// 	playerTitle.text = "Vinnumaður";
			// }
		}
	}
}
