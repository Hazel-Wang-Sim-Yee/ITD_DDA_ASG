using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CandyPositionHelper : MonoBehaviour
{
    Vector3 lastSpawnPoint;

    private readonly float SpawnMinDistance = 1f;

    public Vector3 GetSpawnPosition()
    {
        Vector3 newSpawnPoint = new Vector3(Random.Range(ScreenHelper.ScreenLeft, ScreenHelper.ScreenRight),
            ScreenHelper.ScreenTop + Random.Range(1, 3), 2);
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
