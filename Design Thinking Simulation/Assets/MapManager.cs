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
    }

    public void PlayerSleep()
    {
        GameManager.Instance.PlayerSleep();
    }

}
