using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "ScriptableObjects/DamageEffect")]
public class DamageEffect : Effect
{
    public UnityEvent<GameObject, int> playAnimation;
    public UnityEvent<int, IBattleCharacter, Ability> onDamageToTarget;

    public override IEnumerator TriggerEffect(IBattleCharacter _caster, IBattleCharacter _target, Ability ability, TMP_Text dialogue)
    {
        // Who attacked which target
        dialogue.text = _caster.CharacterName + " used " + ability.AbilityName + " on " + _target.CharacterName + "!";
        yield return new WaitForSeconds(2);

        // Play the attack animation
        playAnimation.Invoke(ability.abilityAnimation, (int) _caster.direction);

        // Play player attack animation
        if (ability.characterAnimationTrigger != "")
        {
            _caster.character.GetComponent<Animator>().SetTrigger(ability.characterAnimationTrigger);
        }

        yield return new WaitForSeconds(2);
        // Display damage
        dialogue.text = _target.CharacterName + " took " + DamageCalculation(_caster, _target, ability) + " damage!";

        yield return new WaitForSeconds(2);
    }

    public int DamageCalculation(IBattleCharacter _caster, IBattleCharacter _target, Ability ability)
    {
        int defenseStat = 1;
        int attackStat = 0;

        switch (ability.DmgType)
        {
            case Ability.DamageType.ATTACK:
                attackStat = _caster.Attack;
                defenseStat = _target.Defense;
                break;
            case Ability.DamageType.SPATTACK:
                attackStat = _caster.SpAttack;
                defenseStat = _target.SpDefense;
                break;
        }

        // Modified damage calculation from Bulbapedia
        int calculatedDamage = ((((20) + 2) * ability.Damage * (attackStat / defenseStat)) / 50) + 10;

        onDamageToTarget.Invoke(calculatedDamage, _target, ability);
        return calculatedDamage;
    }
}
