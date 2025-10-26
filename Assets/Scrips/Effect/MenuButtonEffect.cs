using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnimatedMenu : MonoBehaviour
{
    [SerializeField] private Image[] menuIcons;
    [SerializeField] private Color activeColor = new Color(1f, 0.8f, 0.2f);
    [SerializeField] private Color inactiveColor = Color.white;

    private int currentIndex = -1;

    public void OnMenuClick(int index)
    {
        for (int i = 0; i < menuIcons.Length; i++)
        {
            var img = menuIcons[i];
            img.DOColor(i == index ? activeColor : inactiveColor, 0.2f);
            img.transform.DOScale(i == index ? 1.1f : 1f, 0.25f).SetEase(Ease.OutBack);
        }
        currentIndex = index;
    }
}
