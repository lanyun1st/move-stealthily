using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider slider;          // 血条的 Slider
    public TMPro.TextMeshProUGUI hpText;            // 显示数值的 Text 组件

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        UpdateText(maxHealth, maxHealth);
    }

    public void SetHealth(int currentHealth, int maxHealth)
    {
        slider.value = currentHealth;
        UpdateText(currentHealth, maxHealth);
    }

    private void UpdateText(int current, int max)
    {
        if (hpText != null)
        {
            // 显示格式 "当前/最大"
            hpText.text = $"{current}/{max}";

            // 或者显示百分比 "50%"
            // int percent = Mathf.RoundToInt((float)current / max * 100);
            // hpText.text = $"{percent}%";
        }
    }
}