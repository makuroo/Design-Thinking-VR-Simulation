using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class BoardActivityTrigger : MonoBehaviour
{
    [SerializeField] private BoardActivityUI board;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            board.handGrabber[0] = other.transform.Find("CameraRig/TrackingSpace/LeftHandAnchor/LeftControllerAnchor/LeftController/Grabber").GetComponent<Grabber>();
            board.handGrabber[1] = other.transform.Find("CameraRig/TrackingSpace/RightHandAnchor/RightControllerAnchor/RightController/Grabber").GetComponent<Grabber>();
        }
    }


    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
