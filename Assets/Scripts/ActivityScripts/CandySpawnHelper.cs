using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CandySpawnHelper : MonoBehaviour
{
    private CandyPositionHelper positionHelper;

    private List<GameObject> spawnedCandies = new List<GameObject>();
    private void Awake()
    {
        positionHelper = new CandyPositionHelper();
    }

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

    private bool CandyAlreadySpawnedAndInvisible()
    {
        return spawnedCandies.Exists(x => !x.activeSelf && x != null);
    }

    private void Spawn()
    {
            GameObject spawnedCandy = Instantiate(getRandomCandy(), positionHelper.GetSpawnPosition(), Quaternion.identity);
            spawnedCandies.Add(spawnedCandy);
    }

    private void Reuse()
    {
        GameObject candyToReuse = spawnedCandies.Find(x => !x.activeSelf && x!= null);
        candyToReuse.SetActive(true);
        candyToReuse.transform.position = positionHelper.GetSpawnPosition();
        candyToReuse.GetComponent<FallingCandy>().Reset();
    }

    GameObject getRandomCandy()
    {
        return ActivityManager.instance.candyPrefabs[Random.Range(0, ActivityManager.instance.candyPrefabs.Count)];
    }
}
