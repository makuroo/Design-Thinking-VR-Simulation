using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsFaceToPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform cameraTransform;
    public bool disableFaceToPlayer;
    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!disableFaceToPlayer)
        {
            Vector3 directionToCamera = transform.position - cameraTransform.position;
            directionToCamera.y = 0f;
            transform.rotation = Quaternion.LookRotation(directionToCamera);
        }

    }
}
