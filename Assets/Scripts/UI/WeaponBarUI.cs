using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponBarUI : MonoBehaviour
{
    public Image[] cooldownImages;     // 每个武器的冷却遮罩
    public GameObject[] highlightBoxes; // 当前武器高亮框

    void Start()
    {
        for (int i = 0; i < cooldownImages.Length; i++)
        {
            cooldownImages[i].fillAmount = 0f;
        }

        SetCurrentWeapon(0);
    }

    public void SetCurrentWeapon(int index)
    {
        for (int i = 0; i < highlightBoxes.Length; i++)
        {
            highlightBoxes[i].SetActive(i == index);
        }
    }

    public void TriggerCooldown(int index, float cooldown)
    {
        StartCoroutine(CooldownRoutine(index, cooldown));
    }

    IEnumerator CooldownRoutine(int index, float cooldown)
    {
        float timer = 0f;

        while (timer < cooldown)
        {
            timer += Time.deltaTime;

            cooldownImages[index].fillAmount =
                1f - (timer / cooldown);

            yield return null;
        }

        cooldownImages[index].fillAmount = 0f;
    }
}