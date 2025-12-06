using System.Threading.Tasks;
using UnityEngine;
using System.Threading;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowerBehaviour : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    public bool ShowerOn;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnSelectExited);
            grabInteractable.selectEntered.AddListener(OnSelectEntered);
        }
        ShowerOn = false;
    }
    public void OnSelectExited(SelectExitEventArgs args)
    {
        ShowerOn = false;
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        ShowerOn = true;
    }
}
