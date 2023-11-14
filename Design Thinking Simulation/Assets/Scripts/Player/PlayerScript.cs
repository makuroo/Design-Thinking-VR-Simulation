using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using BNG;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI chanceText;

    public ScreenDetector screenDetector;

    bool isControllerVibrateRepeat;
    bool isStoppedVibrateCommand;
    //    public ScreenDetector screenDetectorScript;

    ScreenFader screenFaderScriptMain;
    GameObject UICamera;
    Image clockImage;

    private void Awake()
    {
        GameManager.Instance.GetPlayerRef();
    }

    void Start()
    {
        screenFaderScriptMain = transform.Find("CameraRig/TrackingSpace/CenterEyeAnchor").GetComponent<ScreenFader>();
        UICamera = transform.Find("CameraRig/TrackingSpace/CenterEyeAnchor/CenterEyeAnchorUI").gameObject;
        clockImage = transform.Find("CameraRig/TrackingSpace/LeftHandAnchor/LeftControllerAnchor/ClockCanvas/ClockImage").GetComponent<Image>();
        Debug.Log(clockImage);
        GameManager.Instance.CanAskCheck();
        Debug.Log(clockImage.color);
        Debug.Log("jalan woii ini di start player");
    }

    // Update is called once per frame
    void Update()
    { 
        if (InputBridge.Instance.GetControllerBindingValue(ControllerBinding.AButtonDown))
        {
            GameManager.Instance.NewGame();
        }
        if (InputBridge.Instance.GetControllerBindingValue(ControllerBinding.BButtonDown))
        {
            StartCoroutine(DoFadeInFadeOut());
        }
    }

    public void PlayerAsk()
    {
        GameManager.Instance.UseAskChance();
        GameManager.Instance.SaveGame();
    }

    public void SetDayText()
    {
        dayText.text = GameManager.Instance.currentDay.ToString();
    }

    public void SetChanceText()
    {
        chanceText.text = GameManager.Instance.questionRemaining.ToString();
    }

    public void ControllerVibrate(bool isRepeatVibrate)
    {
        isControllerVibrateRepeat = isRepeatVibrate;
        InputBridge.Instance.VibrateController(0.1f, 0.2f, .5f, ControllerHand.Left);
        if (!isStoppedVibrateCommand)
        {
            if (isControllerVibrateRepeat)
            {
                    
                //StartCoroutine(ControllerVibrateRepeat());
            }
        }
        else
        {
            isStoppedVibrateCommand = false;
        }

    }

    /*public void StopRepeatingVibrate()
    {
        isStoppedVibrateCommand = true;
        CancelInvoke();
    }

    public void ControllerVibrateRepeat()
    {
        InvokeRepeating("ControllerVibrate", 2f, 2f);
    }

    /*IEnumerator ControllerVibrateRepeat()
    {
        yield return new WaitForSeconds(2);
        ControllerVibrate(isControllerVibrateRepeat);
    }*/

    public void DoFadeInFadeOutFunction()
    {
        StartCoroutine(DoFadeInFadeOut());
    }

    public void DoFadeIn()
    {
        UICamera.SetActive(false);
        screenFaderScriptMain.DoFadeIn();
    }

    IEnumerator DoFadeInFadeOut()
    {
        UICamera.SetActive(false);
        screenFaderScriptMain.DoFadeIn();
        yield return new WaitForSeconds(screenFaderScriptMain.FadeInSpeed);
        screenFaderScriptMain.DoFadeOut();
        UICamera.SetActive(true);
    }

    public void ChangeClockColorToRed()
    {
        clockImage.color = new Color32(255, 84, 64, 255);
    }

    public void ChangeClockColorToGreen()
    {
        clockImage.color = new Color32(161, 255, 156, 255);
    }


}

