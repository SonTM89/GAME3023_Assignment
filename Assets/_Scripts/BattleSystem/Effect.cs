using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public abstract class Effect : ScriptableObject
{
    [SerializeField]
    float animationDuration;
    public float AnimationDuration
    {
        get { return animationDuration; }
    }

    public abstract IEnumerator TriggerEffect(IBattleCharacter _caster, IBattleCharacter _target,
        Ability ability, TMP_Text dialogue);
}
