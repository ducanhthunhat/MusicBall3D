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
            GameManger.Instance.objectPool.DestroyTrap(gameObject);
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
            GetComponent<TrapHitVFX_Strong>()?.PlayHit();

            GameManger.Instance.objectPool.DestroyBullet(col.gameObject);
            HpTrap--;
            if (HpTrap <= 0f)
            {
                GameManger.Instance.objectPool.DestroyTrap(gameObject);
                HpTrap = 10f;
            }
        }
    }
    private void OnDisable()
    {
        DOTween.Kill(gameObject);
    }
}
