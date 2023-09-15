using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BNG;
using System;

public class DragAndDropAnswerChecker : MonoBehaviour
{
    public enum CheckType
    {
        UserPersona,
        ProblemStatement,
        VPC
    }
    public UserPersonaUI userPersonaUI;
    public ProblemStatement problemStatementUI;
    private CustomerDataSO customerData;
    private Grabbable currentGrabbable;
    public TMP_Text currentText;
    public int index;
    public People customer;
    [SerializeField] UserPersonaHistory history;
    [SerializeField] ProblemStatement statement;
    [SerializeField] BoardActivityUI board;
    public CheckType checkerType;

    public void ChooseChecker()
    {
        history.CakePreferenceAnswer(customer.customerData.cakePreferences);
        if (gameObject.GetComponent<SnapZone>().HeldItem != null)
        {
            currentGrabbable = gameObject.GetComponent<SnapZone>().HeldItem;
            currentText = currentGrabbable.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        }
 
        if(checkerType == CheckType.UserPersona)
        {
            if (userPersonaUI.userPersonaChecker == UserPersonaUI.UserPersonaCategory.Goals)
                GoalsAnswerChecker(currentText);
            else if (userPersonaUI.userPersonaChecker == UserPersonaUI.UserPersonaCategory.Frustration)
                FrustrationAnswerChecker(currentText);
            else if (userPersonaUI.userPersonaChecker == UserPersonaUI.UserPersonaCategory.Taste)
                TasteAnswerChecker(currentText.transform.parent.parent.gameObject, index, customer);
            else
                FavouriteCakeAnswerChecker(currentText);
        }else if(checkerType == CheckType.ProblemStatement)
        {
            problemStatementUI.Statement1(currentText.gameObject);
        }

    }

    #region UserPersona
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
        userPersonaUI.gameObject.SetActive(false);
    }

    private void TasteAnswerChecker(GameObject currentText, int random ,People customer)
    {

        history.FindCustomer(customer.customerData);
        history.AddToDict(customer.customerData.peopleName, customer.customerData);
        //return;
        if (customer.customerData.CalculateLikeness(Int32.Parse(currentText.tag)) == 1 || customer.customerData.CalculateLikeness(Int32.Parse(currentText.tag))> 1 && random == 0)
        {
           history.tasteToggleList[Int32.Parse(currentText.tag)].isOn = true;
           Debug.Log("true");
        }
        else if(customer.customerData.CalculateLikeness(Int32.Parse(currentText.tag)) == -1 || customer.customerData.CalculateLikeness(Int32.Parse(currentText.tag)) < -1 && random == 1)
        {
           history.tasteToggleList[Int32.Parse(currentText.tag)].isOn = false;
           Debug.Log("true");
        }
        userPersonaUI.gameObject.SetActive(false);
        if (gameObject.GetComponent<SnapZone>() != null)
            currentGrabbable.GetComponent<DragAndDropObjectData>().Return(gameObject.GetComponent<SnapZone>());
    }

    private void FrustrationAnswerChecker(TMP_Text currentText)
    {
        
        customerData = GameManager.Instance.personCustomerData;
        history.FindCustomer(customer.customerData);
        for (int i =0; i <customer.customerData.frustration.Count; i++)
        {
            if (currentText.text == customer.customerData.frustration[i])
            {
                Debug.Log("True");
                break;
            }
            else
            {
                Debug.Log("False");
            }
        }
        if (gameObject.GetComponent<SnapZone>() != null)
            currentGrabbable.GetComponent<DragAndDropObjectData>().Return(gameObject.GetComponent<SnapZone>());
        history.FrustrationAnswer(currentText);
        userPersonaUI.gameObject.SetActive(false);
        board.userPersonaQuestion.SetActive(false);
        board.choices.SetActive(false);
        board.boardActivityUI.SetActive(true);
    }

    private void GoalsAnswerChecker(TMP_Text currentText)
    {
        customerData = GameManager.Instance.personCustomerData;
        history.FindCustomer(customer.customerData);
        for (int i = 0; i < customer.customerData.goals.Count; i++)
        {
            if (currentText.text == customer.customerData.goals[i])
            {
                Debug.Log("True");
                break;
            }
            else
            {
                Debug.Log("False");
            }
        }
        history.GoalsAnswer(currentText);
        userPersonaUI.gameObject.SetActive(false);
        if (gameObject.GetComponent<SnapZone>() != null)
            currentGrabbable.GetComponent<DragAndDropObjectData>().Return(gameObject.GetComponent<SnapZone>());
        board.userPersonaQuestion.SetActive(false);
        board.choices.SetActive(false);
        board.boardActivityUI.SetActive(true);
    }
    #endregion
}
