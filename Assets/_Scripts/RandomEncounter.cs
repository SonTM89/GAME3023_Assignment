﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomEncounter : MonoBehaviour
{
    float delayTimer = 0;

    /* The tilemap collider is seen as a single collider
     * instead of individual colliders on a sprite, so OnTriggerEnter2D
     * will not work.
     * 
     * OnTriggerStay will continously check, so addition programming is required
     * to make the random encounter feel right.
     */
     
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Should only try to roll an enemy encounter when the character is moving
        if (Input.GetAxis("Horizontal") == 0 && 
            Input.GetAxis("Vertical") == 0)
        {
            return; 
        }

        // Delay the checks by 0.125 seconds so that we aren't rolling to quickly
        delayTimer -= Time.deltaTime;
        if (delayTimer > 0)
        {
            return;
        } 
        else
        {
            delayTimer = 0.125f;
        }

        // Random Encounters @ 5% chance
        if (Random.Range(0, 100) < 5)
        {
            // When the spring arm gets disabled the camera freezes in the current position, which the spring arm will take as
            // the original position on re-enable.
            PlayerCharacterController.Instance.GetComponentInChildren<Camera>().gameObject.transform.localPosition = new Vector3(0, 0, -10);
            PlayerCharacterController.Instance.TurnOff();
            PlayerBattleController.Instance.character.SetActive(true);

            SceneManager.LoadScene("EnemyEncounter");
        }
    }
}
