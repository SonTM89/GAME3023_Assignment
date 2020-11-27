using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "RunEffect", menuName = "ScriptableObjects/RunEffect")]
public class RunEffect : Effect
{
    public override IEnumerator TriggerEffect(IBattleCharacter _caster, IBattleCharacter _target, Ability ability, TMP_Text dialogue)
    {
        dialogue.text = _caster.CharacterName + " ran away...";
        yield return new WaitForSeconds(2);

        //SceneManager.LoadScene("Level1");
        RunAway escape = new RunAway();
        escape.Escape();
        yield return new WaitForSeconds(2);
    }
}
