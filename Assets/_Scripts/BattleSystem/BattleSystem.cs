using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    [SerializeField]
    AbilityTable allAbilities;

    [SerializeField]
    Button[] abilityButtons = new Button[4];

    [SerializeField]
    Button playerPassButton;

    [SerializeField]
    Button playerRunButton;

    [SerializeField]
    PlayerBattleController player;

    [SerializeField]
    EnemyBattleController enemy;

    [SerializeField]
    TMP_Text dialogue;

    [SerializeField]
    GameObject abilityPanel;

    [SerializeField]
    GameObject optionsPanel;

    List<Ability> selectedAbilities;
    List<IBattleCharacter> attackOrder;
    List<IBattleCharacter> targetOrder;

    public UnityEvent onTurnStart = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        Setup();
        StartBattle();
    }

    void Setup()
    {
        // Add Listeners to all abilities
        for (int i = 0; i < allAbilities.NumberOfAbilities(); i++)
        {
            allAbilities.GetAbility(i).onAbilityUse.AddListener(PollAbilities);
        }

        for (int i = 0; i < 4; i++)
        {
            int temp = i; // For the lambda to work

            abilityButtons[i].transform.GetChild(0).GetComponent<TMP_Text>().text = player.Abilities[i].AbilityName;

            // Buttons have an ability attached to them, clicking them picks an ability
            abilityButtons[i].onClick.AddListener(() => { player.Abilities[temp].SelectAbility(player, enemy); });
        }

        // Separated the Run and Pass from "Abilities" but still treating them as abilities to unify them
        playerRunButton.onClick.AddListener(() => { allAbilities.GetAbility(3).SelectAbility(player, enemy); });
        playerPassButton.onClick.AddListener(() => { allAbilities.GetAbility(4).SelectAbility(player, enemy); });

        selectedAbilities = new List<Ability>();
        attackOrder = new List<IBattleCharacter>();
        targetOrder = new List<IBattleCharacter>();
    }

    // Happens at the start of the fight to introduce the enemy
    void StartBattle()
    {
        dialogue.text = "A wild " + enemy.CharacterName + " has appeared!";
        enemy.SelectAction();
    }

    // Retrieve attacks from enemy and player
    // Called when ability button is clicked
    void PollAbilities(Ability ability)
    {
        // Actions go first - ex: items, run, pass
        if (selectedAbilities.Count > 0 &&
            ability.AbilityPriority > selectedAbilities[0].AbilityPriority)
        {
            // Issue with current system is that both player and enemy share the same scriptable object
            // Rough fix is to use a parallel array to hold the caster and target before overwriting them 
            // in the next call
            selectedAbilities.Insert(0, ability);
            attackOrder.Insert(0, ability.Caster);
            targetOrder.Insert(0, ability.Target);
        }
        else
        {
            selectedAbilities.Add(ability);
            attackOrder.Add(ability.Caster);
            targetOrder.Add(ability.Target);
        }

        // If everyone has picked an action, begin attack
        // Doing it this way because in some turn-based games, speed stat determines who moves first
        // so instead of attacking one after the other, wait for both to make a selection, and then decide who goes first
        if (selectedAbilities.Count == 2)
        {
            StartCoroutine(AttackPhase(selectedAbilities));
        }
    }

    IEnumerator AttackPhase(List<Ability> abilities)
    {
        // During attack, player should not be able to interact with the UI
        abilityPanel.SetActive(false);
        optionsPanel.SetActive(false);

        // Run a coroutine for each action
        // Coroutine because using delays to make sure animation plays and text is displayed long enough for players to read
        // and that it is easier to read and that Ability is kept scriptable
        for (int i = 0; i < abilities.Count; i++)
        {
            yield return abilities[i].UseAbility(attackOrder[i], targetOrder[i], dialogue);    
        }

        // Next round
        dialogue.text = "What will you do?";
        optionsPanel.SetActive(true);
        abilities.Clear();
        attackOrder.Clear();
        targetOrder.Clear();

        enemy.SelectAction();
    }
}
