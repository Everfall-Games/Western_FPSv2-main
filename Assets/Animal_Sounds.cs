using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores and plays animal sounds
/// </summary>
public class Animal_Sounds : MonoBehaviour
{

    public GameObject bear;
    private AudioSource bearAudioSource;
    public GameObject deer;
    private AudioSource deerAudioSource;
    public GameObject rabbit;
    private AudioSource rabbitAudioSource;


    #region Sounds
    public List<AudioClip> bearFootsteps;
    public List<AudioClip> bearRoar;

    public List<AudioClip> deerFootsteps;
    public List<AudioClip> deerVocalization;


    public List<AudioClip> rabbitFootsteps;

    #endregion
    private void Start()
    {
        bearAudioSource = bear.GetComponent<AudioSource>();
       // deerAudioSource = deer.GetComponent<AudioSource>();
      //  rabbitAudioSource = rabbit.GetComponent<AudioSource>();
    }
    #region Methods
    public void PlaySound(float rola)
    {
        Debug.Log(rola);
    }
    #endregion
}
