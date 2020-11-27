using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public abstract class Effect : ScriptableObject
{


    public abstract IEnumerator TriggerEffect(IBattleCharacter _caster, IBattleCharacter _target,
        Ability ability, TMP_Text dialogue);
}
