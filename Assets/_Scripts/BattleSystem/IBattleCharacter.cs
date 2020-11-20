using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IBattleCharacter : MonoBehaviour
{
    [SerializeField]
    protected Ability[] abilities = new Ability[4];

    [SerializeField]
    protected int attack = 100;
    public int Attack { get { return attack; } }

    [SerializeField]
    protected int spAttack = 100;
    public int SpAttack { get { return spAttack; } }

    [SerializeField]
    protected int defense = 100;
    public int Defense { get { return defense; } }

    [SerializeField]
    protected int spDefense = 100;
    public int SpDefense { get { return spDefense; } }

    [SerializeField]
    protected int health = 300;
    public int Health { get { return health; } }

    [SerializeField]
    protected int mana = 100;
    public int Mana { get { return mana; } }

    [SerializeField]
    string characterName;
    public string CharacterName
    {
        get { return characterName; }
    }

    public Ability[] Abilities
    {
        get { return abilities; }
    }

    public abstract void SelectAction();
}