using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class AutoPlacementofCharacter : MonoBehaviour
{
    [SerializeField]
    private GameObject welcomePanel;

    [SerializeField]
    private GameObject placedPrefab;

    private GameObject placedObject;
    [SerializeField]
    private Button dismissButton;
    [SerializeField]
    private ARPlaneManager arPlaneManager;
    void Awake()
    {
        dismissButton.onClick.AddListener(Dismiss);
        arPlaneManager = GetComponent<ARPlaneManager>();
        arPlaneManager.planesChanged += PlaneChanged;
    }

    private void PlaneChanged(ARPlanesChangedEventArgs args)
    {
        if (args.added != null && placedObject == null)
        {
            Debug.Log(args.added.Count);
            ARPlane arPlane = args.added[0];
            placedObject = Instantiate(placedPrefab, arPlane.transform.position, Quaternion.identity);
            if (placedObject != null)
            {
                Debug.Log("Placed Object");
                arPlaneManager.planesChanged -= PlaneChanged;
            }
        }
    }

    private void Dismiss()
    {
        welcomePanel.SetActive(false);
    }
}
