using UnityEngine;
using DG.Tweening;

public class TrapHitVFX_Strong : MonoBehaviour
{
    [Header("Renderer")]
    [SerializeField] private Renderer rend;

    [Header("STRONG SCALE HIT")]
    public float punchScale = 0.25f;     // MẠNH
    public float punchTime = 0.18f;

    [Header("FLASH")]
    public Color hitColor = Color.white;
    public int flashCount = 3;            // Nhấp nháy nhiều hơn

    private Vector3 baseScale;
    private Color baseColor;

    private void Awake()
    {
        baseScale = transform.localScale;

        if (rend == null)
            rend = GetComponent<Renderer>();

        if (rend != null)
            baseColor = rend.material.color;
    }

    public void PlayHit()
    {
        transform.DOKill();
        if (rend != null) rend.material.DOKill();

        Sequence seq = DOTween.Sequence();

        // 💥 RUNG SCALE MẠNH (giống bắn tường FPS)
        seq.Append(transform.DOPunchScale(
            new Vector3(0.18f, 0.18f, punchScale),
            punchTime,
            vibrato: 12,
            elasticity: 0.9f
        ));

        // ⚡ FLASH GẮT
        if (rend != null)
        {
            seq.Join(
                rend.material.DOColor(hitColor, 0.04f)
                    .SetLoops(flashCount * 2, LoopType.Yoyo)
                    .OnComplete(() => rend.material.color = baseColor)
            );
        }
    }

    private void OnDisable()
    {
        DOTween.Kill(gameObject);
        transform.localScale = baseScale;
        if (rend != null)
            rend.material.color = baseColor;
    }
}
