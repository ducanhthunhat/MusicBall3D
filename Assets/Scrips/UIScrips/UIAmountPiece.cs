using UnityEngine;
using UnityEngine.UI;
using TMPro; // Nếu dùng TextMeshPro, nếu dùng Text thường thì đổi thành using UnityEngine.UI;

public class UIAmountPiece : UICanvas
{
    [Header("Kéo thả UI vào đây")]
    [SerializeField] private Image iconImage;      // Ảnh icon trên UI
    [SerializeField] private TextMeshProUGUI amountText; // Text số lượng (hoặc Text thường)

    // Hàm nhận dữ liệu từ Player để hiển thị
    public void ShowSkinData(Sprite icon, int amount)
    {
        // Hiển thị Icon
        if (iconImage != null && icon != null)
        {
            iconImage.sprite = icon;
            iconImage.gameObject.SetActive(true);
        }

        // Hiển thị số lượng
        if (amountText != null)
        {
            amountText.text = "x" + amount.ToString();
        }
    }
}