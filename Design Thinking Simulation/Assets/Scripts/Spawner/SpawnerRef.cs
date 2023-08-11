using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRef : MonoBehaviour
{
    public Transform spawnRef1;
    public Transform spawnRef2;
    public Transform spawnRef3;
    public Transform spawnRef4;
    public Transform spawnRef5;
    public Transform spawnRef6;
    bool[] isUsed;
    int temp;
    int i;

    private void Awake()
    {
        spawnRef1.gameObject.GetComponent<MeshRenderer>().enabled = false;
        spawnRef2.gameObject.GetComponent<MeshRenderer>().enabled = false;
        spawnRef3.gameObject.GetComponent<MeshRenderer>().enabled = false;
        spawnRef4.gameObject.GetComponent<MeshRenderer>().enabled = false;
        spawnRef5.gameObject.GetComponent<MeshRenderer>().enabled = false;
        spawnRef6.gameObject.GetComponent<MeshRenderer>().enabled = false;
        isUsed = new bool[20];
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetPosition(GameObject cust)
    {
        do
        {
            i += 1;
            temp = Random.Range(1, 7);
            Debug.Log("Random Range Ref" + temp + "      On try = " + i );
        } while (isUsed[temp] == true);
        Debug.Log("temp = " + temp);
        isUsed[temp] = true;
        

        switch (temp)
        {
            case 1:
                SetPositionAndRotation(cust, spawnRef1);
                Debug.Log("Set ref1");
                break;
            case 2:
                SetPositionAndRotation(cust, spawnRef2);
                Debug.Log("Set ref2");
                break;
            case 3:
                SetPositionAndRotation(cust, spawnRef3);
                Debug.Log("Set ref3");
                break;
            case 4:
                SetPositionAndRotation(cust, spawnRef4);
                Debug.Log("Set ref4");
                break;
            case 5:
                SetPositionAndRotation(cust, spawnRef5);
                Debug.Log("Set ref5");
                break;
            case 6:
                SetPositionAndRotation(cust, spawnRef6);
                Debug.Log("Set ref6");
                break;
            default:
                Debug.LogError("Invalid value for temp!");
                break;
        }
    }

    public void SearchSpawnRef()
    {

    }

    private void SetPositionAndRotation(GameObject cust, Transform targetTransform)
    {
        cust.transform.position = targetTransform.position;
        cust.transform.rotation = targetTransform.rotation;
    }
}
