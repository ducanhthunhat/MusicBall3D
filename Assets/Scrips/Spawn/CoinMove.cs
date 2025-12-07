using UnityEngine;

public class CoinMove : MonoBehaviour
{
    void Update()
    {
        float speed = GameSpeedManager.Instance.CurrentSpeed;
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z < -18f)
            PoolManager.Instance.Despawn(gameObject);

    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
            Despawn();
    }

    void Despawn()
    {
        CancelInvoke();
        PoolManager.Instance.Despawn(gameObject);
    }
}
