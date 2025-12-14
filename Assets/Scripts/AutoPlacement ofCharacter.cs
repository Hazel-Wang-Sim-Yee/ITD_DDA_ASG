/*
* Author: Hazel
* Date: 2025-12-05
* Description: Automatically places the character prefab on detected AR planes.
*/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class AutoPlacementofCharacter : MonoBehaviour
{
    [SerializeField]
    private GameObject welcomePanel; // Panel to show welcome message

    [SerializeField]
    private GameObject placedPrefab; // Prefab to be placed on AR plane

    private GameObject placedObject; // Instance of the placed prefab
    [SerializeField]
    private Button dismissButton; // Button to dismiss the welcome panel
    [SerializeField]
    private ARPlaneManager arPlaneManager; // AR Plane Manager to detect planes

    // Initialize the AR plane detection and button listener
    void Awake()
    {
        dismissButton.onClick.AddListener(Dismiss); 
        // Start listening for plane changes
        if (placedObject == null)
        {
            arPlaneManager = GetComponent<ARPlaneManager>();
            arPlaneManager.planesChanged += PlaneChanged;
        }
        else
        {
            arPlaneManager.planesChanged += ResetCharacterSpawning;
        }
    }

    // Called when AR planes are changed
    private void PlaneChanged(ARPlanesChangedEventArgs args)
    {
        // Place the prefab on the first detected plane
        if (args.added != null && placedObject == null)
        {
            Debug.Log(args.added.Count);
            ARPlane arPlane = args.added[0];
            placedObject = Instantiate(placedPrefab, arPlane.transform.position, Quaternion.identity);
            // Stop listening for further plane changes once placed
            if (placedObject != null)
            {
                Debug.Log("Placed Object");
                arPlaneManager.planesChanged -= PlaneChanged;
            }
        }
    }

    // Dismiss the welcome panel
    private void Dismiss()
    {
        welcomePanel.SetActive(false);
    }

    // Reset character spawning when needed
    private void ResetCharacterSpawning(ARPlanesChangedEventArgs args)
    {
        args.added.Clear();
        placedObject = null;
        arPlaneManager = GetComponent<ARPlaneManager>();
        arPlaneManager.planesChanged += PlaneChanged;
    }
}
