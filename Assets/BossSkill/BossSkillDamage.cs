using UnityEngine;

public class BossSkillDamage : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // gây damage player
            Debug.Log("Player Hit!");
            UIManager.Instance.OpenUI<UIGameOver>();
        }
    }
}
