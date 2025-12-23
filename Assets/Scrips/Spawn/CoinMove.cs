using UnityEngine;

public class CoinMove : MonoBehaviour
{
    void Update()
    {
        float speed = GameSpeedManager.Instance.CurrentSpeed;
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z < -18f)
            GameManger.Instance.objectPool.DestroyCoin(gameObject);

    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
            Despawn();
    }

    void Despawn()
    {
        CancelInvoke();
        GameManger.Instance.objectPool.DestroyCoin(gameObject);
    }
}
