using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private float tileSpeed = 2;

    [SerializeField] private Rigidbody rb;
    void OnValidate()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -tileSpeed);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndPool"))
        {
            PoolManager.Instance.ReturnObject(this.gameObject);
        }
    }
}
