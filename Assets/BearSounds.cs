using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearSounds : MonoBehaviour
{
    public GameObject bear;
    public AudioSource bearAudioSourceFootsteps;
    public AudioSource bearAudioSourceOther;
    public List<AudioClip> bearFootsteps;
    public List<AudioClip> bearFootstepsRun;
    public List<AudioClip> bearHurt;
    public List<AudioClip> bearIdle;
    public List<AudioClip> bearAttack;

    private float bearSpeed;

    private int audioIndex;

    private void Awake()
    {
        if (bear == null)
            bear = this.gameObject;

    }
    private void Update()
    {
        bearSpeed = bear.GetComponent<Animator>().GetFloat("WalkSpeed");
    }
    public void PlayFootstepSound()
    {
        
        //Audio index to play
        if (!bearAudioSourceFootsteps.isPlaying && bearSpeed < 9 )
        {
            Debug.Log("Playing sound");
            audioIndex = Random.Range(0, bearFootsteps.Count);
            bearAudioSourceFootsteps.PlayOneShot(bearFootsteps[audioIndex]);

            //Store previous audio clip so we don't repeat
            AudioClip previousAudioClip = bearFootsteps[audioIndex];

            //Randomize audioIndex without repeating
            while (previousAudioClip == bearFootsteps[audioIndex])
                audioIndex = Random.Range(0, bearFootsteps.Count);
        }
    }

    public void PlayFootstepSoundRun()
    {
        Debug.Log("Playing sound Run");
        //Audio index to play

        audioIndex = Random.Range(0, bearFootstepsRun.Count);
        bearAudioSourceFootsteps.PlayOneShot(bearFootstepsRun[audioIndex]);

         //Store previous audio clip so we don't repeat
         AudioClip previousAudioClip = bearFootstepsRun[audioIndex];

         //Randomize audioIndex without repeating
         while (previousAudioClip == bearFootstepsRun[audioIndex])
             audioIndex = Random.Range(0, bearFootstepsRun.Count);
        
    }

    public void PlayIdleSound(int playChance)
    {
        int chance = Random.Range(0, 100);
        //Play random sound from our sound list
        if (chance < playChance && !bearAudioSourceOther.isPlaying)
            bearAudioSourceOther.PlayOneShot(bearIdle[Random.Range(0, bearIdle.Count)]);
    }

    public void PlayAttackSound(int playChance)
    {
        int chance = Random.Range(0, 100);
        bearAudioSourceOther.pitch = Random.Range(0.85f, 1.05f);
        //Play random sound from our sound list
        if (chance < playChance && !bearAudioSourceOther.isPlaying)
            bearAudioSourceOther.PlayOneShot(bearAttack[Random.Range(0, bearAttack.Count)]);
    }

    public void PlayHurtSound(int playChance)
    {
        int chance = Random.Range(0, 100);
        bearAudioSourceOther.pitch = 1;
        //Play random sound from our sound list
        if (chance < playChance && !bearAudioSourceOther.isPlaying)
        {
            bearAudioSourceOther.pitch = Random.Range(0.6f, 1.4f);
            bearAudioSourceOther.PlayOneShot(bearHurt[Random.Range(0, bearHurt.Count)]);
        }
           
    }
}
