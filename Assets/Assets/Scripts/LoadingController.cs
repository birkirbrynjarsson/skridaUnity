using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingController : MonoBehaviour {

	public GameObject loadingIndicator;

	void Start(){
		loadingIndicator.SetActive(false);
	}
	public void StartLoading(){
		loadingIndicator.SetActive(true);
	}

	public void StopLoading(){
		StartCoroutine(DestroyLoading());
	}

	private IEnumerator DestroyLoading(){
		yield return new WaitForSeconds(0.3f);
		loadingIndicator.SetActive(false);
	}

}
