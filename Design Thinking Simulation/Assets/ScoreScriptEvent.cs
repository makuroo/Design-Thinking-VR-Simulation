using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScriptEvent : MonoBehaviour
{

    public AudioClip stampSound;
    public AudioClip successSound;
    public AudioClip failedSound;
    public AudioClip scoreCalculatingSound;
    public AudioClip scoreDoneSound;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StampSound()
    {
        audioSource.clip = stampSound;
        audioSource.Play();
    }

    public void PlayFailedSound()
    {
        audioSource.clip = failedSound;
        audioSource.Play();
    }

    public void PlaySuccessSound()
    {
        audioSource.clip = successSound;
        audioSource.Play();
    }
    
    public void PlayCalculatingScoreSound()
    {
        audioSource.clip = scoreCalculatingSound;
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public void PlayScoreDoneSound()
    {
        audioSource.clip = scoreDoneSound;
        audioSource.Play();
    }
}
