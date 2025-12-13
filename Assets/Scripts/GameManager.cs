using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public Slider HungerSlider;
    public Slider SleepinessSlider;
    public Slider CleanlinessSlider;
    
    public Button SleepButton;

    public Button ActionMenuButton;

    [SerializeField]
    public Sprite bedDay;

    [SerializeField]
    public Sprite bedNight;
    public string userId;

    [SerializeField]
    GameObject Character;
    [SerializeField]
    string BonName = "BonNui";
    [SerializeField]
    float Awakeness = 100;
    [SerializeField]
    public float Cleanliness = 100;
    [SerializeField]
    public float Fullness = 100;
    [SerializeField]
    public float Recreation = 100;
    [SerializeField]
    string currentState = "HappyCharacter";
    [SerializeField]
    public bool isSleeping = false;
    [SerializeField]
    public Material happyMaterial;
    [SerializeField]
    public Material unhappyMaterial;

    public Transform PlayerCamera;

    /// <summary>
    /// Ensures that the GameManager persists across scenes and implements the singleton pattern.
    /// </summary>
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
/// <summary>
/// preferably dont use start because the login page will have issues because the buttons and sliders dont exist on login scene
/// </summary>
    // void Start()
    // {
    //     isSleeping = false;
    //     PlayerCamera = Camera.main.transform;
    //     Character.transform.position = new Vector3(Character.transform.position.x, Character.transform.position.y + 0.5f, Character.transform.position.z);
    //     StartCoroutine(HappyCharacter());
    //     Debug.Log("Game Started");
    //     ActionMenuButton = GameObject.Find("ActionMenuButton").GetComponent<Button>();
    //     ActionMenuButton.onClick.AddListener(MoveToActivityScene);
    //     SleepButton = GameObject.Find("SleepButton").GetComponent<Button>();
    //     SleepButton.image.sprite = bedDay;
    //     SleepButton.onClick.AddListener(OnButtonClick);
    //     HungerSlider = GameObject.Find("Hunger").GetComponent<Slider>();
    //     SleepinessSlider = GameObject.Find("Sleepiness").GetComponent<Slider>();
    //     CleanlinessSlider = GameObject.Find("Cleanliness").GetComponent<Slider>();
    //     HungerSlider.maxValue = 100;
    //     SleepinessSlider.maxValue = 100;
    //     CleanlinessSlider.maxValue = 100;
    // }

    void Update()
    {
        if (Character == null || PlayerCamera == null) return;

        Character.transform.LookAt(
            new Vector3(PlayerCamera.position.x, Character.transform.position.y, PlayerCamera.position.z)
        );

        if (HungerSlider != null) HungerSlider.value = Fullness;
        if (SleepinessSlider != null) SleepinessSlider.value = Awakeness;
        if (CleanlinessSlider != null) CleanlinessSlider.value = Cleanliness;
        
    }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "SampleScene") return; // your game scene

        InitializeGameplay();
    }
        void InitializeGameplay()
    {
        Debug.Log("Initializing gameplay");

        PlayerCamera = Camera.main.transform;
        Character = GameObject.FindWithTag("Character");

        ActionMenuButton = GameObject.Find("ActionMenuButton")?.GetComponent<Button>();
        SleepButton = GameObject.Find("SleepButton")?.GetComponent<Button>();
        HungerSlider = GameObject.Find("Hunger")?.GetComponent<Slider>();
        SleepinessSlider = GameObject.Find("Sleepiness")?.GetComponent<Slider>();
        CleanlinessSlider = GameObject.Find("Cleanliness")?.GetComponent<Slider>();

        if (SleepButton != null)
            SleepButton.onClick.AddListener(OnButtonClick);

        if (ActionMenuButton != null)
            ActionMenuButton.onClick.AddListener(MoveToActivityScene);

        StartCoroutine(HappyCharacter());
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

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
        public void UpdateCreatureStats(string userId, string name, float happiness, float cleanliness, float hunger) // used in login code
    {
        this.userId = userId;
        this.BonName = name;
        this.Recreation = happiness;
        this.Cleanliness = cleanliness;
        this.Fullness = hunger;

        Debug.Log("Creature stats updated");
    }

    public void OnButtonClick()
    {
        isSleeping = !isSleeping;
        Debug.Log("isSleeping: " + isSleeping);
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

    public void MoveToActivityScene()
    {
        SceneManager.LoadScene("Activity");
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.name + " triggered");
        if (other.gameObject.CompareTag("Food"))
        {
            Debug.Log("Character triggered with food");
            StartCoroutine(StateChanger("IdentifiedFood"));
            if (other.gameObject.GetComponent<FoodBehaviour>().Eatable == true)
            {
                Debug.Log("Character is eating the food");
                other.gameObject.SetActive(false);
                StartCoroutine(StateChanger("EatingFood"));
            }
        }

        if (other.gameObject.CompareTag("Shower"))
        {
            if (other.gameObject.GetComponent<ShowerBehaviour>().ShowerOn == true)
            {
                Debug.Log("Character triggered with shower");
                StartCoroutine(StateChanger("Showering"));
            }
        }
    }

    IEnumerator StateChanger(string newState)
    {
        Debug.Log("Changing state to: " + newState);
        if (currentState == newState)
        {
            yield break; // Already in the desired state
        }

        currentState = newState;

        StartCoroutine(currentState);
        Debug.Log("Current State: " + currentState);
    }

    IEnumerator HappyCharacter()
    {
        if(isSleeping)
        {
            StartCoroutine(StateChanger("Sleeping"));
            yield break;
        }
        //Debug.Log("The character is happy");
        while (currentState == "HappyCharacter")
        {
            EverySecond();
            // Logic for happy character
            Character.GetComponent<Renderer>().material = happyMaterial; // Example: Change character color to green

            // If any stat falls below a threshold, switch to unhappy state
            if (Awakeness < 50 || Cleanliness < 50 || Fullness < 50 || Recreation < 50)
            {
                Debug.Log("Character is becoming unhappy due to low stats");
                StartCoroutine(StateChanger("UnhappyCharacter"));
                yield break; // Exit this coroutine
            }
            yield return new WaitForSeconds(1f);
        }
    }

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
                Debug.Log("Character is becoming happy again due to good stats");
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
            //Debug.Log("Character has identified food");
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
            Debug.Log("Character is eating food");
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
            Debug.Log("Character is showering");
            // Logic for showering state
            Character.GetComponent<Renderer>().material.color = Color.cyan; // Example: Change character color to cyan

            // After showering, increase cleanliness and return to happy state
            Cleanliness = Mathf.Min(Cleanliness + 1, 100);
            Recreation = Mathf.Max(Recreation - 0.02f, 0); // Showering might reduce recreation slightly
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(StateChanger("HappyCharacter"));
            yield break; // Exit this coroutine
        }
    }

    IEnumerator Sleeping()
    {
        if (currentState == "Sleeping")
        {
            Debug.Log("Character is sleeping");
            // Logic for sleeping state
            Character.GetComponent<Renderer>().material.color = Color.magenta; // Example: Change character color to magenta
            SleepButton.image.sprite = bedNight;

            // After sleeping, increase awakeness and return to happy state
            Awakeness = Mathf.Min(Awakeness + 1, 100);
            Fullness = Mathf.Max(Fullness - 0.02f, 0); // Sleeping might reduce fullness slightly
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(StateChanger("HappyCharacter"));
            yield break; // Exit this coroutine
        }
    }


}