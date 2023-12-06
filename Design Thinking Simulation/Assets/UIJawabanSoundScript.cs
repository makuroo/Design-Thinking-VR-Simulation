using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIJawabanSoundScript : MonoBehaviour
{
    AudioSource soundJawaban;

    // Start is called before the first frame update
    void Start()
    {
        soundJawaban = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnEnable()
    {
        soundJawaban.Play();
    }
}
