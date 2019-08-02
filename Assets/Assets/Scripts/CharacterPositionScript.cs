using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPositionScript : MonoBehaviour {

	public PlayMakerFSM fsm;
	public Transform editParent;
	public Transform hudParent;

	// Use this for initialization
	
	// Update is called once per frame
	public void updateParent(){
		if(fsm.ActiveStateName == "PlayerAvatarMenu"){
			transform.SetParent(editParent);
			transform.localPosition = new Vector3 (0f, 0f, 0f);
			transform.localScale = new Vector3 (1f, 1f, 1f);
		} else {
			transform.SetParent (hudParent);
			transform.localPosition = new Vector3 (48, -10f, 0f);
			transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
		}
		if (fsm.ActiveStateName == "MainMenu") {
			transform.localScale = new Vector3 (0f, 0f, 0f);
		}
	}
}
