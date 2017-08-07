using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPprogressScript : MonoBehaviour {

	public RectTransform rectangle;
	private float rectWidth;
	private float rectHeight;

	public float maxWidth;
	public int currentXP;
	public int levelXP;

	// Use this for initialization
	void Start () {
		rectWidth = rectangle.rect.width;
		rectHeight = rectangle.rect.height;

		currentXP = 500;
		levelXP = 1000;
		rectWidth = (float)currentXP / levelXP * maxWidth;

		rectangle.sizeDelta = new Vector2 (rectWidth, rectHeight);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
