using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;
    public GameObject flakePrefab = null;
    private GameObject flake = null;

    [Header("Item")]
    public GameObject topSqueezer;

    [Header("Specific Object Mechanism")]
    public bool isLemonSqueezer = false;
    public bool isGrater = false;
    public bool isFlake = false;
    
    private bool isPouring = false;
    private Stream currentStream = null;
    private bool isLemonSqueezerPouring = false;
    
    [SerializeField]
    private List<GameObject> graterTrigger = new();
    [HideInInspector]
    public bool isAddLemonZest = false;

    // Update is called once per frame
    void Update()
    {
        if (!isLemonSqueezer && !isGrater && !isFlake)
        {
            bool pourCheck = CalculatePourAngle() < pourThreshold;

            if (isPouring != pourCheck)
            {
                isPouring = pourCheck;

                if (isPouring)
                {
                    StartPour();
                }
                else 
                {
                    EndPour();
                }
            }
        } 
        else if (isLemonSqueezer)
        {
            if (isPouring != isLemonSqueezerPouring)
            {
                isPouring = isLemonSqueezerPouring;

                if (isPouring)
                {
                    StartPour();
                }
                else 
                {
                    EndPour();
                }
            }
        }
        else if (isGrater)
        {
            if (isPouring != isAddLemonZest)
            {
                print("zesting valid");
                isPouring = isAddLemonZest;

                if (isPouring)
                {
                    print("start zesting");
                    StartPour();
                }
                else 
                {
                    EndPour();
                }
            }
        }
        else if (isFlake)
        {
            bool pourCheck = CalculatePourAngle() < pourThreshold;

            if (isPouring != pourCheck)
            {
                isPouring = pourCheck;

                if (isPouring)
                {
                    StartFlake();
                }
                else 
                {
                    EndFlake();
                }
            }
        }
    }

    private void StartPour()
    {
        currentStream = CreateStream();
        currentStream.Begin();
    }


    private void EndPour()
    {
        currentStream.End();
        currentStream = null;
    }

    private void StartFlake()
    {
        flake = Instantiate(flakePrefab, origin.position, Quaternion.identity, transform);
    }

    private void EndFlake()
    {
        Destroy(flake);
    }

    private float CalculatePourAngle()
    {
        //check mesh
        return transform.forward.y * Mathf.Rad2Deg;
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }


    private void OnTriggerEnter(Collider other) {
        if (isLemonSqueezer)
        {
            if (other.gameObject.CompareTag("Bowl"))
            {
                isLemonSqueezerPouring = true;
                topSqueezer.transform.localRotation = Quaternion.Euler(-88.08f, 0f, 0f);
            }
        }
        else if (isGrater)
        {
            graterTrigger.Add(other.gameObject);

            bool[] checkItem = new bool[2];
            for (int i = 0; i < 2; i++)
            {
                checkItem[i] = false;
            }
            
            for(int i = 0; i < graterTrigger.Count; i++)
            {
                if (graterTrigger[i].name == "Bowl") checkItem[0] = true;
                if (graterTrigger[i].name == "Lemon Half") checkItem[1] = true;
            }

            bool isAllItem = true;
            for (int i = 0; i < checkItem.Length; i++)
            {
                if(!checkItem[i]) isAllItem = false;
            }

            if (isAllItem)
            {
                print("Ready to zest");
                isAddLemonZest = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isLemonSqueezer)
        {
            if (other.gameObject.CompareTag("Bowl"))
            {
                isLemonSqueezerPouring = false;
                topSqueezer.transform.localRotation = Quaternion.Euler(-21.23f, 0f, 0f);
            }
        }
        else if (isGrater)
        {
            if (graterTrigger.IndexOf(other.gameObject) > -1)
            {
                graterTrigger.Remove(other.gameObject);
                isAddLemonZest = false;
            }
        }
    }
}
