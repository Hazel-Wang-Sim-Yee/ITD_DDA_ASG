using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ActivityManager : MonoBehaviour
{
    public static ActivityManager instance;

    [SerializeField]
    public List<GameObject> candyPrefabs = null;

    [SerializeField]
    public GameObject playerPrefab = null;

    private CandySpawnHelper spawnHelper;

    private readonly float spawningSpeed = 1f;

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

    public void SpawnStartCandies()
    {
        Debug.Log("Spawning Starting Candies");
        StartCoroutine(SpawnCandy());
    }

    private IEnumerator SpawnCandy()
    {
        while (StateMachine.instance.currentGameState == GameStates.Playing)
        {
            yield return new WaitForSeconds(spawningSpeed);
            SpawnTwoOrThreeCandies();
        }
    }

    private void SpawnTwoOrThreeCandies()
    {
        for (int i = 0; i < Random.Range(2, 3); i++)
        {
            spawnHelper.SpawnCandies();
        }
    }

    public void HideCandies()
    {
        StopAllCoroutines();
        foreach (GameObject candy in GameObject.FindGameObjectsWithTag("Food"))
        {
            candy.GetComponent<FallingCandy>().Hide();
        }
    }
}
