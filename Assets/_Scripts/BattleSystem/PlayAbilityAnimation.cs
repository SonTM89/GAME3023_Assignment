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

    void PlayAnimation(GameObject animation)
    {
        if (animation)
        {
            //Animator animInstance = Instantiate(animation).GetComponent<Animator>();
            //animation.GetComponent<Animator>().SetTrigger("Play");
            StartCoroutine(animationDuration(animation));
        }
    }

    IEnumerator animationDuration(GameObject animation)
    {
        GameObject objectClone = Instantiate(animation);
        GameObject animationClone = objectClone.transform.GetChild(0).gameObject;
        Animator animator = animationClone.GetComponent<Animator>();

        animator.SetTrigger("Play");
        yield return new WaitForSeconds(animator.runtimeAnimatorController.animationClips[0].length);

        Destroy(animationClone);
    }
}
