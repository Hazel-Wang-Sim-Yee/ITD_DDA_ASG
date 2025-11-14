using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject Character;
    [SerializeField]
    int Awakeness = 100;
    [SerializeField]
    int Cleanliness = 100;
    [SerializeField]
    int Fullness = 100;
    [SerializeField]
    int Recreation = 100;
    [SerializeField]
    string currentState = "HappyCharacter";

    void Start()
    {
        StartCoroutine(HappyCharacter());
        Debug.Log("Game Started");
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
        Debug.Log("The character is happy");
        while (currentState == "HappyCharacter")
        {
            EverySecond();
            // Logic for happy character
            Character.GetComponent<Renderer>().material.color = Color.green; // Example: Change character color to green

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
            Character.GetComponent<Renderer>().material.color = Color.red; // Example: Change character color to red

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
}