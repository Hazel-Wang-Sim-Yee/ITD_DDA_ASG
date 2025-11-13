using UnityEngine;
using System.Collections;

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

            // Decrease each stat by a certain amount
            while (Awakeness > 0)
            {
                Awakeness -= 1;
                yield return new WaitForSeconds(1f); // Wait for 1 second before decreasing again
            }
            while (Cleanliness > 0)
            {
                Cleanliness -= 1;
                yield return new WaitForSeconds(1f); // Wait for 1 second before decreasing again
            }
            while (Fullness > 0)
            {
                Fullness -= 1;
                yield return new WaitForSeconds(1f); // Wait for 1 second before decreasing again
            }
            while (Recreation > 0)
            {
                Recreation -= 1;
                yield return new WaitForSeconds(1f); // Wait for 1 second before decreasing again
            }

            // Logic for happy character
            Character.GetComponent<Renderer>().material.color = Color.green; // Example: Change character color to green

            // If any stat falls below a threshold, switch to unhappy state
            if (Awakeness < 50 || Cleanliness < 50 || Fullness < 50 || Recreation < 50)
            {
                StartCoroutine(StateChanger("UnhappyCharacter"));
                yield break; // Exit this coroutine
            }
        }
    }

    IEnumerator UnhappyCharacter()
    {
        while (currentState == "UnhappyCharacter")
        {
            while (Awakeness > 0)
            {
                Awakeness -= 1;
            }
            while (Cleanliness > 0)
            {
                Cleanliness -= 1;
            }
            while (Fullness > 0)
            {
                Fullness -= 1;
            }
            while (Recreation > 0)
            {
                Recreation -= 1;
            }

            // Logic for unhappy character
            Character.GetComponent<Renderer>().material.color = Color.red; // Example: Change character color to red

            // If all stats are above a threshold, switch to happy state
            if (Awakeness >= 50 && Cleanliness >= 50 && Fullness >= 50 && Recreation >= 50)
            {
                StartCoroutine(StateChanger("HappyCharacter"));
                yield break; // Exit this coroutine
        }
    }
}}