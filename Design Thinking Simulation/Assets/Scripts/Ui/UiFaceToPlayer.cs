using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFaceToPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform cameraTransform;
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 directionToCamera = transform.position - cameraTransform.position;
        directionToCamera.y = 0f;
        transform.rotation = Quaternion.LookRotation(directionToCamera);

    }
}
