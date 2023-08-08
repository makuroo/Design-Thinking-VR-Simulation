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


    private void Awake()
    {
        spawnRef1.gameObject.GetComponent<MeshRenderer>().enabled = false;
        spawnRef2.gameObject.GetComponent<MeshRenderer>().enabled = false;
        spawnRef3.gameObject.GetComponent<MeshRenderer>().enabled = false;
        spawnRef4.gameObject.GetComponent<MeshRenderer>().enabled = false;
        spawnRef5.gameObject.GetComponent<MeshRenderer>().enabled = false;
        spawnRef6.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetPosition(GameObject cust)
    {
        /*do
        {
            temp = Random.Range(1, 7);
        } while (isUsed[temp]);

        isUsed[temp] = true;*/

        temp = Random.Range(1, 7);


        switch (temp)
        {
            case 1:
                SetPositionAndRotation(cust, spawnRef1);
                break;
            case 2:
                SetPositionAndRotation(cust, spawnRef2);
                break;
            case 3:
                SetPositionAndRotation(cust, spawnRef3);
                break;
            case 4:
                SetPositionAndRotation(cust, spawnRef4);
                break;
            case 5:
                SetPositionAndRotation(cust, spawnRef5);
                break;
            case 6:
                SetPositionAndRotation(cust, spawnRef6);
                break;
            default:
                Debug.LogError("Invalid value for temp!");
                break;
        }
    }

    private void SetPositionAndRotation(GameObject cust, Transform targetTransform)
    {
        cust.transform.position = targetTransform.position;
        cust.transform.rotation = targetTransform.rotation;
    }
}
