using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ChangeSceneScript : MonoBehaviour
{
    public float AngleToChangeScene = 10;
    [SerializeField] bool isHomeDoor;
    [SerializeField] bool isCafeDoor;
    [SerializeField] bool isRestaurantDoor;
    [SerializeField] bool isCanteenDoor;
    public TextMeshProUGUI world1Text;
    public TextMeshProUGUI world2Text;
    public TextMeshProUGUI world3Text;
    public GameObject canvasChangeScene;

    void Start()
    {
        if(isHomeDoor)
        {
            world1Text.text = "Cafe";
            world2Text.text = "Restaurant";
            world3Text.text = "Canteen";
        }
        else if(isCafeDoor)
        {
            world1Text.text = "Home";
            world2Text.text = "Restaurant";
            world3Text.text = "Canteen";
        }
        else if(isRestaurantDoor)
        {
            world1Text.text = "Home";
            world2Text.text = "Cafe";
            world3Text.text = "Canteen";
        }
        else if(isCanteenDoor)
        {
            world1Text.text = "Home";
            world2Text.text = "Cafe";
            world3Text.text = "Restaurant";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<BNG.PlayerScript>())
        {
            canvasChangeScene.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BNG.PlayerScript>())
        {
            canvasChangeScene.SetActive(false);
            //Debug.Log(other.gameObject);
        }    
    }


    public void Button1()
    {
        if (isHomeDoor)
        {
            SceneManager.LoadScene("Cafe");
            GameManager.Instance.canDoActivity = false;
        }
        else if (isCafeDoor)
        {
            SceneManager.LoadScene("Home");
        }
        else if (isRestaurantDoor)
        {
            SceneManager.LoadScene("Home");
        }
        else if (isCanteenDoor)
        {
            SceneManager.LoadScene("Home");
        }
    }

    public void Button2()
    {
        if (isHomeDoor)
        {
            SceneManager.LoadScene("Restaurant");
            GameManager.Instance.canDoActivity = false;
        }
        else if (isCafeDoor)
        {
            SceneManager.LoadScene("Restaurant");
        }
        else if (isRestaurantDoor)
        {
            SceneManager.LoadScene("Cafe");
        }
        else if (isCanteenDoor)
        {
            SceneManager.LoadScene("Cafe");
        }
    }

    public void Button3()
    {
        if (isHomeDoor)
        {
            SceneManager.LoadScene("Canteen");
            GameManager.Instance.canDoActivity = false;
        }
        else if (isCafeDoor)
        {
            SceneManager.LoadScene("Canteen");
        }
        else if (isRestaurantDoor)
        {
            SceneManager.LoadScene("Canteen");
        }
        else if (isCanteenDoor)
        {
            SceneManager.LoadScene("Restaurant");
        }
    }

    public void SetActiveCanvasChangeScene(bool isActive)
    {
        canvasChangeScene.SetActive(isActive);
    }

}
