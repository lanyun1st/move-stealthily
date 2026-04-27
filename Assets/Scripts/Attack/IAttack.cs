using UnityEngine;

public interface IAttack
{
    void Execute(Transform attacker);
    float GetCooldown();
}