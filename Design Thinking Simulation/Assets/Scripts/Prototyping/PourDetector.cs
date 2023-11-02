using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    [Header("Specific Object Mechanism")]
    public bool isLemonSqueezer = false;
    public bool isGrater = false;
    
    private bool isPouring = false;
    private Stream currentStream = null;
    private bool isLemonSqueezerPouring = false;
    
    [SerializeField]
    private List<GameObject> graterTrigger = new();
    public bool isAddLemonZest = false;

    // Update is called once per frame
    void Update()
    {
        if (!isLemonSqueezer && !isGrater)
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
    }

    private void StartPour()
    {
        print("Start");
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    {
        print("End");
        currentStream.End();
        currentStream = null;
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
                continue;
            }

            bool isAllItem = true;
            for (int i = 0; i < graterTrigger.Count; i++)
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
