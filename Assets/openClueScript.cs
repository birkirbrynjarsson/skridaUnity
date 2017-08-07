using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openClueScript : MonoBehaviour {

	public Text title;
	public Text content;
	public Text date;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void updateClue(string title, string date, string content){
		this.title.text = title;
		this.date.text = date;
		this.content.text = content;
	}
}
