using UnityEngine;
using System.Collections.Generic;

public class SkinPieceSpawner : MonoBehaviour
{
    public static SkinPieceSpawner Instance;

    [Header("Cài đặt")]
    [SerializeField] private float spawnInterval = 15f;
    
    // Kéo các Prefab Skin khác nhau (Xanh, Đỏ...) vào List này trong Inspector
    [SerializeField] private List<GameObject> skinPiecePrefabs; 

    private float[] lanes = { -2f, 0f, 2f };
    private float lastSpawnTime = 0f;
    private bool wasBossActive = false;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Update()
    {
        // Reset timer khi Boss vừa chết
        if (wasBossActive && !GameManger.Instance.isBossActive)
        {
            lastSpawnTime = Time.time;
        }
        wasBossActive = GameManger.Instance.isBossActive;

        if (GameManger.Instance.isBossActive) return;

        // Logic đếm giờ spawn
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            SpawnRandomSkinPiece();
            lastSpawnTime = Time.time;
        }
    }

    void SpawnRandomSkinPiece()
    {
        if (skinPiecePrefabs == null || skinPiecePrefabs.Count == 0) return;

        // 1. Chọn ngẫu nhiên 1 loại skin
        int randomIndex = Random.Range(0, skinPiecePrefabs.Count);
        GameObject selectedPrefab = skinPiecePrefabs[randomIndex];

        // 2. Tính vị trí
        int lane = PlatformTrap.lastEmptyLane;
        float baseZ = PlatformTrap.lastTrapZ;
        if (baseZ < transform.position.z) baseZ = transform.position.z + 50f;
        Vector3 pos = new Vector3(lanes[lane], 0.5f, baseZ + 3f);

        // 3. Lấy Pool và Spawn
        FastPool pool = FastPoolManager.GetPool(selectedPrefab);
        GameObject pieceObj = pool.FastInstantiate(pos, Quaternion.identity, null);

        // 4. Gán Pool cho object con
        skin_Piece pieceScript = pieceObj.GetComponent<skin_Piece>();
        if (pieceScript != null)
        {
            pieceScript.SetPool(pool);
        }
    }
}