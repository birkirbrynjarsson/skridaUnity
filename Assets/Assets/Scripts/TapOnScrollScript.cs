using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TapOnScrollScript : MonoBehaviour, IPointerDownHandler {

	// You can add listeners in inspector
	public delegate void tapAction(GameObject scroll);
	public static event tapAction onScrollTapped;

	public void OnPointerDown(PointerEventData eventData){
		if(onScrollTapped != null){
			onScrollTapped.Invoke (gameObject);
		}
	}
}