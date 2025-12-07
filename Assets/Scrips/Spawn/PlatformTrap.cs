using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformTrap : MonoBehaviour
{
    private float[] lanes = { -2f, 0f, 2f };
    [SerializeField] private GameObject trapPrefab;
    [SerializeField] private GameObject coinPrefab;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        // Bắt đầu spawn ngay lập tức
        while (true)
        {
            SpawnTrapsAndCoins();

            yield return new WaitForSeconds(Random.Range(1.8f, 4f));
        }
    }

    void SpawnTrapsAndCoins()
    {
        List<int> laneList = new List<int> { 0, 1, 2 };

        int laneA = laneList[Random.Range(0, laneList.Count)];
        laneList.Remove(laneA);

        int laneB = laneList[Random.Range(0, laneList.Count)];
        laneList.Remove(laneB);

        // LANE TRỐNG LÀ laneList[0]
        int emptyLane = laneList[0];

        // SPWAN TRAP 2 LANE
        Vector3 posA = new Vector3(lanes[laneA], 0, transform.position.z);
        PoolManager.Instance.Spawn(trapPrefab, posA, Quaternion.identity);

        Vector3 posB = new Vector3(lanes[laneB], 0, transform.position.z);
        PoolManager.Instance.Spawn(trapPrefab, posB, Quaternion.identity);

        // SPAWN COIN VÀO LANE TRỐNG
        SpawnCoinsInLane(emptyLane);
    }

    void SpawnCoinsInLane(int laneIndex)
    {
        float laneX = lanes[laneIndex];
        float baseZ = transform.position.z;

        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = new Vector3(laneX, 1f, baseZ + i * 2f);
            PoolManager.Instance.Spawn(coinPrefab, pos, Quaternion.identity);
        }
    }
}
