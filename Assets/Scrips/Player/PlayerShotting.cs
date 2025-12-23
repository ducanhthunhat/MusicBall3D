using System;
using UnityEngine;
using DG.Tweening;

public class PlayerShotting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    private float nextFireTime = 0f;
    [SerializeField] private float fireRate = 0.15f;

    public static event Action onPlayerShoot;


    void Update()
    {
        if (Input.GetMouseButton(0)) // GIỮ CHUỘT
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private void Shoot()
    {
        if (firePoint == null)
        {
            Debug.LogError("FirePoint NULL");
            return;
        }

        if (GameManger.Instance == null || GameManger.Instance.objectPool == null)
        {
            Debug.LogError("GameManger or ObjectPool NULL");
            return;
        }

        var bullet = GameManger.Instance.objectPool.GetBullet(
            firePoint.position,
            firePoint.rotation,
            null
        );

        if (bullet == null)
        {
            Debug.LogError("Bullet NULL");
            return;
        }

        DOTween.Kill(bullet);
        onPlayerShoot?.Invoke();
    }




    private void OnDisable()
    {
        DOTween.Kill(gameObject);
    }
}
