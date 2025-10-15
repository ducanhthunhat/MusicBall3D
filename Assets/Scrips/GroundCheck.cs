using UnityEngine;
using System;

public class GroundCheck : MonoBehaviour
{
    public static event Action OnFall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnFall?.Invoke();
        }
    }

}
