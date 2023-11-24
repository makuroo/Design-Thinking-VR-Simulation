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

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bowl"))
        {
            Debug.Log("Bowl Detected");
            if (bowlScript.isBakeReady)
            {
                Debug.Log("Ready to bake");
                ovenDoor.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
                //Add cake here
                Destroy(bowl);
            }
        }
    }
}
