/*
* Author: Hazel
* Date: 2025-11-30
* Description: Handles the shower behaviour in the game.
*/
using System.Threading.Tasks;
using UnityEngine;
using System.Threading;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowerBehaviour : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable; // Reference to the XR Grab Interactable component
    public bool ShowerOn; // Indicates if the shower is on

    // Start is called before the first frame update
    void Start()
    { 
        // Get the XR Grab Interactable component
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        // Add event listeners for select entered and exited events
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnSelectExited);
            grabInteractable.selectEntered.AddListener(OnSelectEntered);
        }
        // Initially, the shower is off
        ShowerOn = false;
    }

    // Called when the shower is turned off
    public void OnSelectExited(SelectExitEventArgs args)
    {
        ShowerOn = false;
    }

    // Called when the shower is turned on
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        ShowerOn = true;
    }
}
