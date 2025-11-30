using UnityEngine;

public class TileMover : MonoBehaviour
{
    public float speed = 5f;    // tốc độ tile di chuyển về player
    public float destroyZ = -5f;

    void Update()
    {
        transform.Translate(0, 0, -speed * Time.deltaTime);

        if (transform.position.z < destroyZ)
            PoolManager.Instance.ReturnObject(gameObject);
    }
}
