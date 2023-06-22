using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFaceToPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform cameraTransform;
    Canvas thisCanvas;
    Camera cameraCaster;
    private void Awake()
    {
        thisCanvas = GetComponent<Canvas>();
        cameraTransform = Camera.main.transform;
        //reference si cameracaster
    }
    void Start()
    {
        cameraCaster = GameObject.Find("CameraCaster").GetComponent<Camera>();
        if(thisCanvas != null)
        {
            thisCanvas.worldCamera = cameraCaster;
        }
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 directionToCamera = transform.position - cameraTransform.position;
        directionToCamera.y = 0f;
        transform.rotation = Quaternion.LookRotation(directionToCamera);

    }
}
