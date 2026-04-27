using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<int, int> OnHealthChanged; // 参数：当前血量，最大血量
    public event Action OnDied;

    public int maxHP = 100;
    public int currentHP;
    public HealthBarUI healthBar;
    public DeathUI deathUI;
    void Awake()
    {
        currentHP = maxHP;
    }

    void Start()
    {
        if (healthBar != null)
            healthBar.SetMaxHealth(maxHP);
    }

    public void TakeDamage(int damage)
    {
        if (currentHP <= 0) return;

        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        // 直接更新血条 UI
        if (healthBar != null)
            healthBar.SetHealth(currentHP, maxHP);

        // 触发事件（可选，如果你还需要其他反馈）
        OnHealthChanged?.Invoke(currentHP, maxHP);

        if (currentHP == 0)
        {
            OnDied?.Invoke();
            Die();
        }
    }
    public void AddMaxHealth(int amount)
    {
        maxHP += amount;          // 增加最大血量
        currentHP = maxHP;        // 回满血

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHP);
            healthBar.SetHealth(currentHP, maxHP);
        }

        OnHealthChanged?.Invoke(currentHP, maxHP);
    }
    void Die()
    {
        if (deathUI != null)
            deathUI.Show();

        gameObject.SetActive(false);
    }

}