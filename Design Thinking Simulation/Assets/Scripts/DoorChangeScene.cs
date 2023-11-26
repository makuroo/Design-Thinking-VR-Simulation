using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class DoorChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public string nextSceneName;
    public float AngleToChangeScene = 10;
    //float delayChangeScene = 1;
    //Vector3 localRotation;
    //float yAngle;
    [SerializeField] bool isHomeDoor;
    [SerializeField] bool isCafeDoor;
    [SerializeField] bool isRestaurantDoor;
    [SerializeField] bool isCanteenDoor;
    public Text world1Text;
    public Text world2Text;
    public Text world3Text;
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
        /*
        localRotation = transform.localEulerAngles;
        yAngle = Mathf.Floor(localRotation.y);
        if (yAngle >= 180)
        {
            yAngle -= 180;
        }
        else
        {
            yAngle = 180 - yAngle;
        }
        CheckDoorOpen();
        */
    }

    /*public void CheckDoorOpen()
    {
        if (yAngle > AngleToChangeScene && nextSceneName != null)
        {
            StartCoroutine(ChangeScene(delayChangeScene));
        }
    }*/

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerScript>())
        {
            canvasChangeScene.SetActive(false);
            //Debug.Log(other.gameObject);
        }    
    }

    /*IEnumerator ChangeScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName);
    }*/


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
            GameManager.Instance.canDoActivity = false;
        }
        else if (isRestaurantDoor)
        {
            SceneManager.LoadScene("Home");
            GameManager.Instance.canDoActivity = false;
        }
        else if (isCanteenDoor)
        {
            SceneManager.LoadScene("Home");
            GameManager.Instance.canDoActivity = false;
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
