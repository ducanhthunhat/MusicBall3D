using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float fallDistance = 5f;
    [SerializeField] private float fallDuration = 0.35f;
    [SerializeField] private float hoverOffset = 4f;
    [SerializeField] private float moveSmooth = 0.25f;  // thời gian trễ khi theo tay (nhỏ = nhanh, lớn = chậm)
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;
    [SerializeField] private float raycastDistance = 0.6f;
    [SerializeField] private LayerMask Tile;
    [SerializeField] private LayerMask CheckDistance;

    private Tween currentTween;
    private Tween moveTween;
    private Camera mainCam;
    private bool CanJump;
    private Tile tile;
    [SerializeField] private float tileDistance;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        // Kiểm tra có đang đứng trên tile hay không
        CanJump = Physics.Raycast(transform.position, Vector3.down, raycastDistance, Tile);

        // Kiểm tra tile phía trước (khoảng cách)
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, raycastDistance + 30f, CheckDistance))
        {
            tileDistance = hit.distance;
        }
        else
        {
            tileDistance = -1f; // Không thấy tile nào phía trước
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        MoveWithMouse();
#else
        MoveWithTouch();
#endif
        JumpBack();
    }

    private void MoveWithTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = touch.position;
            Vector3 worldPos = mainCam.ScreenToWorldPoint(
                new Vector3(touchPos.x, touchPos.y, Mathf.Abs(mainCam.transform.position.z - transform.position.z))
            );

            float targetX = Mathf.Clamp(worldPos.x, minX, maxX);
            MoveSmoothly(targetX);
        }
    }

    private void MoveWithMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = mainCam.ScreenToWorldPoint(
            new Vector3(mousePos.x, mousePos.y, Mathf.Abs(mainCam.transform.position.z - transform.position.z))
        );

        float targetX = Mathf.Clamp(worldPos.x, minX, maxX);
        MoveSmoothly(targetX);
    }

    private void MoveSmoothly(float targetX)
    {
        if (moveTween != null && moveTween.IsActive()) moveTween.Kill();

        // Hiệu ứng di chuyển mượt + có trễ nhẹ
        moveTween = transform.DOMoveX(targetX, moveSmooth)
            .SetEase(Ease.OutQuad)
            .SetLink(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckFall"))
        {
            StopTween();
            currentTween = transform.DOMoveY(transform.position.y - fallDistance, fallDuration)
                .SetEase(Ease.InQuad)
                .SetLink(gameObject);
        }
    }

    private void JumpBack()
    {
        if (!CanJump) return;
        StopTween();

        // Raycast kiểm tra tile phía trước
        if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, 30f, CheckDistance))
        {
            tileDistance = hit.distance;

            Tile tile = hit.collider.GetComponent<Tile>();
            if (tile == null) return;

            float tileSpeed = tile.tileSpeed;

            // Tính thời gian tile đến player
            float timeToReachPlayer = tileDistance / tileSpeed;

            // Giới hạn để tránh giá trị cực đoan
            timeToReachPlayer = Mathf.Clamp(timeToReachPlayer, 0.3f, 2f);

            // Độ cao nhảy tỉ lệ thuận với thời gian tile đến
            float minJump = 2f;
            float maxJump = 8f;
            float jumpHeight = Mathf.Lerp(minJump, maxJump, Mathf.InverseLerp(0.3f, 2f, timeToReachPlayer));


            // Tổng thời gian nhảy = thời gian tile đến player
            float totalJumpTime = timeToReachPlayer;

            // Bay lên và rơi xuống chia đều 2 giai đoạn
            float halfTime = totalJumpTime / 2f;

            Sequence seq = DOTween.Sequence();

            // Bay lên nhanh
            seq.Append(transform.DOMoveY(transform.position.y + jumpHeight, halfTime)
                .SetEase(Ease.OutQuad));

            // Rơi xuống đúng lúc tile tới
            seq.Append(transform.DOMoveY(transform.position.y, halfTime)
                .SetEase(Ease.InQuad));

            seq.SetLink(gameObject);
            currentTween = seq;
        }
    }

    private void StopTween()
    {
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
            currentTween = null;
        }
    }

    private void OnDestroy()
    {
        StopTween();
        if (moveTween != null && moveTween.IsActive())
            moveTween.Kill();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycastDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * (raycastDistance + 30f));
    }
}
