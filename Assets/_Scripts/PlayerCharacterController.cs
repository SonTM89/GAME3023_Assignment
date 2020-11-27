using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacterController : IBattleCharacter
{
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        // Set Player velocity
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementVector *= speed;

        //transform.Translate(movementVector);
        rb.velocity = movementVector;
    }
}
