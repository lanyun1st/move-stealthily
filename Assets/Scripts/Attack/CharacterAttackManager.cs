using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterAttackManager : MonoBehaviour
{
    public MonoBehaviour[] attackScripts;

    public WeaponBarUI weaponBar;   // 武器栏UI

    private IAttack[] attacks;
    private float lastAttackTime = 0f;
    private int currentAttackIndex = 0;

    public GameObject knife;
public GameObject gun;
    void Start()
    {
        attacks = new IAttack[attackScripts.Length];

        for (int i = 0; i < attackScripts.Length; i++)
        {
            attacks[i] = attackScripts[i] as IAttack;

            if (attacks[i] == null)
                Debug.LogError("攻击脚本没有实现 IAttack 接口");
            else
                Debug.Log("成功加载攻击: " + attackScripts[i].name);
        }
    }

    void Update()
    {
        HandleWeaponSwitch();
        HandleAttack();
    }

    void HandleWeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentAttackIndex = 0;

            knife.SetActive(true);
            gun.SetActive(false);

            weaponBar.SetCurrentWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentAttackIndex = 1;

            knife.SetActive(false);
            gun.SetActive(true);

            weaponBar.SetCurrentWeapon(1);
        }
    }

    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            TryAttack(currentAttackIndex);
        }
    }

    void TryAttack(int index)
    {
        if (index < 0 || index >= attacks.Length) return;

        IAttack attack = attacks[index];

        if (Time.time - lastAttackTime >= attack.GetCooldown())
        {
            attack.Execute(transform);
            lastAttackTime = Time.time;

            // 触发武器冷却UI
            weaponBar.TriggerCooldown(index, attack.GetCooldown());
        }
    }
}