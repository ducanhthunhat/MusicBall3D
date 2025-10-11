using UnityEngine;
using DG.Tweening;

public class InfiniteGroundDOTween : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform ground1;
    [SerializeField] private Transform ground2;

    [Header("Settings")]
    [SerializeField] private float groundLength = 3f;
    [SerializeField] private float moveDuration = 3f; // thời gian để di chuyển 1 đoạn
    [SerializeField] private Ease moveEase = Ease.Linear;

    private void Start()
    {
        // Đặt vị trí ban đầu
        ground1.position = Vector3.zero;
        ground2.position = new Vector3(0, 0, groundLength);

        // Bắt đầu tween cho mỗi ground
        StartLoopMove(ground1);
        StartLoopMove(ground2, groundLength / moveDuration * 0.5f); // lệch pha một nửa chu kỳ
    }

    private void StartLoopMove(Transform ground, float delay = 0f)
    {
        // Tween di chuyển ground về trục âm Z
        ground.DOMoveZ(ground.position.z - groundLength, moveDuration)
            .SetEase(moveEase)
            .SetDelay(delay)
            .OnComplete(() =>
            {
                // Khi đi hết một đoạn, dịch ra trước và tween lại
                ground.position = new Vector3(0, 0, ground.position.z + groundLength * 2f);
                StartLoopMove(ground); // gọi lại chính nó (loop)
            });
    }
}
