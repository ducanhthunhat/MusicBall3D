using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public LevelData[] levels;
    public float startZ = 5f;

    private void Awake() => Instance = this;

    public void LoadLevel(int index)
    {
        if (index < 0 || index >= levels.Length)
        {
            Debug.LogError("Level index out of range!");
            return;
        }

        StopAllCoroutines();
        StartCoroutine(SpawnTiles(levels[index]));
    }

    private IEnumerator SpawnTiles(LevelData levelData)
    {
        float spawnZ = startZ;

        foreach (var tileData in levelData.tiles)
        {
            GameObject t = PoolManager.Instance.GetObject();
            t.transform.position = new Vector3(tileData.xPosition, 0f, spawnZ);
            t.SetActive(true);

            if (!t.TryGetComponent<TileMover>(out _))
                t.AddComponent<TileMover>();

            spawnZ += tileData.distanceZ;

            yield return new WaitForSeconds(0.1f); // delay spawn tile
        }
    }
}
