using UnityEngine;

public class skin_Piece : MonoBehaviour
{
    [Header("Dữ liệu (Cài đặt trong từng Prefab)")]
    public string skinName;    // Ví dụ: "Red Piece"
    public Sprite skinIcon;    // Kéo ảnh icon vào đây

    private FastPool myPool;   // Biến để nhớ Pool

    // Hàm nhận Pool từ Spawner
    public void SetPool(FastPool pool)
    {
        myPool = pool;
    }

    void Update()
    {
        // Nếu Boss ra -> Tự hủy (Tùy chọn)
        if (GameManger.Instance.isBossActive)
        {
            Despawn();
            return;
        }

        // Di chuyển
        float speed = GameSpeedManager.Instance.CurrentSpeed;
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // Ra khỏi màn hình -> Hủy
        if (transform.position.z < -18f)
        {
            Despawn();
        }
    }

    // --- LƯU Ý: ĐÃ XÓA OnTriggerEnter Ở ĐÂY (Chuyển sang Player) ---

    public void Despawn()
    {
        // Trả về đúng Pool đã sinh ra nó
        if (myPool != null)
        {
            myPool.FastDestroy(gameObject);
        }
        else
        {
            Destroy(gameObject); // Dự phòng
        }
    }
}