using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleController : IBattleCharacter
{
    [SerializeField]
    float[] weights = { 25, 25, 25, 25 };

    [SerializeField]
    float lastDmgOutput = -9999;

    [SerializeField]
    DamageEffect dmgEffect;

    private float maxWeightChange = 20;
    private int selectedAbility;
    public string selectedAbilityName;

    private void Start()
    {
        attack = Random.Range(95, 115);
        defense = Random.Range(95, 115);

        spAttack = Random.Range(95, 115);
        spDefense = Random.Range(95, 115);

        health = Random.Range(200, 500);

        dmgEffect.onDamageToTarget.AddListener(ProcessBattleResult);

        // Used to figure out when Struggle becomes active
        foreach (Ability ability in abilities)
        {
            if (ability.AbilityCost < minAbilityCost)
            {
                minAbilityCost = ability.AbilityCost;
            }
        }
    }

    public override void SelectAction()
    {
        DecisionMaking();
    }

    public void DecisionMaking()
    {
        int randRoll = Random.Range(1, 100);

        if (randRoll < weights[0])
        {
            Abilities[0].SelectAbility(this, PlayerBattleController.Instance);
            selectedAbility = 0;
            selectedAbilityName = Abilities[0].name;
        }
        else if (randRoll < weights[0] + weights[1])
        {
            Abilities[1].SelectAbility(this, PlayerBattleController.Instance);
            selectedAbility = 1;
            selectedAbilityName = Abilities[1].name;
        }
        else if (randRoll < weights[0] + weights[1] + weights[2])
        {
            Abilities[2].SelectAbility(this, PlayerBattleController.Instance);
            selectedAbility = 2;
            selectedAbilityName = Abilities[2].name;
        }
        else
        {
            Abilities[3].SelectAbility(this, PlayerBattleController.Instance);
            selectedAbility = 3;
            selectedAbilityName = Abilities[3].name;
        }
    }

    public void ProcessBattleResult(int damage, IBattleCharacter _target, Ability ability)
    {
        if (ReferenceEquals(_target, PlayerBattleController.Instance))
        {
            if (lastDmgOutput <= 0)
            {
                lastDmgOutput = damage;
                return;
            }

            float damageRatio = damage / lastDmgOutput;
            float weightChange = (weights[selectedAbility] * damageRatio) - weights[selectedAbility];
            UpdateWeights(weightChange);

            lastDmgOutput = damage;
        }
    }

    public void UpdateWeights(float weightChange)
    {
        if (weightChange == 0)
        {
            return;
        }

        // Get the sign of the change and clamp the change
        weightChange = Mathf.Min(maxWeightChange, Mathf.Abs(weightChange)) * (weightChange / Mathf.Abs(weightChange));

        float changeInOtherWeights = weightChange / 3;

        for (int i = 0; i < weights.Length; i++)
        {
            if (i == selectedAbility)
            {
                weights[i] += weightChange;
            }
            else
            {
                weights[i] -= changeInOtherWeights;
            }
        }
    }
}
