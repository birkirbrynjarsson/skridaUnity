using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButtonScript : MonoBehaviour {

	public Button backButton;
	public PlayMakerFSM fsm;

	// Use this for initialization
	void Start () {
		backButton.onClick.AddListener (goBack);	
	}

	void goBack(){
		fsm.Fsm.GotoPreviousState ();
	}
}
