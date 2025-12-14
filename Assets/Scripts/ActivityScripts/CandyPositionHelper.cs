/*
* Author: Hazel
* Date: 2025-12-08
* Description: Helper class to determine candy spawn positions.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CandyPositionHelper : MonoBehaviour
{
    Vector3 lastSpawnPoint; // To keep track of the last spawn point

    private readonly float SpawnMinDistance = 1f; // Minimum distance between spawns

    // Get a valid spawn position for a candy
    public Vector3 GetSpawnPosition()
    {
        Vector3 newSpawnPoint = new Vector3(Random.Range(ScreenHelper.ScreenLeft, ScreenHelper.ScreenRight),
            ScreenHelper.ScreenTop + Random.Range(1, 3), 2);
        // Ensure the new spawn point is sufficiently far from the last spawn point
        if (Vector3.Distance(newSpawnPoint,lastSpawnPoint) > SpawnMinDistance)
        {
            lastSpawnPoint = newSpawnPoint;
            return newSpawnPoint;
        }
        else
        {
            return GetSpawnPosition();
        }
    }
}
