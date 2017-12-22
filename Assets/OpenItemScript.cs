using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenItemScript : MonoBehaviour {

	public Text title;
	public Text date;
	public Text location;
	public Text description;
	public Image image;

	public RectTransform scrollWrapper;
	public Sprite play;
	public Sprite locked;
	public Sprite finished;

	public Image challenge1;
	public Image challenge2;
	public Image challenge3;

	public void updateClue(string title, string date, string location, string description, Sprite sprite, int level){
		this.title.text = title;
		this.date.text = date;
		this.location.text = location;
		this.description.text = description;
		this.image.overrideSprite = sprite;

		scrollWrapper.localPosition = new Vector3 (0f, 0f, 0f);

		if (level == 0) {
			challenge1.overrideSprite = play;
			challenge2.overrideSprite = locked;
			challenge3.overrideSprite = locked;
		} else if (level == 1) {
			challenge1.overrideSprite = finished;
			challenge2.overrideSprite = play;
			challenge3.overrideSprite = locked;
		} else if (level == 2) {
			challenge1.overrideSprite = finished;
			challenge2.overrideSprite = finished;
			challenge3.overrideSprite = play;
		} else if (level == 3) {
			challenge1.overrideSprite = finished;
			challenge2.overrideSprite = finished;
			challenge3.overrideSprite = finished;
		}
	}
}
