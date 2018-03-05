using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineHoverScript : MonoBehaviour {

	public bool hoverOn = false;
	public bool rotateOn = false;
	public float speed = 2f;
	public float max = 30f;

	// Use this for initialization
	void Start () {
		StartCoroutine (hover());
	}
	
	// Update is called once per frame
	void Update () {
		if (hoverOn) {
			float zPos = Mathf.Sin(Time.time * speed) * max;
			transform.localPosition = new Vector3(0f, 0f, zPos);
		}
		if (rotateOn) {
			transform.Rotate (0,20*Time.deltaTime,40*Time.deltaTime);
		}
	}

	IEnumerator hover(){
		yield return new WaitForSeconds (2f);
		hoverOn = true;
		rotateOn = true;
	}
}
