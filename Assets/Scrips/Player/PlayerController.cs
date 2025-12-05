using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private int currentLane = 1;
    private float[] lanes = { -2f, 0f, 2f };

    public float moveTime = 0.35f;   // tăng nhẹ để mượt hơn
    private Tweener moveTween;

    private float tiltAngle = 20f;   // góc nghiêng khi đổi lane
    private float tiltTime = 0.2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            ChangeLane(-1);

        if (Input.GetKeyDown(KeyCode.D))
            ChangeLane(1);

        SwipeControl();
    }

    // Swipe điều khiển
    private Vector2 startPos;
    private float minSwipe = 100f;
    void SwipeControl()
    {
        if (Input.touchCount == 0) return;

        Touch t = Input.GetTouch(0);

        if (t.phase == TouchPhase.Began)
            startPos = t.position;
        else if (t.phase == TouchPhase.Ended)
        {
            float deltaX = t.position.x - startPos.x;
            if (Mathf.Abs(deltaX) > minSwipe)
            {
                if (deltaX > 0) ChangeLane(1);
                else ChangeLane(-1);
            }
        }
    }

    // HIỆU ỨNG MƯỢT HƠN
    void ChangeLane(int direction)
    {
        int oldLane = currentLane;
        currentLane = Mathf.Clamp(currentLane + direction, 0, 2);

        if (oldLane == currentLane) return;

        moveTween?.Kill();

        // nghiêng khi đổi lane
        float targetTilt = direction * -tiltAngle;
        transform.DORotate(new Vector3(0, 0, targetTilt), tiltTime);

        // chạy lane mượt easing
        moveTween = transform
            .DOMoveX(lanes[currentLane], moveTime)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                // đứng thẳng về lại
                transform.DORotate(Vector3.zero, 0.15f);
            });
    }
}
