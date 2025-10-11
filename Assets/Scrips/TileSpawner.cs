using UnityEngine;
using System.Collections;
using System;
public class TileSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float timeSpawn = 2f;
    [SerializeField] private float minX = 0f;
    [SerializeField] private float maxX = 10f;
    public static event Action OnTileSpawner;
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnTile();
            yield return new WaitForSeconds(timeSpawn);
        }
    }
    public void SpawnTile()
    {
        GameObject tile = PoolManager.Instance.GetObject();
        Vector3 spawnPos = spawnPoint.position; spawnPos.x = UnityEngine.Random.Range(minX, maxX);
        tile.transform.position = spawnPos; tile.SetActive(true);
        OnTileSpawner?.Invoke();
    }
}