using UnityEngine;
using System.Collections;

public class MiniBoss : MonoBehaviour
{
    [Header("HP")]
    public int hp = 20;
    public int maxHP = 20;
    private bool isDead = false;

    [Header("Skill")]
    public float skillInterval = 2f;
    public float warningTime = 1f;

    private float[] lanes = { -2f, 0f, 2f };

    private UIBossHp bossHpUI;

    void OnEnable()
    {
        hp = maxHP;
        isDead = false;


        bossHpUI = UIManager.Instance.GetUI<UIBossHp>();

        if (bossHpUI != null)
        {
            bossHpUI.SetHP(1f);
        }
        else
        {
            Debug.LogError("Chua tim thay UIBossHp trong UIManager!");
        }

        StartCoroutine(SkillRoutine());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator SkillRoutine()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(skillInterval);
            yield return SpawnSkillWithWarning();
        }
    }

    IEnumerator SpawnSkillWithWarning()
    {
        int lane = Random.Range(0, lanes.Length);

        if (GameManger.Instance == null || GameManger.Instance.objectPool == null) yield break;

        Vector3 pos = new Vector3(
            lanes[lane],
            0.5f,
            -transform.position.z
        );

        var warning = GameManger.Instance.objectPool
            .GetBossSkillWarning(pos, Quaternion.identity, null);

        yield return new WaitForSeconds(warningTime);

        if (warning != null)
            GameManger.Instance.objectPool.DestroyBossSkillWarning(warning.gameObject);

        var skill = GameManger.Instance.objectPool
            .GetBossSkill(pos, Quaternion.identity, null);

        yield return new WaitForSeconds(3f);

        if (skill != null)
            GameManger.Instance.objectPool.DestroyBossSkill(skill.gameObject);
    }

    // ---------------- DAMAGE ----------------
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
            other.gameObject.SetActive(false);
        }
    }

    void TakeDamage(int dmg)
    {
        if (isDead) return;

        hp -= dmg;

        if (bossHpUI != null)
        {
            bossHpUI.SetHP((float)hp / maxHP);
        }

        if (hp <= 0) Die();
    }

    void Die()
    {
        isDead = true;
        BossSpawner.Instance.OnBossDefeated();

        // FIX 3: Đóng UI Boss khi Boss chết
        UIManager.Instance.CloseUI<UIBossHp>(0.5f);

        GameManger.Instance.objectPool.DestroyBoss(gameObject);
    }
}