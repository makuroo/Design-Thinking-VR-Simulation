using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivityDaysUI : MonoBehaviour
{
    public BoardActivityUI board;
    public TMP_Text userPersonaTextDays;
    public TMP_Text problemStatementTextDays;
    public TMP_Text vpcTextDays;
    
    private void OnEnable()
    {
        if(board.userPersonaActiveDay - GameManager.Instance.currentDay <= 0)
        {
            userPersonaTextDays.text = "Unlocked";
        }
        else
        {
            userPersonaTextDays.text = board.userPersonaActiveDay - GameManager.Instance.currentDay + "Day(s)";
        }

        if (board.problemStatementActiveDay - GameManager.Instance.currentDay <= 0)
        {
            problemStatementTextDays.text = "Unlocked";
        }
        else
        {
            problemStatementTextDays.text = board.userPersonaActiveDay - GameManager.Instance.currentDay + "Day(s)";
        }

        if (board.vpcActiveDay - GameManager.Instance.currentDay <= 0)
        {
            userPersonaTextDays.text = "Unlocked";
        }
        else
        {
            userPersonaTextDays.text = board.vpcActiveDay - GameManager.Instance.currentDay + "Day(s)";
        }
    }
}
