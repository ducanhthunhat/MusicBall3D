using UnityEngine;
using DG.Tweening;

public class Tile : MonoBehaviour
{
    [SerializeField] private float tileSpeed = 2f;
    [SerializeField] private float moveDistance = 50f;

    private void OnEnable()
    {
        transform.DOMoveZ(transform.position.z - moveDistance, moveDistance / tileSpeed)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                PoolManager.Instance.ReturnObject(gameObject);
            });
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}
