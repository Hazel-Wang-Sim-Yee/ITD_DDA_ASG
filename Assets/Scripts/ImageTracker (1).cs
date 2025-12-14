/*
* Author: Hazel
* Date: 2025-11-30
* Description: Tracks images using AR Foundation and spawns associated prefabs.
*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ImageTracker : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager trackedImageManager; // Reference to the ARTrackedImageManager

    [SerializeField]
    private GameObject[] placeablePrefabs; // Array of prefabs to place for each tracked image

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>(); // Dictionary to hold spawned prefabs

    // Subscribe to tracked image events
    private void Start()
    {
        // Get the ARTrackedImageManager component
        if (trackedImageManager != null)
        {
            trackedImageManager.trackablesChanged.AddListener(OnImageChanged);
            SetupPrefabs();
        }
    }

    // Unsubscribe from tracked image events
    void SetupPrefabs()
    {
        // Instantiate and store prefabs in the dictionary
        foreach (GameObject prefab in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            newPrefab.SetActive(false);
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }
    }

    // Called when tracked images are changed
    void OnImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }

        foreach (KeyValuePair<TrackableId, ARTrackedImage> lostObj in eventArgs.removed)
        {
            UpdateImage(lostObj.Value);
        }
    }

    // Update the tracked image and associated prefab
    void UpdateImage(ARTrackedImage trackedImage)
    {
        if(trackedImage != null)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                //Enable the associated content
                if(spawnedPrefabs[trackedImage.referenceImage.name].transform.parent != trackedImage.transform)
                {
                    spawnedPrefabs[trackedImage.referenceImage.name].transform.SetParent(trackedImage.transform);
                    spawnedPrefabs[trackedImage.referenceImage.name].transform.localPosition = Vector3.zero;
                    spawnedPrefabs[trackedImage.referenceImage.name].transform.localRotation = Quaternion.identity;
                    spawnedPrefabs[trackedImage.referenceImage.name].SetActive(true);
                }
            }
        }
    }
}
