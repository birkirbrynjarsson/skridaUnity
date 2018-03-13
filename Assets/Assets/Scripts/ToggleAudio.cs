using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAudio : MonoBehaviour {

	public AudioSource audioSource;
	public Image icon;
	public Sprite onIcon;
	public Sprite offIcon;
	public bool audioPlaying;

	public void toggle(){
		if(audioPlaying){
			icon.sprite = offIcon;
			audioSource.mute = true;
			audioPlaying = false;
		} else {
			icon.sprite = onIcon;
			audioSource.mute = false;
			audioPlaying = true;
		}
	}
}
