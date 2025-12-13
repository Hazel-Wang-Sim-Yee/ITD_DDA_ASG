using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class NamingAndChoosingBon : MonoBehaviour
{
    public static NamingAndChoosingBon instance;

    [SerializeField]
    TMP_InputField nameInputField;

    private Button confirmNameButton;


    private Button confirmBonButton;

    [SerializeField]
    GameObject bonSelectionPanel;

    [SerializeField]
    GameObject Bon;

    void Start()
    {
        confirmNameButton = GameObject.Find("ConfirmNameButton").GetComponent<Button>();
        
        confirmNameButton.onClick.AddListener(ConfirmName);
        
    }

    void ConfirmName()
    {
        string playerName = nameInputField.text;
        StatManager.instance.UpdateCreatureName(playerName);
        Debug.Log(StatManager.instance.BonName);
        bonSelectionPanel.SetActive(true);
        Bon.SetActive(true);
        confirmBonButton = GameObject.Find("ConfirmBonButton").GetComponent<Button>();
        confirmBonButton.onClick.AddListener(ConfirmBon);
    }

    void ConfirmBon()
    {
        SceneManager.LoadScene("SampleScene");
    }
}