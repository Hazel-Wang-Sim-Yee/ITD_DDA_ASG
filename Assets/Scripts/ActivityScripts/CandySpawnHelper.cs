/*
* Author: Hazel
* Date: 2025-12-08
* Description: Helper class to manage candy spawning.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CandySpawnHelper : MonoBehaviour
{
    private CandyPositionHelper positionHelper; // Helper to get spawn positions

    private List<GameObject> spawnedCandies = new List<GameObject>(); // List to keep track of spawned candies

    // Initialize the Candy Spawn Helper
    private void Awake()
    {
        positionHelper = new CandyPositionHelper();
    }

    // Spawn candies either by reusing invisible ones or creating new ones
    public void SpawnCandies()
    {
        if (CandyAlreadySpawnedAndInvisible())
        {
            Reuse();
        }
        else
        {
            Spawn();
        }
    }

    // Check if there are any spawned candies that are invisible
    private bool CandyAlreadySpawnedAndInvisible()
    {
        return spawnedCandies.Exists(x => !x.activeSelf && x != null);
    }

    // Spawn a new candy
    private void Spawn()
    {
            GameObject spawnedCandy = Instantiate(getRandomCandy(), positionHelper.GetSpawnPosition(), Quaternion.identity);
            spawnedCandies.Add(spawnedCandy);
    }

    // Reuse an invisible candy by repositioning and reactivating it
    private void Reuse()
    {
        GameObject candyToReuse = spawnedCandies.Find(x => !x.activeSelf && x!= null);
        candyToReuse.SetActive(true);
        candyToReuse.transform.position = positionHelper.GetSpawnPosition();
        candyToReuse.GetComponent<FallingCandy>().Reset();
    }

    // Get a random candy prefab from the Activity Manager
    GameObject getRandomCandy()
    {
        return ActivityManager.instance.candyPrefabs[Random.Range(0, ActivityManager.instance.candyPrefabs.Count)];
    }
}
