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
        if(GameManager.Instance.interviewCount>=3)
        {
            problemStatementTextDays.text = "Terbuka";
        }
        else
        {
            problemStatementTextDays.text = "Menyelesaikan: <color=red>" + GameManager.Instance.interviewCount.ToString() + "</color>/ 3 <b>User Persona</b>";
        }

        if (GameManager.Instance.hasDoneProblemStatement)
        {
            vpcTextDays.text = "Terbuka";
        }
        else
        {
            vpcTextDays.text = "Menyelesaikan 1 <b>Problem Statement</b>";
        }
    }
}
