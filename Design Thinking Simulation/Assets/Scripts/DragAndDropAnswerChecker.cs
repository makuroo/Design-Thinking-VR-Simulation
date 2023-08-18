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
    [SerializeField] UserPersonaHistory history;

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

        history.FindCustomer(customer.customerData);
        history.FavoritCakeAnswer(currentText);
        if(gameObject.GetComponent<SnapZone>() != null)
            currentGrabbable.GetComponent<DragAndDropObjectData>().Return(gameObject.GetComponent<SnapZone>());
    }

    private void TasteAnswerChecker(TMP_Text currentText, int index, People customer)
    {
        history.FindCustomer(customer.customerData);
        if (customer.CalculateLikeness(index) == 0 && currentText.text == "Neutral")
        {
            Debug.Log("n");
            history.tasteToggleList[index].isOn = false;
        }

        else if (customer.CalculateLikeness(index) == 1 && currentText.text == "Like")
        {
            Debug.Log("l");
            history.tasteToggleList[index].isOn = true;
        }

        else if (customer.CalculateLikeness(index) < 0 && currentText.text == "Dislike")
        {
            Debug.Log("d");
            history.tasteToggleList[index].isOn = false;
        }

        else if (customer.CalculateLikeness(index) > 1 && currentText.text == "Really Like")
        {
            Debug.Log("rl");
            history.tasteToggleList[index].isOn = true;
        }

        else if (customer.CalculateLikeness(index) < -1 && currentText.text == "Really Dislike")
        {
            Debug.Log("rd");
            history.tasteToggleList[index].isOn = false;
        }
        else
            Debug.Log("wrong");


    }

    private void FrustrationAnswerChecker(TMP_Text currentText)
    {
        
        customerData = GameManager.Instance.personCustomerData;
        history.FindCustomer(customerData);
        for (int i =0; i < customerData.frustration.Count; i++)
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

        history.FrustrationAnswer(currentText);

    }

    private void GoalsAnswerChecker(TMP_Text currentText)
    {
        customerData = GameManager.Instance.personCustomerData;
        history.FindCustomer(customerData);
        for (int i = 0; i < customerData.goals.Count; i++)
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

        history.FrustrationAnswer(currentText);
    }
}
