using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static Unity.VisualScripting.Round<TInput, TOutput>;

public class backgroundMusic : MonoBehaviour
{

    private AudioSource audiosource;
    public AudioClip chillMusic;
    public AudioClip combatMusic;
    public PplayerMovement myplayer;

    // Start is called before the first frame update
    void Start()
    {
        myplayer = FindAnyObjectByType<PplayerMovement>();
        audiosource = GetComponent<AudioSource>();
        if (myplayer.isInCombat)
        {
            audiosource.volume = 0.01f;
            audiosource.clip = combatMusic;
            audiosource.loop = true;
            audiosource.Play();
        }
        else
        {
            audiosource.volume = 0.01f;
            audiosource.clip = chillMusic;
            audiosource.loop = true;
            audiosource.Play();
        }
    }
}
