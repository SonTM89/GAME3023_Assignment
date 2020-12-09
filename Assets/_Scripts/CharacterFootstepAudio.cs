using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFootstepAudio : MonoBehaviour
{
    [SerializeField]
    AudioClip[] footstepSounds;

    [SerializeField]
    AudioSource footstepSource;

    [Tooltip("Variance with which to pitch footsteps up/down both ways from 1")]
    [SerializeField]
    float pitchVariance = 2.0f;

    public void PlayFootstep()
    {
        footstepSource.clip = footstepSounds[0];
        // footstepSource.clip = footstepSounds[Random.Range(0, footstepSounds.Length)];

        footstepSource.pitch = Random.Range(1.0f - pitchVariance, 1.0f + pitchVariance);

        footstepSource.Play();
    }
}
