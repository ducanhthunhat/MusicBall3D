using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformTrap : MonoBehaviour
{
    private float[] lanes = { -2f, 0f, 2f };

    public static int lastEmptyLane = 1;
    public static float lastTrapZ = 0f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {

            if (GameManger.Instance.isBossActive)
            {
                yield return null;
                continue;
            }
            // -------------------------------

            yield return StartCoroutine(SpawnTrapsThenCoins());
            yield return new WaitForSeconds(Random.Range(1.5f, 2.3f));
        }
    }

    IEnumerator SpawnTrapsThenCoins()
    {
        List<int> laneList = new List<int> { 0, 1, 2 };

        int laneA = laneList[Random.Range(0, laneList.Count)];
        laneList.Remove(laneA);

        int laneB = laneList[Random.Range(0, laneList.Count)];
        laneList.Remove(laneB);

        int emptyLane = laneList[0];

        lastEmptyLane = emptyLane;
        lastTrapZ = transform.position.z;

        SpawnTrap(laneA);
        SpawnTrap(laneB);

        yield return null;

        int chosenLane = (Random.value > 0.5f) ? laneA : laneB;
        SpawnCoinsBehindTrap(chosenLane);
    }

    void SpawnTrap(int laneIndex)
    {
        Vector3 trapPos = new Vector3(lanes[laneIndex], 0f, transform.position.z);
        GameManger.Instance.objectPool.GetTrap(trapPos, Quaternion.identity, null);
    }

    void SpawnCoinsBehindTrap(int laneIndex)
    {
        float laneX = lanes[laneIndex];
        float trapZ = transform.position.z;

        for (int i = 0; i < 6; i++)
        {
            Vector3 pos = new Vector3(laneX, 1f, trapZ + i * 1.6f + 4.8f);
            GameManger.Instance.objectPool.GetCoinMove(pos, Quaternion.identity, null);
        }
    }
}