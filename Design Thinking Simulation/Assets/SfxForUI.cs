using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SfxForUI : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip onHoverSound;
    public AudioClip onClickedSound;
    public AudioClip AnswerSound;
    public AudioClip AnswerSound2;
    public AudioClip drumSound;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayHoverSound()
    {
        audioSource.clip = onHoverSound;
        audioSource.Play();
    }

    public void PlayClickedSound()
    {
        audioSource.clip = onClickedSound;
        audioSource.Play();
    }

    public void PlayAnswerSound()
    {
        int temp = UnityEngine.Random.Range(0, 2);
        if(temp == 0)
        {
            audioSource.clip = AnswerSound;
        }
        else
        {
            audioSource.clip = AnswerSound2;
        }
        audioSource.Play();
    }

    public void PlayDrumSound()
    {
        audioSource.clip = drumSound;
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
