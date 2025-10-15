using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private SongData songData;

    private int currentBeatIndex = 0;

    private void Start()
    {
        if (songData == null || songData.beatTimings == null || songData.beatTimings.Length == 0)
        {
            Debug.LogError("⚠️ Chưa gán SongData hoặc dữ liệu beat trống!");
            enabled = false;
            return;
        }

        musicSource.clip = songData.songClip;
    }

    private void Update()
    {
        if (!musicSource.isPlaying || songData.beatTimings == null)
            return;

        // Kiểm tra nếu đến thời gian của beat tiếp theo
        if (currentBeatIndex < songData.beatTimings.Length &&
            musicSource.time >= songData.beatTimings[currentBeatIndex])
        {
            float xPos = songData.xPositions[currentBeatIndex];
            SpawnTile(xPos);
            currentBeatIndex++;
        }
    }

    private void SpawnTile(float xPos)
    {
        GameObject tile = PoolManager.Instance.GetObject();
        Vector3 spawnPos = spawnPoint.position;
        spawnPos.x = xPos;

        tile.transform.position = spawnPos;
        tile.SetActive(true);
    }
}
