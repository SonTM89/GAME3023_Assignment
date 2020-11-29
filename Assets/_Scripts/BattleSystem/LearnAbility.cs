using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LearnAbility : MonoBehaviour
{
    public AbilityTable masterAbilityTable;

    public Button[] abilityButtons;
    public TMP_Text skillNameLabel;
    public RunAway exitBattle;

    public TMP_Text dialogue;

    private Ability newAbility;

    // Start is called before the first frame update
    void Start()
    {
        // -1 for id 0, -3 for default abilities RUN, STRUGGLE, PASS
        newAbility = masterAbilityTable.GetAbility(Random.Range(0, masterAbilityTable.NumberOfAbilities() - 1 - 3));
        skillNameLabel.text = newAbility.name;

        BindAbilitiesToButtons();
    }

    void BindAbilitiesToButtons()
    {
        for (int i = 0; i < PlayerBattleController.Instance.Abilities.Length; i++)
        {
            int temp = i; // For the lambda to work

            abilityButtons[i].transform.GetChild(0).GetComponent<TMP_Text>().text = PlayerBattleController.Instance.Abilities[i].AbilityName;

            // Buttons have an ability attached to them
            abilityButtons[i].onClick.AddListener(() => { ReplaceSkill(temp); });
        }
    }

    public void ReplaceSkill(int slotNumber)
    {
        PlayerBattleController.Instance.Abilities[slotNumber] = newAbility;

        StartCoroutine(ExitBattle());
    }

    IEnumerator ExitBattle()
    {
        GetComponent<Canvas>().enabled = false;
        dialogue.text = PlayerBattleController.Instance.CharacterName + " learned " + newAbility.name + "!";
        yield return new WaitForSeconds(2);

        exitBattle.Escape();
    }
}
