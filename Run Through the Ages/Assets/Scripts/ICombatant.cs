using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ICombatant
{


    float GetSpeed();
    float GetAttack();
    float GetAttackSpeed();
    float GetCourage();
    float GetHealth();
    float GetCurrentAtkSpeed();
    string GetTag();

    ICombatant GetTarget();
    void SetTarget(ICombatant combatant);

    bool TakeDamage(float damage);
    void SetCurrentAtkSpeed();
    void SetCurrentAtkSpeed(float AtkSpeed);
    bool MakeAttack(List<ICombatant> ls );



}
