using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownScript : MonoBehaviour {

	public Dropdown dropdown;

	public void nextValue(){
		int next = dropdown.value + 1;
		dropdown.value = next == dropdown.options.Count ? 0 : next;
	}

	public void prevValue(){
		int prev = dropdown.value - 1;
		dropdown.value = prev == -1 ? dropdown.options.Count - 1 : prev;
	}
}
