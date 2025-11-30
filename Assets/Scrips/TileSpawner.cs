using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public LevelData levelData;      // Level hiện tại
    public float startZ = 10f;       // Z bắt đầu spawn
    public float tileSpeed = 5f;     // tốc độ di chuyển tile về player
    public BallPlayerController ballController;

    private List<Transform> spawnedTiles = new List<Transform>();

    void Start()
    {
        StartCoroutine(SpawnTiles());
    }

    private IEnumerator SpawnTiles()
    {
        float spawnZ = startZ;

        foreach (var tileData in levelData.tiles)
        {
            // Lấy tile từ PoolManager
            GameObject tile = PoolManager.Instance.GetObject();
            tile.transform.position = new Vector3(tileData.xPosition, 0f, spawnZ);
            tile.SetActive(true);

            if (!tile.TryGetComponent<TileMover>(out TileMover mover))
                mover = tile.AddComponent<TileMover>();

            mover.speed = tileSpeed;

            spawnedTiles.Add(tile.transform);

            // cập nhật spawnZ cho tile tiếp theo dựa trên distanceZ
            spawnZ += tileData.distanceZ;

            yield return null; // hoặc WaitForSeconds nếu muốn delay spawn
        }

        // gửi danh sách tile cho BallPlayerController
        if (ballController != null)
            ballController.SetTiles(spawnedTiles.ToArray());
    }
}
