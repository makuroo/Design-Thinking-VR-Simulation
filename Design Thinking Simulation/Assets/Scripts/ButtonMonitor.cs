using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ButtonMonitor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] screen;
    public GameObject[] tutorialScreen;
    public float[] durationTutorial;
    int currentScreen = 1;
    [SerializeField] int currentTutorialScreen = 1;
    bool isOnTutorial;
    PlayerScript playerScript;

    private void Awake()
    {
        playerScript = GameObject.Find("PlayerController").GetComponent<PlayerScript>();
    }

    void Start()
    {
        if (GameManager.Instance.hasFinishTutorial)
        {
            foreach(GameObject go in tutorialScreen)
            {
                go.SetActive(false);
            }
            screen[0].SetActive(true);
        }
            

        if (GameManager.Instance.currentDay == 1 && GameManager.Instance.questionRemaining == 3 && !GameManager.Instance.hasFinishTutorial)
        {
            currentTutorialScreen = 1;
            isOnTutorial = true;
            UpdateTutorialScreen(currentTutorialScreen);
            StartCoroutine(Tutorial1Done());
        }
        else if(GameManager.Instance.currentDay == 1 && GameManager.Instance.questionRemaining == 0)
        {
            //play tidur video
            currentTutorialScreen = 3;
            isOnTutorial = true;
            UpdateTutorialScreen(currentTutorialScreen);
        }
        else
        {
            isOnTutorial = false;
            UpdateScreen(currentScreen);
        }
    }

    IEnumerator Tutorial1Done()
    {
        /*yield return new WaitForSeconds(durationTutorial[currentTutorialScreen-1]);
        playerScript.ControllerVibrate(true);*/
        yield return new WaitForSeconds(durationTutorial[currentTutorialScreen - 1] -3);
        playerScript.ControllerVibrate(true);
        yield return new WaitForSeconds(1);
        playerScript.ControllerVibrate(true);
        yield return new WaitForSeconds(1);
        playerScript.ControllerVibrate(true);
        yield return new WaitForSeconds(1);
        playerScript.ControllerVibrate(true);
        StartTutorial2();
    }

    public void StartTutorial2()
    {
        currentTutorialScreen = 2;
        UpdateTutorialScreen(currentTutorialScreen);
        StartCoroutine(Tutorial2Done());
    }

    IEnumerator Tutorial2Done()
    {
        yield return new WaitForSeconds(durationTutorial[currentTutorialScreen-1]);
        GameManager.Instance.hasFinishTutorial = true;
        currentTutorialScreen = 0;
        isOnTutorial = false;
        UpdateTutorialScreen(currentTutorialScreen);
        currentScreen = 1;
        UpdateScreen(currentScreen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScreen()
    {
        if(!isOnTutorial)
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
    }

    public void PreviousScreen()
    {
        if(!isOnTutorial)
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
    }

    private void UpdateScreen(int screenToOn)
    {
        if(!isOnTutorial)
        {
            int temp = 1;
            foreach(GameObject i in screen)
            {
                if (screenToOn == temp) i.SetActive(true);
                else i.SetActive(false);
                temp += 1;
            }
            Debug.Log(screenToOn);
        }
    }


    private void UpdateTutorialScreen(int screenToOn)
    {
        int temp = 1;
        foreach (GameObject i in tutorialScreen)
        {
            if (screenToOn == temp) i.SetActive(true);
            else i.SetActive(false);
            temp += 1;
        }
    }
}
