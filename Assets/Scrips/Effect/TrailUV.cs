using UnityEngine;
using DG.Tweening;

public class TrailUVTween : MonoBehaviour
{
    [SerializeField] private Renderer trailRenderer;
    [SerializeField] private float scrollSpeed = 1f;

    void Start()
    {
        Material mat = trailRenderer.material;
        // Cuộn texture ngược liên tục
        DOTween.To(
            () => mat.mainTextureOffset,
            x => mat.mainTextureOffset = x,
            new Vector2(-1f, 0f),
            scrollSpeed
        )
        .SetEase(Ease.Linear)
        .SetLoops(-1, LoopType.Incremental); // lặp vô hạn, texture chạy mãi
    }
}
