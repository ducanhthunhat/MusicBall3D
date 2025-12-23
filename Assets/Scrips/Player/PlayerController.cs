using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    // ... (Giữ nguyên các biến di chuyển cũ của bạn) ...
    private int currentLane = 1;
    private float[] lanes = { -2f, 0f, 2f };
    public float moveTime = 0.35f;
    private Tweener moveTween;
    private float tiltAngle = 20f;
    private float tiltTime = 0.2f;

    // ... (Giữ nguyên Update, SwipeControl, ChangeLane cũ) ...

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) ChangeLane(-1);
        if (Input.GetKeyDown(KeyCode.D)) ChangeLane(1);
        SwipeControl();
    }

    // ... (Các hàm SwipeControl, ChangeLane giữ nguyên như cũ) ...
    private Vector2 startPos;
    private float minSwipe = 100f;
    void SwipeControl()
    {
        if (Input.touchCount == 0) return;
        Touch t = Input.GetTouch(0);
        if (t.phase == TouchPhase.Began) startPos = t.position;
        else if (t.phase == TouchPhase.Ended)
        {
            float deltaX = t.position.x - startPos.x;
            if (Mathf.Abs(deltaX) > minSwipe)
            {
                if (deltaX > 0) ChangeLane(1); else ChangeLane(-1);
            }
        }
    }

    void ChangeLane(int direction)
    {
        int oldLane = currentLane;
        currentLane = Mathf.Clamp(currentLane + direction, 0, 2);
        if (oldLane == currentLane) return;
        moveTween?.Kill();
        float targetTilt = direction * -tiltAngle;
        transform.DORotate(new Vector3(0, 0, targetTilt), tiltTime);
        moveTween = transform.DOMoveX(lanes[currentLane], moveTime)
            .SetEase(Ease.OutCubic)
            .OnComplete(() => { transform.DORotate(Vector3.zero, 0.15f); });
    }

    public void OnEnable() { PlayerShotting.onPlayerShoot += PlayerShoot; }
    public void OnDisable() { PlayerShotting.onPlayerShoot -= PlayerShoot; }
    public void PlayerShoot() { Debug.Log("Player has shot!"); }

    // --- CODE MỚI THÊM VÀO ---
    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra va chạm với Skin Piece
        skin_Piece skin = other.GetComponentInParent<skin_Piece>();
        Debug.Log("Collided with: " + other.name);
        if (skin != null)
        {
            // 1. Cộng vào kho (Theo tên riêng)
            int amount = GameManger.Instance.AddSkin(skin.skinName);

            // 2. Mở UI và hiển thị đúng loại vừa ăn
            UIAmountPiece ui = UIManager.Instance.OpenUI<UIAmountPiece>();
            if (ui != null)
            {
                ui.ShowSkinData(skin.skinIcon, amount);
                UIManager.Instance.CloseUI<UIAmountPiece>(3f);
            }

            // 3. Thu hồi vật phẩm
            skin.Despawn();
        }

    }
}