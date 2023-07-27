using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BNG;
using System;

public class DragAndDropAnswerChecker : MonoBehaviour
{
    public UserPersonaUI userPersonaUI;
    private CustomerDataSO customerData;
    private Grabbable currentGrabbable;
    private TMP_Text currentText;
    public int index;
    public People customer;

    public void ChooseChecker()
    {
        if (gameObject.GetComponent<SnapZone>().HeldItem != null)
        {
            currentGrabbable = gameObject.GetComponent<SnapZone>().HeldItem;
            currentText = currentGrabbable.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        }
        if (userPersonaUI.userPersonaChecker == UserPersonaUI.UserPersonaCategory.Goals)
            GoalsAnswerChecker(currentText);
        else if (userPersonaUI.userPersonaChecker == UserPersonaUI.UserPersonaCategory.Frustration)
            FrustrationAnswerChecker(currentText);
        else if (userPersonaUI.userPersonaChecker == UserPersonaUI.UserPersonaCategory.Taste)
            TasteAnswerChecker(currentText, index, customer);
        else
            FavouriteCakeAnswerChecker(currentText);
        //else if (userPersonaUI.userPersonaChecker == UserPersonaUI.UserPersonaCategory.Image)
        //    FeelsAnswerChecker(currentText);
        //else
        //    SaysAnswerChecker(currentText);
    }

    private void FavouriteCakeAnswerChecker(TMP_Text currentText)
    {
        if (customer.customerData.kueFavorit == currentText.text)
        {
            Debug.Log("true");
        }else
        {
            Debug.Log("false");
        }
    }

    //private void SaysAnswerChecker(TMP_Text currentText)
    //{
    //    customerData = GameManager.Instance.personCustomerData;
    //    for (int i = 0; i < personCustomerEmpathy.Says.Count; i++)
    //    {
    //        if (currentText.text == personCustomerEmpathy.Says[i])
    //        {
    //            Debug.Log("True");
    //            break;
    //        }
    //        else
    //        {
    //            Debug.Log("False");
    //        }
    //    }
    //}

    private void TasteAnswerChecker(TMP_Text currentText, int index, People customer)
    {
        if (customer.CalculateLikeness(index) == 0 && currentText.text == "Neutral")
        {
            Debug.Log("n");
        }

        else if (customer.CalculateLikeness(index) == 1 && currentText.text == "Like")
        {
            Debug.Log("l");
        }

        else if (customer.CalculateLikeness(index) < 0 && currentText.text == "Dislike")
        {
            Debug.Log("d");
        }

        else if (customer.CalculateLikeness(index) > 1 && currentText.text == "Really Like")
        {
            Debug.Log("rl");
        }

        else if (customer.CalculateLikeness(index) < -1 && currentText.text == "Really Dislike")
        {
            Debug.Log("rd");
        }
        else
            Debug.Log("wrong");


    }

    private void FrustrationAnswerChecker(TMP_Text currentText)
    {
        customerData = GameManager.Instance.personCustomerData;
        for(int i =0; i < customerData.frustration.Count; i++)
        {
            if (currentText.text == customerData.frustration[i])
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

    private void GoalsAnswerChecker(TMP_Text currentText)
    {
        customerData = GameManager.Instance.personCustomerData;
        for(int i =0; i<customer.customerData.goals.Count; i++)
        {
            if (currentText.text == customerData.goals[i])
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
