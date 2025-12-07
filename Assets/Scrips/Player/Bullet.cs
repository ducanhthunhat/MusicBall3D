using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    private void OnEnable()
    {
        Invoke(nameof(Despawn), 3f);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
        PoolManager.Instance.Despawn(gameObject);
    }
}
