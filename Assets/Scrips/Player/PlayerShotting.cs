using System;
using UnityEngine;
using DG.Tweening;

public class PlayerShotting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    public static event Action onPlayerShoot;

    private int clickCount = 0;
    private float clickTimer = 0f;
    private float clickThreshold = 0.2f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickCount++;

            if (clickCount == 1)
            {
                clickTimer = Time.time;
            }
            else if (clickCount == 2 && Time.time - clickTimer <= clickThreshold)
            {
                Shoot();
                clickCount = 0;
            }
        }

        if (clickCount == 1 && Time.time - clickTimer > clickThreshold)
        {
            clickCount = 0;
        }
    }

    private void Shoot()
    {
        GameObject bullet = PoolManager.Instance.Spawn(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        DOTween.Kill(bullet, complete: false);

        onPlayerShoot?.Invoke();
        Debug.Log("Player shot!");
    }
}
