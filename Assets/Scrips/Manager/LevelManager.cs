using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Cơ sở dữ liệu level")]
    public LevelDatabase levelDatabase;
    public static LevelManager Instance { get; private set; }

    private GameObject currentLevelInstance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(int index)
    {
        if (levelDatabase == null || levelDatabase.allLevels.Length == 0)
        {
            Debug.LogError("Chưa gán LevelDatabase hoặc chưa có level!");
            return;
        }

        if (index < 0 || index >= levelDatabase.allLevels.Length)
        {
            Debug.LogWarning("Level index không hợp lệ!");
            return;
        }

        // Xóa level cũ nếu có
        if (currentLevelInstance != null)
            Destroy(currentLevelInstance);

        // Lấy dữ liệu level
        LevelData data = levelDatabase.allLevels[index];
        Debug.Log($"Đang load: {data.levelName}");

        // Tạo level từ prefab
        currentLevelInstance = Instantiate(data.levelPrefab, Vector3.zero, Quaternion.identity, null);
    }
    public void UnloadCurrentLevel()
    {
        if (currentLevelInstance != null)
        {
            Destroy(currentLevelInstance);
            currentLevelInstance = null;
            Debug.Log("Đã tắt level hiện tại và trở về menu.");
        }
        else
        {
            Debug.LogWarning("Không có level nào đang được load để tắt!");
        }
    }

    public int LevelCount => levelDatabase?.allLevels.Length ?? 0;
}