using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private int currentLane = 1;
    private float[] lanes = { -2f, 0f, 2f };

    public float moveTime = 0.35f;
    private Tweener moveTween;

    private float tiltAngle = 20f;
    private float tiltTime = 0.2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            ChangeLane(-1);

        if (Input.GetKeyDown(KeyCode.D))
            ChangeLane(1);

        SwipeControl();
    }
    public void OnEnable()
    {
        PlayerShotting.onPlayerShoot += PlayerShoot;
    }
    public void OnDisable()
    {
        PlayerShotting.onPlayerShoot -= PlayerShoot;
    }

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

    void ChangeLane(int direction)
    {
        int oldLane = currentLane;
        currentLane = Mathf.Clamp(currentLane + direction, 0, 2);

        if (oldLane == currentLane) return;

        moveTween?.Kill();

        float targetTilt = direction * -tiltAngle;
        transform.DORotate(new Vector3(0, 0, targetTilt), tiltTime);

        moveTween = transform
            .DOMoveX(lanes[currentLane], moveTime)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                transform.DORotate(Vector3.zero, 0.15f);
            });
    }

    public void PlayerShoot()
    {
        Debug.Log("Player has shot!");
    }
}
