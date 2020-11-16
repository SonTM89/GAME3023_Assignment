using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AnimState
{
    WALKUP,
    WALKRIGHT,
    WALKDOWN,
    WALKLEFT
}

public class PlayerCharacterAnimationController : MonoBehaviour
{


    private Animator myAnimator;
    private AnimState currentState = AnimState.WALKDOWN;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        bool isWalking = (movementVector.x == 0 && movementVector.y == 0) ? false : true;

        if (isWalking)
        {
            if (movementVector.y > 0.0)
            {
                currentState = AnimState.WALKUP;
            }
            else if (movementVector.y < 0.0)
            {
                currentState = AnimState.WALKDOWN;
            }
            else if (movementVector.x < 0.0)
            {
                currentState = AnimState.WALKLEFT;
            }
            else if (movementVector.x > 0.0)
            {
                currentState = AnimState.WALKRIGHT;
            }
        }

        myAnimator.SetBool("IsWalking", isWalking);

        myAnimator.SetInteger("WalkDirection", (int)currentState);
    }
}
