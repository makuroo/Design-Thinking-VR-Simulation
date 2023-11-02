using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    public GameObject ovenDoor;
    public GameObject bowl;
    private Baking bowlScript;

    // Start is called before the first frame update
    void Start()
    {
        bowlScript = bowl.GetComponent<Baking>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ovenDoor.transform.localRotation.x < -135f)
        {
            ovenDoor.transform.localRotation = Quaternion.Euler(-135f, 0f, 0f);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bowl"))
        {
            Debug.Log("Bowl Detected");
            if (bowlScript.isBakeReady)
            {
                Debug.Log("Ready to bake");
                Destroy(bowl);
            }
        }
    }
}
