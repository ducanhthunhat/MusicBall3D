using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectButtonEffect : MonoBehaviour
{
    [SerializeField] private Image buttonImage;           // Ảnh của nút
    [SerializeField] private Sprite normalSprite;         // Sprite mặc định
    [SerializeField] private Sprite selectedSprite;       // Sprite khi chọn

    private static SelectButtonEffect currentSelected;    // Lưu nút hiện đang chọn

    private void Start()
    {
        // Nếu chưa gán Image thì tự tìm trong nút
        if (buttonImage == null)
            buttonImage = GetComponent<Image>();
    }

    public void OnSelectButton()
    {
        // Nếu có nút khác đang được chọn thì reset nó
        if (currentSelected != null && currentSelected != this)
        {
            currentSelected.SetNormal();
        }

        // Đặt nút này làm đang chọn
        currentSelected = this;
        SetSelected();
    }

    private void SetSelected()
    {
        buttonImage.sprite = selectedSprite;
    }

    private void SetNormal()
    {
        buttonImage.sprite = normalSprite;
    }
}
