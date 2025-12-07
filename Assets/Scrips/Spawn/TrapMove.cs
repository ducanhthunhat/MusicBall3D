using UnityEngine;
using DG.Tweening;

public class TrapMove : MonoBehaviour
{
    [SerializeField] private float despawnZ = -18f;
    [SerializeField] private float HpTrap = 3f;
    private void Update()
    {
        float speed = GameSpeedManager.Instance.CurrentSpeed;

        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z < despawnZ)
        {
            PoolManager.Instance.Despawn(gameObject);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Game Over");
            UIManager.Instance.OpenUI<UIGameOver>();
            UIManager.Instance.PauseGame();
        }
        else if (col.CompareTag("Bullet"))
        {
            GetComponent<TrapHitEffect>()?.PlayHitEffect();
            PoolManager.Instance.Despawn(col.gameObject);
            HpTrap -= 1f;
            if (HpTrap <= 0f)
            {
                PoolManager.Instance.Despawn(gameObject);
            }
        }
    }
    private void OnDisable()
    {
        DOTween.Kill(gameObject);
    }
}
