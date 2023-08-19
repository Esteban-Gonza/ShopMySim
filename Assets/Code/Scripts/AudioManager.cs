using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour{

    [SerializeField] AudioClip coin;
    [SerializeField] AudioClip cashMachine;
    private AudioSource audioSource;

    #region Singleton class: AudioManager

    public static AudioManager instance;

    private void Awake(){

        if (instance == null){
            instance = this;
        }
    }
    #endregion

    private void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound){
        audioSource.PlayOneShot(sound);
    }

    public void PlayCoin(){
        audioSource.PlayOneShot(coin);
    }

    public void CashMachine(){
        audioSource.PlayOneShot(cashMachine);
    }
}