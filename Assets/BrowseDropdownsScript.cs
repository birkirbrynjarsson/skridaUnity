using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrowseDropdownsScript : MonoBehaviour {

	public GameObject dropdownParent;
	public Button nextChildBtn;
	public Button prevChildBtn;
	public List<Transform> dropdowns;
	public int currentActive;

	// Use this for initialization
	void Start () {
		nextChildBtn.onClick.AddListener (nextDropdown);
		prevChildBtn.onClick.AddListener (prevDropdown);
		dropdowns = new List<Transform> ();
		foreach(Transform child in dropdownParent.transform){
			dropdowns.Add (child);
			child.gameObject.SetActive (false);
		}
		currentActive = 2;
		dropdowns [currentActive].gameObject.SetActive (true);
	}
	
	public void nextDropdown(){
		dropdowns [currentActive].gameObject.SetActive (false);
		currentActive++;
		if (currentActive >= dropdowns.Count) {
			currentActive = 0;
		}
		if (currentActive == 0 || currentActive == 1 || currentActive == 7 || currentActive == 12 || currentActive == 13 || currentActive == 15) {
			nextDropdown ();
		}
		dropdowns [currentActive].gameObject.SetActive (true);
	}

	public void prevDropdown(){
		dropdowns [currentActive].gameObject.SetActive (false);
		currentActive--;
		if (currentActive < 0) {
			currentActive = dropdowns.Count - 1;
		}
		if (currentActive == 0 || currentActive == 1 || currentActive == 7 || currentActive == 12 || currentActive == 13 || currentActive == 15) {
			prevDropdown ();
		}
		dropdowns [currentActive].gameObject.SetActive (true);
	}
}
