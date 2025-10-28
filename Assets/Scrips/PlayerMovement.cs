using UnityEngine;
using DG.Tweening;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float fallDistance = 5f;    // Độ sâu khi rơi
    [SerializeField] private float fallDuration = 0.35f;   // Thời gian rơi
    [SerializeField] private float hoverOffset = 4f;    // Độ cao cố định khi lơ lửng
    [SerializeField] private float moveSmooth = 8f;      // Độ mượt khi di chuyển
    [SerializeField] private float minX = -5f;            // Giới hạn trái
    [SerializeField] private float maxX = 5f;             // Giới hạn phải
    [SerializeField] private float raycastDistance = 0.6f; // Khoảng cách raycast để kiểm tra mặt đất
    private Tween currentTween;
    private Camera mainCam;
    [SerializeField] private LayerMask Tile;

    private bool CanJump;
    private void Start()

    {
        mainCam = Camera.main;
    }
    private void Update()
    {
        CanJump = Physics.Raycast(transform.position, Vector3.down, raycastDistance, Tile);
#if UNITY_EDITOR || UNITY_STANDALONE
        MoveWithMouse(); // Dành cho khi test trên PC
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
            // Lấy vị trí chạm trên màn hình
            Vector3 touchPos = touch.position;
            Vector3 worldPos = mainCam.ScreenToWorldPoint(
                new Vector3(touchPos.x, touchPos.y, Mathf.Abs(mainCam.transform.position.z - transform.position.z))

            );
            float targetX = Mathf.Clamp(worldPos.x, minX, maxX);
            Vector3 targetPos = new Vector3(targetX, transform.position.y, transform.position.z);
            // Di chuyển mượt theo ngón tay
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSmooth);
        }
    }

    private void MoveWithMouse()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 worldPos = mainCam.ScreenToWorldPoint(

            new Vector3(mousePos.x, mousePos.y, Mathf.Abs(mainCam.transform.position.z - transform.position.z))

        );
        float targetX = Mathf.Clamp(worldPos.x, minX, maxX);
        Vector3 targetPos = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSmooth);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckFall"))
        {
            StopTween();
            // Rơi thẳng xuống
            currentTween = transform.DOMoveY(transform.position.y - fallDistance, fallDuration)
                .SetEase(Ease.InQuad)
                .SetLink(gameObject);
        }
    }

    private void JumpBack()
    {
        if (!CanJump) return;
        else
        {
            StopTween();
            // Nhảy trở lại vị trí lơ lửng
            currentTween = transform.DOMoveY(hoverOffset, fallDuration)

                .SetEase(Ease.OutQuad)

                .SetLink(gameObject);
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
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycastDistance);
    }

}