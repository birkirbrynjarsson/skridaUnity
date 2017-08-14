using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TapOnChestScript : MonoBehaviour, IPointerDownHandler {

	public delegate void tapAction(GameObject chest);
	public static event tapAction onChestTapped;

	public void OnPointerDown(PointerEventData eventData){
		if(onChestTapped != null){
			onChestTapped.Invoke (gameObject);
		}
	}
}
