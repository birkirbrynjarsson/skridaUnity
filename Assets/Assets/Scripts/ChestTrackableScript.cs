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
    public class ChestTrackableScript : MonoBehaviour, ITrackableEventHandler
    {

        public int treasureNumber;
        public delegate void chestAction(int nr);
        public static event chestAction onChestPickup;

        private TrackableBehaviour mTrackableBehaviour;

        private GameObject spawnedChest;
        private List<GameObject> spawnedEffects;

        public PlayerControllerScript playerController;
        public AudioClip revealSound;

        // Use this for initialization
        void Start()
        {
            TapOnChestScript.onChestTapped += pickUpChest;
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        public void pickUpChest(GameObject chest)
        {
            if (chest == spawnedChest)
            {
                Destroy(spawnedChest);
                if (onChestPickup != null)
                {
                    onChestPickup.Invoke(treasureNumber);
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
                startTreasureFound();
            }
            else
            {
                OnTrackingLost();
                stopTreasureFound();
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

        void startTreasureFound()
        {
            createCircularLight();
            if (!treasureFound())
            {
                playRevealSound();
                GameObject chestObject = (GameObject)Resources.Load("TreasureChest");
                spawnedChest = Instantiate(chestObject);
                spawnedChest.transform.SetParent(transform, false);
                spawnedChest.GetComponent<ChestScript>().resetPosition();
                spawnedChest.GetComponent<ChestScript>().goUp();
            }
        }

        void stopTreasureFound()
        {
            if (spawnedChest != null)
            {
                Destroy(spawnedChest);
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
            effect.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            spawnedEffects.Add(effect);
        }

        bool treasureFound()
        {
            if (playerController.player.foundTreasures.Find(x => x.treasureId == treasureNumber).level >= 0)
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
