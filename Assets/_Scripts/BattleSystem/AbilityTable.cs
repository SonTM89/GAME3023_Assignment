using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Ability Table", menuName = "ScriptableObjects/AbilityTable", order = 2)]
public class AbilityTable : ScriptableObject
{
    [SerializeField]
    private Ability[] abilities;

    public Ability GetAbility(int id)
    {
        return abilities[id];
    }

    public int NumberOfAbilities()
    {
        return abilities.Length;
    }

    public void AssignAbilityIDs()
    {
        for(int i = 0; i < abilities.Length; i++)
        {
            //abilities[i].AbilityID = i;
        }
    }

}
