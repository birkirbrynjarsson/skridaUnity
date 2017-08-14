using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour {

	public Vector3 initPos;
	public Quaternion initRot;
	public Vector3 initScl;

	public Vector3 destPos;
	public float travelTime;

	public bool hover;
	public float hoverSpeed;
	public float hoverMax;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (hover) {
			float yPos = destPos.y + Mathf.Sin(Time.time * hoverSpeed) * hoverMax;
			transform.localPosition = new Vector3(0f, yPos, 0f);
		}
	}

	public void resetPosition(){
		hover = false;
		transform.localPosition = initPos;
		transform.localRotation = initRot;
		transform.localScale = initScl;
	}

	public void goUp(){
		Vector3 rotation = new Vector3 (0f, 1080f, 0f);
		iTween.MoveTo(gameObject, iTween.Hash("position", destPos, "islocal", true, "easetype", iTween.EaseType.easeInOutSine, "time", travelTime, "oncomplete", "endPosAnimation"));
		iTween.RotateAdd(gameObject, iTween.Hash("amount", rotation, "time", travelTime, "easetype", iTween.EaseType.easeOutBounce, "looptype", iTween.LoopType.none));
	}

	public void endPosAnimation(){
		hover = true;
	}
}
