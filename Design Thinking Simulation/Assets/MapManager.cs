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

    public void PlayerSleep()
    {
        GameManager.Instance.PlayerSleep();
        GameManager.Instance.ClearCustomer();
        GameManager.Instance.RandomizeCustomer();
        GameManager.Instance.RandomizeQuestion();
    }

}
