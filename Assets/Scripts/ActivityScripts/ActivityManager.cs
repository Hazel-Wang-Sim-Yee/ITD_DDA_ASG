/*
* Author: Hazel
* Date: 2025-12-08
* Description: Manages activity-related functionalities such as spawning candies.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ActivityManager : MonoBehaviour
{
    public static ActivityManager instance; // Singleton instance

    [SerializeField]
    public List<GameObject> candyPrefabs = null; // List of candy prefabs to spawn

    [SerializeField]
    public GameObject playerPrefab = null; // Player prefab

    private CandySpawnHelper spawnHelper; // Helper for spawning candies

    private readonly float spawningSpeed = 1f; // Speed of spawning candies

    // Initialize the Activity Manager
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        spawnHelper = gameObject.AddComponent<CandySpawnHelper>();
    }

    // Spawn starting candies at the beginning of the game
    public void SpawnStartCandies()
    {
        Debug.Log("Spawning Starting Candies");
        StartCoroutine(SpawnCandy());
    }

    // Coroutine to spawn candies at regular intervals
    private IEnumerator SpawnCandy()
    {
        while (StateMachine.instance.currentGameState == GameStates.Playing)
        {
            yield return new WaitForSeconds(spawningSpeed);
            SpawnTwoOrThreeCandies();
        }
    }

    // Spawn two or three candies
    private void SpawnTwoOrThreeCandies()
    {
        for (int i = 0; i < Random.Range(2, 3); i++)
        {
            spawnHelper.SpawnCandies();
        }
    }

    // Hide all candies in the scene
    public void HideCandies()
    {
        StopAllCoroutines();
        foreach (GameObject candy in GameObject.FindGameObjectsWithTag("Food"))
        {
            candy.GetComponent<FallingCandy>().Hide();
        }
    }
}
