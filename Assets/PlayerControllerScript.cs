using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerScript : MonoBehaviour {

	public static System.Random rand;

	public RectTransform XpProgressBar;
	private float progressWidth;
	private float progressHeight;
	public float progressMaxWidth;

	public string playerName;
	public Text playerNameText;
	public InputField playerNameInput;

	public string sex;
	public Button maleButton;
	public Image maleImage;
	public Sprite maleBtnInactive;
	public Sprite maleBtnActive;
	public Button femaleButton;
	public Image femaleImage;
	public Sprite femaleBtnInactive;
	public Sprite femaleBtnActive;

	public int level;
	public int totalXp;
	public int currentXp;
	public int levelXp;

	public Button addXpButton;
	public Text lvlText;
	public Text xpText;

	// Use this for initialization
	void Start () {
		progressHeight = XpProgressBar.rect.height;
		rand = new System.Random((int)System.DateTime.Now.Ticks & 0x0000FFFF);
		addXpButton.onClick.AddListener (addXp);
		playerNameInput.onValueChanged.AddListener (updatePlayerName);
		maleButton.onClick.AddListener (maleClicked);
		femaleButton.onClick.AddListener (femaleClicked);

		lvlText.text = level.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void addXp(){
		currentXp += rand.Next (1000);
		while (currentXp > levelXp) {
			goUpLevel ();
			currentXp -= levelXp;
		}
		xpText.text = currentXp.ToString () + " / " + levelXp.ToString () + " XP";
		updateProgressBar ();
	}

	void updateProgressBar(){
		progressWidth = (float)currentXp / levelXp * progressMaxWidth;
		XpProgressBar.sizeDelta = new Vector2 (progressWidth, progressHeight);
	}

	void goUpLevel(){
		level++;
		lvlText.text = level.ToString ();
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
