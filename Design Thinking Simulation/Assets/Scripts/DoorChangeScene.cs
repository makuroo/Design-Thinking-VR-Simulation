using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public string nextSceneName;
    public float AngleToChangeScene = 10;
    float delayChangeScene = 1;
    Vector3 localRotation;
    float yAngle;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        Debug.Log(yAngle);
        CheckDoorOpen();



    }

    public void CheckDoorOpen()
    {
        if (yAngle > AngleToChangeScene && nextSceneName != null)
        {
            StartCoroutine(ChangeScene(delayChangeScene));
        }
    }

    IEnumerator ChangeScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName);
    }
}
