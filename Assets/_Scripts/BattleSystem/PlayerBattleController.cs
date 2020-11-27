using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBattleController : IBattleCharacter
{
    private static PlayerBattleController instance;
    public static PlayerBattleController Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    private int originalHealth;

    public override void SelectAction()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        originalHealth = health;

        // Used to figure out when Struggle becomes active
        foreach (Ability ability in abilities)
        {
            if (ability.AbilityCost < minAbilityCost)
            {
                minAbilityCost = ability.AbilityCost;
            }
        }
    }
}
