using System.Collections;
using System.Collections.Generic;
using DoozyUI;
using UnityEngine;

public class LoadingController : MonoBehaviour {

	public GameObject loadingIndicator;
	public PlayMakerFSM fsm;
	public Sprite noCameraNotificationImage;

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

	public void goToSearchState(){
		#if UNITY_IOS && !UNITY_EDITOR
		iOSCameraPermission.VerifyPermission(gameObject.name, "CameraPermissionCallback");
		#elif PLATFORM_ANDROID && !UNITY_EDITOR
		if(AndroidRuntimePermissions.CheckPermission("android.permission.CAMERA") == AndroidRuntimePermissions.Permission.Granted) {
			CameraPermissionCallback("true");
		} else {
			CameraPermissionCallback("false");
		}
		#else
		if (Application.HasUserAuthorization(UserAuthorization.WebCam)) {
			CameraPermissionCallback("true");
		} else {
			CameraPermissionCallback("false");
		}
		#endif
	}

	private void CameraPermissionCallback(string permissionWasGranted)
    {        
        if (permissionWasGranted == "true") {
			StartLoading();
			fsm.SetState("Search");	
        } else {
			UIManager.ShowNotification("NoStarNotification", -1, true, "Villa! Engin Myndavél", "Þú verður að virkja myndavéla- aðgang fyrir appið í stillingum á símanum þínum.", noCameraNotificationImage);
        }
    }



}
