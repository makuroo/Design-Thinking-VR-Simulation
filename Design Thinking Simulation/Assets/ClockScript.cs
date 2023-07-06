using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject minuteArrow;
    public GameObject hourArrow;

    public void SetClock(int minute, int hour)
    {
        minuteArrow.transform.rotation = Quaternion.Euler(0f, 180f, minute * 6);
        hourArrow.transform.rotation = Quaternion.Euler(0f, 180f, hour * 15);
    }
}
