using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunAway : MonoBehaviour
{
    public void Escape()
    {
        PlayerBattleController.Instance.character.SetActive(false);

        SceneManager.LoadScene("Level1");
    }
}
