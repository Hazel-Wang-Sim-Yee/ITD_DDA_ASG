using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider HungerSlider;
    [SerializeField] private UnityEngine.UI.Slider SleepinessSlider;
    [SerializeField] private UnityEngine.UI.Slider CleanlinessSlider;
    [SerializeField]
    GameObject Character;
    [SerializeField]
    float Awakeness = 100;
    [SerializeField]
    float Cleanliness = 100;
    [SerializeField]
    float Fullness = 100;
    [SerializeField]
    float Recreation = 100;
    [SerializeField]
    string currentState = "HappyCharacter";
    [SerializeField]
    public bool isSleeping = false;
    [SerializeField]
    public Material happyMaterial;
    [SerializeField]
    public Material unhappyMaterial;

    public Transform PlayerCamera;


    void Start()
    {
        PlayerCamera = Camera.main.transform;
        Character.transform.position = new Vector3(Character.transform.position.x, Character.transform.position.y + 0.5f, Character.transform.position.z);
        StartCoroutine(HappyCharacter());
        Debug.Log("Game Started");
    }

    void Update()
    {
        Character.transform.LookAt(new Vector3(PlayerCamera.position.x, Character.transform.position.y, PlayerCamera.position.z));
        //Character.transform.position = new Vector3(PlayerCamera.position.x, Character.transform.position.y, PlayerCamera.position.z + 0.5f);
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

    public void OnButtonClick()
    {
        isSleeping = !isSleeping;
        if (isSleeping)
        {
            StartCoroutine(StateChanger("Sleeping"));
        }
        else
        {
            StartCoroutine(StateChanger("HappyCharacter"));
        }
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

            // After sleeping, increase awakeness and return to happy state
            Awakeness = Mathf.Min(Awakeness + 1, 100);
            Fullness = Mathf.Max(Fullness - 0.02f, 0); // Sleeping might reduce fullness slightly
            yield return new WaitForSeconds(0.1f);
            yield break; // Exit this coroutine
        }
    }


}