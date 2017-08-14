using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openClueScript : MonoBehaviour {

	public Text title;
	public Text date;
	public Text location;
	public Text content;
	public Image image;
	public Sprite sprite;

	private float noImageTop;
	private float yesImageTop;

	void Start(){
		noImageTop = -145f;
		yesImageTop = -330f;
	}

	public void updateClue(string title, string date, string location, string content, Sprite sprite){
		this.title.text = title;
		this.date.text = date;
		this.location.text = location;
		this.content.text = content;
		if (sprite != null) {
			this.image.gameObject.SetActive (true);
			this.image.overrideSprite = sprite;
			this.content.rectTransform.offsetMax = new Vector2 (this.content.rectTransform.offsetMax.x, yesImageTop);
		} else {
			this.image.gameObject.SetActive (false);
			this.content.rectTransform.offsetMax = new Vector2 (this.content.rectTransform.offsetMax.x, noImageTop);
		}
	}
}
