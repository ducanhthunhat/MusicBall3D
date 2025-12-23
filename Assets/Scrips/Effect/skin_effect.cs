using UnityEngine;
using DG.Tweening;

public class SkinPieceEffect : MonoBehaviour
{
    private void OnEnable()
    {
        PlayEffect();
    }

    public void PlayEffect()
    {
        transform.DOKill();

        transform.DOMoveY(transform.position.y + 0.6f, 0.6f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

        transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
    }
}
