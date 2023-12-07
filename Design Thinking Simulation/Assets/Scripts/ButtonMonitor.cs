using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ButtonMonitor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] screen;
    public float[] durationTutorial;
    int currentScreen = 1;
    [SerializeField] int currentTutorialScreen = 1;
    PlayerScript playerScript;

    private void Awake()
    {
        playerScript = GameObject.Find("PlayerController").GetComponent<PlayerScript>();
    }

    void Start()
    {
        if (GameManager.Instance.hasFinishTutorial)
        {
            //ke screen index terakhir buat nunjukin ss suruh pencet keyboard
            NextScreen();
        }
        UpdateScreen(currentScreen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScreen()
    {
        if(currentScreen >= screen.Length)
        {
            currentScreen = 1;
        }
        else
        {
            currentScreen += 1;
        }
        UpdateScreen(currentScreen);
    }

    public void PreviousScreen()
    {
        if (currentScreen <= 1)
        {
            currentScreen = screen.Length;
        }
        else
        {
            currentScreen -= 1;
        }
        UpdateScreen(currentScreen);
    }

    private void UpdateScreen(int screenToOn)
    {
        int temp = 1;
        foreach (GameObject i in screen)
        {
            if (screenToOn == temp) i.SetActive(true);
            else i.SetActive(false);
            temp += 1;
        }
        Debug.Log(screenToOn);
    }
}
