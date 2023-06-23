using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BNG;

public class DragAndDropAnswerChecker : MonoBehaviour
{
    public EmpathyMapButtons empathyMapButtons;
    private EmpathyMapSO personCustomerEmpathy;
    private Grabbable currentGrabbable;
    private TMP_Text currentText;

    public void ChooseChecker()
    {
        if (gameObject.GetComponent<SnapZone>().HeldItem != null)
        {
            currentGrabbable = gameObject.GetComponent<SnapZone>().HeldItem;
            currentText = currentGrabbable.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        }
        if (empathyMapButtons.empathyChecker == EmpathyMapButtons.EmpathyType.Think)
            ThinkAnswerChecker(currentText);
        else if (empathyMapButtons.empathyChecker == EmpathyMapButtons.EmpathyType.Does)
            DoesAnswerChecker(currentText);
        else if (empathyMapButtons.empathyChecker == EmpathyMapButtons.EmpathyType.Feels)
            FeelsAnswerChecker(currentText);
        else
           SaysAnswerChecker(currentText);
    }

    private void SaysAnswerChecker(TMP_Text currentText)
    {
        personCustomerEmpathy = GameManager.Instance.personCustomerEmpathy;
        for (int i = 0; i < personCustomerEmpathy.Says.Count; i++)
        {
            if (currentText.text == personCustomerEmpathy.Says[i])
            {
                Debug.Log("True");
                break;
            }
            else
            {
                Debug.Log("False");
            }
        }
    }

    private void FeelsAnswerChecker(TMP_Text currentText)
    {
        personCustomerEmpathy = GameManager.Instance.personCustomerEmpathy;
        for (int i = 0; i < personCustomerEmpathy.Feels.Count; i++)
        {
            if (currentText.text == personCustomerEmpathy.Feels[i])
            {
                Debug.Log("True");
                break;
            }
            else
            {
                Debug.Log("False");
            }
        }
    }

    private void DoesAnswerChecker(TMP_Text currentText)
    {
        personCustomerEmpathy = GameManager.Instance.personCustomerEmpathy;
        for (int i = 0; i < personCustomerEmpathy.Does.Count; i++)
        {
            if (currentText.text == personCustomerEmpathy.Does[i])
            {
                Debug.Log("True");
                break;
            }
            else
            {
                Debug.Log("False");
            }
        }
    }

    public void ThinkAnswerChecker(TMP_Text currentText)
    {
        personCustomerEmpathy = GameManager.Instance.personCustomerEmpathy;
        for (int i = 0; i < personCustomerEmpathy.Thinks.Count; i++)
        {

            if (currentText.text == personCustomerEmpathy.Thinks[i])
            {
                Debug.Log("True");
                break;
            }
            else
            {
                Debug.Log("False");
            }
        }
    }
}
