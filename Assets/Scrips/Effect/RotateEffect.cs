using UnityEngine;
using DG.Tweening;

public class RotateEffect : MonoBehaviour
{
    [SerializeField] private float TimeDone = 10f;
    private Tween rotateTween;
    private void Start()
    {
        // Xoay quanh trục Z
        rotateTween = transform
            .DORotate(new Vector3(0, 0, -360f), TimeDone, RotateMode.FastBeyond360) // Xoay 360 độ
            .SetEase(Ease.Linear) // Xoay đều, không giật
            .SetLoops(-1, LoopType.Restart) // Lặp vô hạn
            .SetUpdate(true); // ✅ vẫn hoạt động dù đổi scene
    }

    private void OnDestroy()
    {
        if (rotateTween != null && rotateTween.IsActive())
            rotateTween.Kill();
    }
}
