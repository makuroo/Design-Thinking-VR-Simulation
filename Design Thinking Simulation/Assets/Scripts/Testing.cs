using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public Transform rightHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCake();
    }

    public void SpawnCake()
    {
        if (GameManager.Instance.isTesting)
        {
            GameObject cake = Instantiate(GameManager.Instance.bakedCake.cakeAsset, rightHand, worldPositionStays:false);
            cake.transform.localScale = new Vector3(0.085f, 0.085f, 0.085f);
        }
    }
}
