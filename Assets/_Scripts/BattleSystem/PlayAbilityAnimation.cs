using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAbilityAnimation : MonoBehaviour
{
    public Animator animationPlayer;
    public AnimatorOverrideController clipSwapper;
    public SpriteRenderer sprite;
    public DamageEffect effect;
       
    // Start is called before the first frame update
    void Start()
    {
        animationPlayer = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        effect.playAnimation.AddListener(PlayAnimation);
        animationPlayer.runtimeAnimatorController = clipSwapper;
    }

    void PlayAnimation(AnimationClip animation)
    {
        if (animation)
        {
            sprite.enabled = true;

            RuntimeAnimatorController runtimePlayer = animationPlayer.runtimeAnimatorController;
            clipSwapper[runtimePlayer.animationClips[0].name] = animation;

            StartCoroutine(animationDuration(animation));
        }
    }

    IEnumerator animationDuration(AnimationClip animation)
    {
        animationPlayer.SetTrigger("Play");
        yield return new WaitForSeconds(animation.length);

        sprite.enabled = false;
    }
}
