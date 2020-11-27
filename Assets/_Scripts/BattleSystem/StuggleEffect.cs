using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "StruggleEffect", menuName = "ScriptableObjects/StruggleEffect")]
public class StuggleEffect : DamageEffect
{
    public override IEnumerator TriggerEffect(IBattleCharacter _caster, IBattleCharacter _target, Ability ability, TMP_Text dialogue)
    {
        yield return new WaitForSeconds(2);

        dialogue.text = "Lucky Hit!";

        yield return new WaitForSeconds(2);
        // Display damage
        dialogue.text = _target.CharacterName + " took " + DamageCalculation(_caster, _target, ability) + " damage!";

        yield return new WaitForSeconds(2);
    }

    public override int DamageCalculation(IBattleCharacter _caster, IBattleCharacter _target, Ability ability)
    {
        // Fixed damage
        _target.DeductHealth(100);
        return 100;
    }
}
