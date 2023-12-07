using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanteenRandomizerOpenShop : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> shop = new List<GameObject>();
    void Start()
    {
        switch (GameManager.Instance.CanteenOpenShop)
        {
            case 1:
                shop[8].SetActive(false);
                break;
            case 2:
                shop[3].SetActive(false);
                shop[7].SetActive(false);
                // Do something for shop 2
                break;
            case 3:
                shop[2].SetActive(false);
                shop[4].SetActive(false);
                shop[8].SetActive(false);
                // Do something for shop 3
                break;
            case 4:
                shop[1].SetActive(false);
                shop[3].SetActive(false);
                shop[5].SetActive(false);
                shop[2].SetActive(false);
                // Do something for shop 4
                break;
            case 5:
                shop[1].SetActive(false);
                shop[2].SetActive(false);
                shop[5].SetActive(false);
                shop[6].SetActive(false);
                shop[8].SetActive(false);
                // Do something for shop 5
                break;
            case 6:
                shop[3].SetActive(false);
                shop[4].SetActive(false);
                shop[6].SetActive(false);
                shop[7].SetActive(false);
                shop[8].SetActive(false);
                shop[2].SetActive(false);
                // Do something for shop 6
                break;
            case 7:
                shop[1].SetActive(false);
                shop[2].SetActive(false);
                shop[4].SetActive(false);
                shop[5].SetActive(false);
                shop[6].SetActive(false);
                shop[7].SetActive(false);
                shop[8].SetActive(false);
                // Do something for shop 7
                break;
            case 8:
                foreach(GameObject gate in shop)
                {
                    gate.SetActive(true);
                }
                // Do something for shop 8
                break;
            default:
                // If the shop number doesn't match any case
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
