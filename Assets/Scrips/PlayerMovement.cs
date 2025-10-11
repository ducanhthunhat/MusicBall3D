using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float control = 10f;
    // [SerializeField] private float speedMove = 5f;
    [SerializeField] private float addForce = 10f;
    private Rigidbody rb;

    private void OnValidate()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void OnEnable()
    {
        TileSpawner.OnTileSpawner += FallStep;
    }
    public void OnDisable()
    {
        TileSpawner.OnTileSpawner -= FallStep;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(horizontal * control, rb.velocity.y, rb.velocity.z);
    }
    private void FallStep()
    {
        rb.useGravity = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Tile"))
        {
            rb.velocity = new Vector3(rb.velocity.x, addForce, rb.velocity.z);
            rb.useGravity = false;
        }
    }
}
