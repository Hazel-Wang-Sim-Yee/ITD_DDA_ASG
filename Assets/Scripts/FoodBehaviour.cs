/*
* Author: Hazel
* Date: 2025-11-12
* Description: Handles the behaviour of food items in the game.
*/
using System.Threading.Tasks;
using UnityEngine;
using System.Threading;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public class FoodBehaviour : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable; // Reference to the XR Grab Interactable component
    public bool Eatable; // Indicates if the food item is eatable

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
        // Initially, the food item is not eatable
        Eatable = false;
    }

    // Called when the food item is released
    public void OnSelectExited(SelectExitEventArgs args)
    {
        Eatable = true;
    }

    // Called when the food item is grabbed
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        Eatable = false;
    }
}
