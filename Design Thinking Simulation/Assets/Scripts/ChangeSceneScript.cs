using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ChangeSceneScript : MonoBehaviour
{
    [SerializeField] bool isHomeDoor;
    [SerializeField] bool isCafeDoor;
    [SerializeField] bool isMall;
    [SerializeField] bool isCanteenDoor;
    public TextMeshProUGUI world1Text;
    public TextMeshProUGUI world2Text;
    public TextMeshProUGUI world3Text;
    public GameObject canvasChangeScene;
    PlayerScript playerScript;

    void Start()
    {
        if(isHomeDoor)
        {
            world1Text.text = "Cafe";
            world2Text.text = "Mall";
            world3Text.text = "Canteen";
        }
        else if(isCafeDoor)
        {
            world1Text.text = "Home";
            world2Text.text = "Mall";
            world3Text.text = "Canteen";
        }
        else if(isMall)
        {
            world1Text.text = "Home";
            world2Text.text = "Cafe";
            world3Text.text = "Canteen";
        }
        else if(isCanteenDoor)
        {
            world1Text.text = "Home";
            world2Text.text = "Cafe";
            world3Text.text = "Mall";
        }
        playerScript = GameObject.Find("PlayerController").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerScript>())
        {
            canvasChangeScene.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerScript>())
        {
            canvasChangeScene.SetActive(false);
            //Debug.Log(other.gameObject);
        }    
    }

    public void LoadSceneAsyncFunction(string tempString)
    {
        StartCoroutine(LoadSceneAsync(tempString));
    }

    IEnumerator LoadSceneAsync(string tempString)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(tempString);
        asyncOperation.allowSceneActivation = false; //set to false if u dont want to load it immediately
        yield return new WaitForSeconds(1f);
        asyncOperation.allowSceneActivation = true; //set to false if u dont want to load it immediately
    }

    public void Button1()
    {
        if (isHomeDoor)
        {
            playerScript.DoFadeIn();
            LoadSceneAsyncFunction("Cafe");
            GameManager.Instance.canDoActivity = false;
        }
        else if (isCafeDoor)
        {
            playerScript.DoFadeIn();
            LoadSceneAsyncFunction("Home");
        }
        else if (isMall)
        {
            playerScript.DoFadeIn();
            LoadSceneAsyncFunction("Home");
        }
        else if (isCanteenDoor)
        {
            playerScript.DoFadeIn();
            LoadSceneAsyncFunction("Home");
        }
    }

    public void Button2()
    {
        if (isHomeDoor)
        {
            playerScript.DoFadeIn();
            LoadSceneAsyncFunction("Mall");
            GameManager.Instance.canDoActivity = false;
        }
        else if (isCafeDoor)
        {
            playerScript.DoFadeIn();
            LoadSceneAsyncFunction("Mall");
        }
        else if (isMall)
        {
            playerScript.DoFadeIn();
            LoadSceneAsyncFunction("Cafe");
        }
        else if (isCanteenDoor)
        {
            playerScript.DoFadeIn();
            LoadSceneAsyncFunction("Cafe");
        }
    }

    public void Button3()
    {
        if (isHomeDoor)
        {
            playerScript.DoFadeIn();
            LoadSceneAsyncFunction("Canteen");
            GameManager.Instance.canDoActivity = false;
        }
        else if (isCafeDoor)
        {
            playerScript.DoFadeIn();
            LoadSceneAsyncFunction("Canteen");
        }
        else if (isMall)
        {
            playerScript.DoFadeIn();
            LoadSceneAsyncFunction("Canteen");
        }
        else if (isCanteenDoor)
        {
            playerScript.DoFadeIn();
            LoadSceneAsyncFunction("Mall");
        }
    }

    public void SetActiveCanvasChangeScene(bool isActive)
    {
        canvasChangeScene.SetActive(isActive);
    }

}
