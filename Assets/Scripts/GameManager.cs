/*
* Author: Hazel
* Date: 2025-11-12
* Description: Manages the overall game state and character behaviour.
*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance

    public Slider HungerSlider; // Reference to the Hunger UI slider
    public Slider SleepinessSlider; // Reference to the Sleepiness UI slider
    public Slider CleanlinessSlider; // Reference to the Cleanliness UI slider

    public Button SleepButton; // Reference to the Sleep button

    public Button ActionMenuButton; // Reference to the Action Menu button

    public Button ExitButton; // Reference to the Exit button

    [SerializeField]
    public Sprite bedDay; // Sprite for bed during the day

    [SerializeField]
    public Sprite bedNight; // Sprite for bed during the night
    public string userId; // User ID of the player

    [SerializeField]
    GameObject Character; // Reference to the character GameObject
    [SerializeField]
    public string BonName = "BonNui"; // Name of the character
    [SerializeField]
    float Awakeness = 100; // Awakeness level of the character
    [SerializeField]
    public float Cleanliness = 100; // Cleanliness level of the character
    [SerializeField]
    public float Fullness = 100; // Fullness level of the character
    [SerializeField]
    public float Recreation = 100; // Recreation level of the character
    [SerializeField]
    string currentState = "HappyCharacter"; // Current state of the character
    [SerializeField]
    public bool isSleeping = false; // Indicates if the character is sleeping
    [SerializeField]
    public Material happyMaterial; // Material for happy character
    [SerializeField]
    public Material unhappyMaterial; // Material for unhappy character

    public Transform PlayerCamera; // Reference to the player's camera

    // Assign values and set up the game manager
    void Start()
    {
        isSleeping = false; // Initialize sleeping state
        PlayerCamera = Camera.main.transform; // Get the main camera transform
        Character.transform.position = new Vector3(Character.transform.position.x, Character.transform.position.y + 0.5f, Character.transform.position.z); // Adjust character position
        StartCoroutine(HappyCharacter()); // Start the happy character coroutine
        ActionMenuButton = GameObject.Find("ActionMenuButton").GetComponent<Button>(); // Find and assign the Action Menu button
        ActionMenuButton.onClick.AddListener(MoveToActivityScene); // Add listener to the Action Menu button
        ExitButton = GameObject.Find("LogOut").GetComponent<Button>(); // Find and assign the Exit button
        ExitButton.onClick.AddListener(OnExitButtonClick); // Add listener to the Exit button
        SleepButton = GameObject.Find("SleepButton").GetComponent<Button>(); // Find and assign the Sleep button
        SleepButton.image.sprite = bedDay; // Set initial sprite for Sleep button
        SleepButton.onClick.AddListener(OnButtonClick); // Add listener to the Sleep button
        HungerSlider = GameObject.Find("Hunger").GetComponent<Slider>(); // Find and assign the Hunger slider
        SleepinessSlider = GameObject.Find("Sleepiness").GetComponent<Slider>(); // Find and assign the Sleepiness slider
        CleanlinessSlider = GameObject.Find("Cleanliness").GetComponent<Slider>(); // Find and assign the Cleanliness slider
        HungerSlider.maxValue = 100; // Set max value for Hunger slider
        SleepinessSlider.maxValue = 100; // Set max value for Sleepiness slider
        CleanlinessSlider.maxValue = 100; // Set max value for Cleanliness slider
        userId = StatManager.instance.UserId; // Get user ID from StatManager
        BonName = StatManager.instance.BonName; // Get character name from StatManager
        Recreation = StatManager.instance.Recreation; // Get recreation level from StatManager
        Cleanliness = StatManager.instance.Cleanliness; // Get cleanliness level from StatManager
        Fullness = StatManager.instance.Fullness; // Get fullness level from StatManager
        Awakeness = StatManager.instance.Awakeness; // Get awakeness level from StatManager
    }

    // Update character behaviour and UI each frame
    void Update()
    {
        // Make the character face the player camera
        if (Character == null || PlayerCamera == null) return;

        Character.transform.LookAt(
            new Vector3(PlayerCamera.position.x, Character.transform.position.y, PlayerCamera.position.z)
        );

        // Update the UI sliders with current stats
        if (HungerSlider != null) HungerSlider.value = Fullness;
        if (SleepinessSlider != null) SleepinessSlider.value = Awakeness;
        if (CleanlinessSlider != null) CleanlinessSlider.value = Cleanliness;
        

    }

    // Handle scene loading events
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "SampleScene") return; // your game scene
    }
    
    // Decrease character stats every second
    void EverySecond()
    {
        if (Awakeness > 0 || Cleanliness > 0 || Fullness > 0 || Recreation > 0)
        {
            if (Awakeness > 0)
            {
                Awakeness -= 1;
            }
            if (Cleanliness > 0)
            {
                Cleanliness -= 1;
            }
            if (Fullness > 0)
            {
                Fullness -= 1;
            }
            if (Recreation > 0)
            {
                Recreation -= 1;
            }
        }

    }

    // Handle Sleep button click to toggle sleeping state
    public void OnButtonClick()
    {
        isSleeping = !isSleeping;
        Debug.Log("isSleeping: " + isSleeping);
        // Change character state based on sleeping status
        if (isSleeping)
        {
            Debug.Log("Sleep button is " + SleepButton);
            Debug.Log("BedNight sprite" + bedNight);
            StartCoroutine(StateChanger("Sleeping"));
        }
        else
        {
            StartCoroutine(StateChanger("HappyCharacter"));
        }
    }

    // Handle Exit button click to log out and return to login scene
    public void OnExitButtonClick()
    {
        // Update player stats in StatManager before logging out
        StatManager.instance.updateDataFromPlayer(userId, Recreation, Cleanliness, Fullness);
        SceneManager.LoadScene("LogInPage");
    }

    // Move to the activity scene
    public void MoveToActivityScene()
    {
        StatManager.instance.UpdateAwakeness();
        SceneManager.LoadScene("Activity");
    }

    // Handle character interactions with triggers
    void OnTriggerStay(Collider other)
    {
        // Check for food interaction
        if (other.gameObject.CompareTag("Food"))
        {
            StartCoroutine(StateChanger("IdentifiedFood"));
            if (other.gameObject.GetComponent<FoodBehaviour>().Eatable == true)
            {
                other.gameObject.SetActive(false);
                StartCoroutine(StateChanger("EatingFood"));
            }
        }

        // Check for shower interaction
        if (other.gameObject.CompareTag("Shower"))
        {
            if (other.gameObject.GetComponent<ShowerBehaviour>().ShowerOn == true)
            {
                StartCoroutine(StateChanger("Showering"));
            }
        }
    }

    // Change the current state of the character
    IEnumerator StateChanger(string newState)
    {
        // If already in the desired state, do nothing
        if (currentState == newState)
        {
            yield break; // Already in the desired state
        }

        currentState = newState;

        StartCoroutine(currentState); // Start the coroutine for the new state
    }

    // Coroutine for the happy character state
    IEnumerator HappyCharacter()
    {
        // If the character is sleeping, switch to sleeping state
        if (isSleeping)
        {
            StartCoroutine(StateChanger("Sleeping"));
            yield break;
        }

        // Loop while in happy character state
        while (currentState == "HappyCharacter")
        {
            EverySecond();
            // Logic for happy character
            Character.GetComponent<Renderer>().material = happyMaterial; // Example: Change character color to green

            // If any stat falls below a threshold, switch to unhappy state
            if (Awakeness < 50 || Cleanliness < 50 || Fullness < 50 || Recreation < 50)
            {
                StartCoroutine(StateChanger("UnhappyCharacter"));
                yield break; // Exit this coroutine
            }
            yield return new WaitForSeconds(1f);
        }
    }

    // Coroutine for the unhappy character state
    IEnumerator UnhappyCharacter()
    {
        while (currentState == "UnhappyCharacter")
        {
            EverySecond();
            // Logic for unhappy character
            Character.GetComponent<Renderer>().material = unhappyMaterial; // Example: Change character color to red

            // If all stats are above a threshold, switch to happy state
            if (Awakeness >= 50 && Cleanliness >= 50 && Fullness >= 50 && Recreation >= 50)
            {
                StartCoroutine(StateChanger("HappyCharacter"));
                yield break; // Exit this coroutine
            }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator IdentifiedFood()
    {
        while (currentState == "IdentifiedFood")
        {
            // Logic for identified food state
            Character.GetComponent<Renderer>().material.color = Color.yellow; // Example: Change character color to yellow

            // After some time, return to happy state
            yield return new WaitForSeconds(5f);
            StartCoroutine(StateChanger("HappyCharacter"));
            yield break; // Exit this coroutine
        }
    }

    IEnumerator EatingFood()
    {
        if (currentState == "EatingFood")
        {
            // Logic for eating food state
            Character.GetComponent<Renderer>().material.color = Color.blue; // Example: Change character color to blue

            // After eating, increase fullness and return to happy state
            Fullness = Mathf.Min(Fullness + 30, 100);
            Cleanliness = Mathf.Max(Cleanliness - 10, 0); // Eating might reduce cleanliness
            yield return new WaitForSeconds(3f);
            StartCoroutine(StateChanger("HappyCharacter"));
            yield break; // Exit this coroutine
        }
    }

    IEnumerator Showering()
    {
        if (currentState == "Showering")
        {
            // Logic for showering state
            Character.GetComponent<Renderer>().material.color = Color.cyan; // Example: Change character color to cyan

            // After showering, increase cleanliness and return to happy state
            Cleanliness = Mathf.Min(Cleanliness + 10, 100);
            Recreation = Mathf.Max(Recreation - 2f, 0); // Showering might reduce recreation slightly
            yield return new WaitForSeconds(2f);
            StartCoroutine(StateChanger("HappyCharacter"));
            yield break; // Exit this coroutine
        }
    }

    IEnumerator Sleeping()
    {
        if (currentState == "Sleeping")
        {
            // Logic for sleeping state
            Character.GetComponent<Renderer>().material.color = Color.magenta; // Example: Change character color to magenta
            SleepButton.image.sprite = bedNight;

            // After sleeping, increase awakeness and return to happy state
            Awakeness = Mathf.Min(Awakeness + 20, 100);
            Fullness = Mathf.Max(Fullness - 2f, 0); // Sleeping might reduce fullness slightly
            yield return new WaitForSeconds(2f);
            StartCoroutine(StateChanger("HappyCharacter"));
            yield break; // Exit this coroutine
        }
    }


}