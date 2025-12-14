/*
* Author: Hazel
* Date: 2025-12-11
* Description: Handles naming the creature and choosing Bon before starting the game.
*/
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class NamingAndChoosingBon : MonoBehaviour
{
    public static NamingAndChoosingBon instance; // Singleton instance

    [SerializeField]
    TMP_InputField nameInputField; // Input field for creature name

    private Button confirmNameButton; // Button to confirm name


    private Button confirmBonButton; // Button to confirm Bon choice

    [SerializeField]
    GameObject bonSelectionPanel; // Panel for Bon selection

    [SerializeField]
    GameObject Bon; // Bon GameObject

    void Start()
    {
        // Confirm Name Button setup
        confirmNameButton = GameObject.Find("ConfirmNameButton").GetComponent<Button>();
        
        confirmNameButton.onClick.AddListener(ConfirmName);
        
    }

    // Confirm Name button action
    void ConfirmName()
    {
        string playerName = nameInputField.text; // Get the entered name
        StatManager.instance.UpdateCreatureName(playerName); // Update the creature name in StatManager
        bonSelectionPanel.SetActive(true); // Show Bon selection panel
        Bon.SetActive(true); // Show Bon GameObject
        confirmBonButton = GameObject.Find("ConfirmBonButton").GetComponent<Button>(); // Get Confirm Bon Button
        confirmBonButton.onClick.AddListener(ConfirmBon); // Add listener to Confirm Bon Button
    }

    // Confirm Bon button action
    void ConfirmBon()
    {
        SceneManager.LoadScene("SampleScene"); // Load the main game scene
    }
}