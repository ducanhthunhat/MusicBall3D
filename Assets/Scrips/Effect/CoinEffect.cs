using UnityEngine;
using DG.Tweening; // Rất quan trọng: Đảm bảo bạn có dòng này

public class InfiniteRotation : MonoBehaviour
{
    public float rotationDuration = 2.0f;

    void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), rotationDuration, RotateMode.FastBeyond360)

            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }
}