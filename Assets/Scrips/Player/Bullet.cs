using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    private void OnEnable()
    {
        Invoke(nameof(Despawn), 5f);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Trap"))
        {
            Despawn();

        }
    }
    void Despawn()
    {
        CancelInvoke();
        GameManger.Instance.objectPool.DestroyBullet(gameObject);
    }
}
