using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // Start is called before the first frame update


    private void Start()
    {
        GameManager.Instance.GetClockReference();
        GameManager.Instance.GetDirectionalLight();
        GameManager.Instance.CanAskCheck();
    }

    private void Update()
    {
        
    }

    public void PlayerSleep()
    {
        GameManager.Instance.PlayerSleep();
        GameManager.Instance.DistributeCustomerCount(); // reset distribusi customer setiap ganti hari
        GameManager.Instance.ClearCustomer();
        GameManager.Instance.RandomizeCustomer();
        GameManager.Instance.RandomizeQuestion();
    }

}
