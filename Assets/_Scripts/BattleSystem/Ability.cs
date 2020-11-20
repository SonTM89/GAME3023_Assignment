using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Ability", order = 1)]
public class Ability : ScriptableObject
{
    [SerializeField]
    List<Effect> effects;

    [SerializeField]
    int abilityID;
    public int AbilityID
    {
        get { return abilityID; }
        set { abilityID = value; }
    }

    [SerializeField]
    string abilityName;
    public string AbilityName
    {
        get { return abilityName; }
    }

    [SerializeField]
    int damage;
    public int Damage
    {
        get { return damage; }
    }

    public enum DamageType
    {
        NONE,
        ATTACK,
        SPATTACK
    }

    [SerializeField]
    private DamageType damageType;
    public DamageType DmgType { get { return damageType; } }

    IBattleCharacter caster;
    public IBattleCharacter Caster
    {
        get { return caster; }
    }

    IBattleCharacter target;
    public IBattleCharacter Target
    {
        get { return target; }
    }

    [SerializeField]
    int abilityCost;
    public int AbilityCost
    {
        get { return abilityCost; }
    }

    [SerializeField]
    int abilityPriority;
    public int AbilityPriority
    {
        get { return abilityPriority; }
    }

    public UnityEvent<Ability> onAbilityUse = new UnityEvent<Ability>();

    public IEnumerator UseAbility(IBattleCharacter _caster, IBattleCharacter _target, TMP_Text dialogue)
    {
        foreach (var effect in effects)
        {
            yield return effect.TriggerEffect(_caster, _target, this, dialogue);
        }
    }

    public void SelectAbility(IBattleCharacter _caster, IBattleCharacter _target)
    {
        target = _target;
        caster = _caster;

        // Calls the Battle System to go to battle phase
        onAbilityUse.Invoke(this);
    }
}
