using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFade : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;
    private bool isFading = false;
    private float fadeTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.black;
        StartFadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFading)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Clamp01(fadeTimer / fadeDuration);

            fadeImage.color = new Color(0f, 0f, 0f, alpha);

            if(fadeTimer >= fadeDuration)
            {
                isFading = false;
                fadeImage.gameObject.SetActive(true);
            }
        }
    }


    public void StartFadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        fadeTimer = 0f;
        isFading = true;
    }

    public void StartFadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(0f, 0f, 0f, 0f);
        fadeTimer = 0f;
        isFading = true;
    }

}
