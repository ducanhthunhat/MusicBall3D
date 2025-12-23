using UnityEngine;
using UnityEngine.UI;

public class UIBossHp : UICanvas
{
    [SerializeField] private Image hpFill;

    public void SetHP(float percent)
    {
        hpFill.fillAmount = percent;
    }

    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }
}
