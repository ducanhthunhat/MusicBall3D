using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float control = 5f;          // Tốc độ di chuyển ngang
    [SerializeField] private float fallDistance = 10f;    // Độ sâu khi rơi
    [SerializeField] private float fallDuration = 0.3f;   // Thời gian rơi
    [SerializeField] private float hoverOffset = 0.5f;    // Độ cao cố định khi lơ lửng
    [SerializeField] private float moveSmooth = 10f;      // Độ mượt khi theo chuột
    [SerializeField] private float minX = -5f;            // Giới hạn trái
    [SerializeField] private float maxX = 5f;             // Giới hạn phải

    private Tween currentTween;
    private bool isFalling = false;
    private bool isHovering = false;

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        MoveWithMouse();
    }

    private void MoveWithMouse()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 worldPos = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(mainCam.transform.position.z - transform.position.z)));

        float targetX = Mathf.Clamp(worldPos.x, minX, maxX);

        Vector3 targetPos = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSmooth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckFall"))
        {
            StopTween();

            isFalling = true;
            isHovering = false;

            // Rơi thẳng xuống
            currentTween = transform.DOMoveY(transform.position.y - fallDistance, fallDuration)
                .SetEase(Ease.InQuad)
                .OnComplete(() =>
                {
                    isFalling = false;
                });
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Tile"))
        {
            StopTween();

            isHovering = true;
            isFalling = false;

            // Bay nhẹ lên rồi dừng ở vị trí cố định (lơ lửng)
            float targetY = collision.contacts[0].point.y + hoverOffset;

            currentTween = transform.DOMoveY(targetY, 0.3f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
                });
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
}
