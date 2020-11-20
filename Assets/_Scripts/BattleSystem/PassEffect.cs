using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "PassEffect", menuName = "ScriptableObjects/PassEffect")]
public class PassEffect : Effect
{
    public override IEnumerator TriggerEffect(IBattleCharacter _caster, IBattleCharacter _target, Ability ability, TMP_Text dialogue)
    {
        dialogue.text = _caster.CharacterName + " passed their turn.";
        yield return new WaitForSeconds(2);
    }
}
