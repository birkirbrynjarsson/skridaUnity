using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour {

	public static System.Random rand;

	public Vector3 initPos;
	public Quaternion initRot;
	public Vector3 initScl;

	public Vector3 destPos;
	public float travelTime;

	void Start(){
		rand = new System.Random((int)System.DateTime.Now.Ticks & 0x0000FFFF);
	}

	public void resetPosition(){
		transform.localPosition = initPos;
		transform.localRotation = initRot;
		transform.localScale = initScl;
	}

	public void goUp(){
		Vector3 rotation = new Vector3 (90f, 60f, 0f);
		iTween.MoveTo(gameObject, iTween.Hash("position", destPos, "islocal", true, "easetype", iTween.EaseType.linear, "time", travelTime, "oncomplete", "endPosAnimation"));
		iTween.RotateAdd(gameObject, iTween.Hash("amount", rotation, "time", travelTime, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.none));
	}

	void endPosAnimation(){
		StartCoroutine (createFireWorks ());
	}

	IEnumerator createFireWorks(){
		List<GameObject> effectObjects = new List<GameObject> ();
		string jmoFireworks = "CFX Prefabs (Mobile)/Explosions/Firework Variants/CFXM_Firework_";
		effectObjects.Add ((GameObject)Resources.Load ("CFX Prefabs (Mobile)/Explosions/CFXM_Firework"));
		effectObjects.Add ((GameObject)Resources.Load (jmoFireworks + "Blue"));
		effectObjects.Add ((GameObject)Resources.Load (jmoFireworks + "Green"));
		effectObjects.Add ((GameObject)Resources.Load (jmoFireworks + "Orange"));
		effectObjects.Add ((GameObject)Resources.Load (jmoFireworks + "Red"));
		float x = (float)rand.Next (50) + transform.position.x - 25f;
		float y = (float)rand.Next (50) + transform.position.y - 25f;
		float z = (float)rand.Next (50) + transform.position.z - 25f;
		while (true) {
			GameObject effect = Instantiate (effectObjects[rand.Next(effectObjects.Count)]);
			effect.transform.position = new Vector3(x, y, z);
			yield return new WaitForSeconds ((float)rand.Next(8)/10);
			x = (float)rand.Next (50) + transform.position.x - 25f;
			y = (float)rand.Next (50) + transform.position.y - 25f;
			z = (float)rand.Next (50) + transform.position.z - 25f;
		}
	}
}
