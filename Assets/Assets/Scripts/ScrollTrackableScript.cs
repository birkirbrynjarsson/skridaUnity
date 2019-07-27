/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vuforia
{
    public class ScrollTrackableScript : MonoBehaviour, ITrackableEventHandler
    {

        public int messageNumber;
        public delegate void messageAction(int msgNr, bool showNotification);
        public static event messageAction onMessagePickup;

        private TrackableBehaviour mTrackableBehaviour;

        private GameObject spawnedScroll;
        private List<GameObject> spawnedEffects;

        public PlayerControllerScript playerController;
        public AudioClip revealSound;

        void Start()
        {
            TapOnScrollScript.onScrollTapped += pickUpScroll;
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        public void pickUpScroll(GameObject scroll)
        {
            if (scroll == spawnedScroll)
            {
                Destroy(spawnedScroll);
                if (onMessagePickup != null)
                {
                    onMessagePickup.Invoke(messageNumber, true);
                }
            }
        }

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
            TrackableBehaviour.Status previousStatus,
            TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
                startClueFound();
            }
            else
            {
                OnTrackingLost();
                stopClueFound();
            }
        }

        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

            // Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

            // Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        void startClueFound()
        {
            createCircularLight();
            if (!clueFound())
            {
                playRevealSound();
                GameObject scrollObject = (GameObject)Resources.Load("scroll");
                spawnedScroll = Instantiate(scrollObject);
                spawnedScroll.transform.SetParent(transform, false);
                spawnedScroll.GetComponent<ScrollScript>().resetPosition();
                spawnedScroll.GetComponent<ScrollScript>().goUp();
            }
        }

        void stopClueFound()
        {
            if (spawnedScroll != null)
            {
                Destroy(spawnedScroll);
            }
            if (spawnedEffects != null)
            {
                for (int i = 0; i < spawnedEffects.Count; i++)
                {
                    Destroy(spawnedEffects[i]);
                }
            }
        }

        void createCircularLight()
        {
            if (spawnedEffects == null)
            {
                spawnedEffects = new List<GameObject>();
            }
            string jmoEffects = "CFX Prefabs (Mobile)/";
            GameObject effectObject = (GameObject)Resources.Load(jmoEffects + "Misc/CFXM_CircularLightWall");
            GameObject effect = Instantiate(effectObject);
            effect.transform.parent = gameObject.transform;
            effect.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            spawnedEffects.Add(effect);
        }

        bool clueFound()
        {
            if (playerController.player.foundMessages.Find(x => x.messageId == messageNumber) != null)
            {
                return true;
            }
            return false;
        }

        void playRevealSound()
        {
            playerController.gameObject.GetComponent<AudioSource>().PlayOneShot(revealSound);
        }
    }
}