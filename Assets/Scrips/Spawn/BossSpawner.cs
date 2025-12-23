// File: BossSpawner.cs
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public bool isMiniBoss = false;
    public static BossSpawner Instance;

    public Transform spawnPoint;
    public float respawnDelay = 60f;

    private bool bossAlive = false;
    private float lastBossDeathTime;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        lastBossDeathTime = Time.time;
    }

    void Update()
    {
        // Kiểm tra điều kiện thời gian để spawn Boss
        if (!bossAlive && Time.time - lastBossDeathTime >= respawnDelay)
        {
            isMiniBoss = true;
            SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        // 1. Báo cho cả game biết Boss đã xuất hiện -> Ngừng spawn Platform/Coin
        GameManger.Instance.isBossActive = true;

        // 2. Mở thanh máu
        UIManager.Instance.OpenUI<UIBossHp>();

        // 3. Spawn Boss
        if (isMiniBoss)
        {
            GameManger.Instance.objectPool.GetBoss(
                spawnPoint.position,
                Quaternion.identity,
                null
            );
            isMiniBoss = false;
        }

        bossAlive = true;
        Debug.Log("Boss Spawned! Stop Platform Spawning.");
    }

    public void OnBossDefeated()
    {
        bossAlive = false;
        lastBossDeathTime = Time.time;

        // QUAN TRỌNG: Báo cho game biết Boss đã chết -> Tiếp tục spawn Platform/Coin
        GameManger.Instance.isBossActive = false;

        Debug.Log("Boss Defeated - Respawn after 60s - Resume Platform Spawning");
    }
}