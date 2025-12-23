using UnityEngine;

public class GatlingRecoilVFX3D : MonoBehaviour
{
    [Header("Vertical Recoil")]
    public float recoilHeight = 0.015f;
    public float recoilSpeed = 40f;

    [Header("Horizontal Recoil")]
    public float horizontalAmount = 0.008f;

    private Vector3 baseLocalPos;
    private float recoilTimer;

    private void OnEnable()
    {
        baseLocalPos = transform.localPosition;
        recoilTimer = Random.value * 10f; // lệch pha mỗi viên
    }

    private void Update()
    {
        recoilTimer += Time.deltaTime * recoilSpeed;

        // 🔼 Giật dọc (chủ đạo)
        float vertical = Mathf.Abs(Mathf.Sin(recoilTimer)) * recoilHeight;

        // ↔ Giật ngang rất nhỏ
        float horizontal = Mathf.Sin(recoilTimer * 0.5f) * horizontalAmount;

        transform.localPosition = baseLocalPos + new Vector3(horizontal, vertical, 0f);
    }

    private void OnDisable()
    {
        transform.localPosition = baseLocalPos;
    }
}
