using System.Threading.Tasks;
using UnityEngine;
using System.Threading;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public class FoodBehaviour : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    public bool Eatable;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnSelectExited);
            grabInteractable.selectEntered.AddListener(OnSelectEntered);
        }
        Eatable = false;
    }
    public void OnSelectExited(SelectExitEventArgs args)
    {
        Eatable = true;
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        Eatable = false;
    }
}
