using UnityEngine;
using DG.Tweening;

public class TrapHitEffect : MonoBehaviour
{
    [SerializeField] private Renderer rend; // Renderer để nhấp nháy màu
    private Vector3 initialPos;
    private Color initialColor;

    private void Awake()
    {
        initialPos = transform.localPosition;

        if (rend == null)
            rend = GetComponent<Renderer>();

        if (rend != null)
            initialColor = rend.material.color;
    }

    public void PlayHitEffect()
    {
        // Dừng mọi tween cũ
        transform.DOKill();
        if (rend != null)
            rend.material.DOKill();

        Sequence seq = DOTween.Sequence();

        // Rung nhẹ theo X ±0.05
        seq.Append(transform.DOLocalMoveX(initialPos.x + 0.05f, 0.05f));
        seq.Append(transform.DOLocalMoveX(initialPos.x - 0.05f, 0.05f));
        seq.Append(transform.DOLocalMoveX(initialPos.x, 0.05f));

        // Nhấp nháy màu trắng nhẹ
        if (rend != null)
        {
            seq.Join(rend.material.DOColor(Color.white, 0.05f)
                .SetLoops(2, LoopType.Yoyo)
                .OnComplete(() => rend.material.color = initialColor));
        }
    }

    public void ResetEffect()
    {
        transform.localPosition = initialPos;
        if (rend != null)
            rend.material.color = initialColor;
    }

    private void OnDisable()
    {
        DOTween.Kill(gameObject);
        ResetEffect();
    }
}
