using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAbilityAnimation : MonoBehaviour
{
    public DamageEffect effect;
       
    // Start is called before the first frame update
    void Start()
    {
        effect.playAnimation.AddListener(PlayAnimation);
    }

    void PlayAnimation(GameObject animation, int direction)
    {
        if (animation)
        {
            StartCoroutine(animationDuration(animation, direction));
        }
    }

    IEnumerator animationDuration(GameObject animation, int direction)
    {
        GameObject objectClone = Instantiate(animation);
        // I can change the direction of the animation by changing the sign of the scale of the parent.
        objectClone.transform.localScale = Vector3.Scale(objectClone.transform.localScale, new Vector3(direction, 1, 1));

        GameObject animationClone = objectClone.transform.GetChild(0).gameObject;
        Animator animator = animationClone.GetComponent<Animator>();

        animator.SetTrigger("Play");
        yield return new WaitForSeconds(animator.runtimeAnimatorController.animationClips[0].length);

        Destroy(objectClone);
    }
}
