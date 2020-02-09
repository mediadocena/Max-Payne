using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{

    private AudioSource audioSource;
    public AudioClip Shoot;
    public AudioClip SlowShoot;
    private bulletTime btime;
    [SerializeField]
    private AudioClip scream_Clip, die_Clip;

    [SerializeField]
    private AudioClip[] attack_Clips;

    // Use this for initialization
    void Awake()
    {
        btime = GameObject.FindWithTag("BulletTime").GetComponent<bulletTime>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Play_ScreamSound()
    {
        audioSource.clip = scream_Clip;
        audioSource.Play();
    }

    public void Play_AttackSound()
    {
        Debug.Log(btime);
        if (btime.isSlowed)
        {
            audioSource.PlayOneShot(SlowShoot, 1f);
        }
        else { 
            audioSource.PlayOneShot(Shoot, 1f);
        }
    }

    public void Play_DeadSound()
    {
        audioSource.clip = die_Clip;
        audioSource.Play();
    }

} // class











