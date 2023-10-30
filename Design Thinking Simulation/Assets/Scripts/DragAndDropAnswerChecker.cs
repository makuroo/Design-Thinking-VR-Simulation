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
    public GameObject currSnapZone;


    private void Update()
    {
        if (board.handGrabber[0]!=null && board.handGrabber[1]!=null )
        {
            foreach (Grabber g in board.handGrabber)
            {
                if (g.HeldGrabbable == null)
                    g.ForceRelease = false;
            }
        }

    }

    public void ChooseChecker()
    {
        
        if (gameObject.GetComponent<SnapZone>() != null){
            currSnapZone = gameObject;
            foreach (Grabber g in board.handGrabber)
            {
                g.ForceRelease = true;
            }
        }

        if (gameObject.GetComponent<SnapZone>().HeldItem != null)
        {
            currentGrabbable = gameObject.GetComponent<SnapZone>().HeldItem;
            
            currentGrabbable.transform.parent = gameObject.GetComponent<SnapZone>().transform;
            currentText = currentGrabbable.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
            if (board.answerList.Find(x => x.name == currentGrabbable.gameObject.name))
            {
                
                Debug.Log("found");
                return;
            }
                
            else
                board.answerList.Add(currentGrabbable.gameObject.GetComponent<DragAndDropObjectData>());
        }

        if (checkerType == CheckType.UserPersona)
        {
            history.CakePreferenceAnswer(customer.customerData.cakePreferences);
            if (userPersonaUI.userPersonaChecker == UserPersonaUI.UserPersonaCategory.Goals)
                GoalsAnswerChecker(currentText);
            else if (userPersonaUI.userPersonaChecker == UserPersonaUI.UserPersonaCategory.Frustration)
                FrustrationAnswerChecker(currentText);
            else if (userPersonaUI.userPersonaChecker == UserPersonaUI.UserPersonaCategory.Taste)
                TasteAnswerChecker(currentText.transform.parent.parent.gameObject, index, customer);
            else
                FavouriteCakeAnswerChecker(currentText);

            for(int i=0; i < board.topicButtons.Count; i++)
            {
                if (board.topicButtons[i].interactable == true)
                    return;

                if(board.topicButtons[^1].interactable == false)
                {
                    board.topics.SetActive(false);
                    board.jobFinishGO.SetActive(true);
                }
            }
        }
        else if (checkerType == CheckType.ProblemStatement)
        {
            Debug.Log(currentGrabbable.gameObject);
            if (currentGrabbable.gameObject.CompareTag("TargetUsia"))
            {
                TargetUsiaChecker(currentText);
            }
            else if (currentGrabbable.gameObject.CompareTag("Ukuran"))
            {
                UkuranChecker(currentText);
            }
            else if (currentGrabbable.gameObject.CompareTag("JenisMakanan"))
            {
                JenisMakananChecker(currentText);
            }else if (currentGrabbable.gameObject.CompareTag("JenisKue"))
            {
                CakeAnswerChecker(currentText);
            }
            else if(currentGrabbable.gameObject.layer== 14)
            {
                if (currentGrabbable == null)
                    Debug.Log(null);
                problemStatementUI.Statement1(currentGrabbable.gameObject);
            }
        }else if(checkerType == CheckType.VPC)
        {
            VpcCheck();
        }

    }

    #region UserPersona
    private void FavouriteCakeAnswerChecker(TMP_Text currentText)
    {
        if (customer.customerData.kueFavorit.cakeName == currentText.text)
        {
            Debug.Log("fav cake true");

        }
        else
        {
            Debug.Log("false");
        }

        history.FindCustomer(customer.customerData);
        history.FavoritCakeAnswer(customer.customerData);
        if (gameObject.GetComponent<SnapZone>() != null)
            currentGrabbable.GetComponent<DragAndDropObjectData>().Return(gameObject.GetComponent<SnapZone>());

        board.userPersonaQuestion.gameObject.SetActive(false);
        board.choices.SetActive(false);
        board.topics.SetActive(true);
    }

    private void TasteAnswerChecker(GameObject currentText, int random, People customer)
    {

        history.FindCustomer(customer.customerData);
        history.AddToDict(customer.customerData.peopleName, customer.customerData);
        //return;
        if (customer.customerData.CalculateLikeness(Int32.Parse(currentText.tag)) == 1 || customer.customerData.CalculateLikeness(Int32.Parse(currentText.tag)) > 1 && random == 0)
        {
            history.tasteToggleList[Int32.Parse(currentText.tag)].isOn = true;
            Debug.Log(" taste true");
        }
        else if (customer.customerData.CalculateLikeness(Int32.Parse(currentText.tag)) == -1 || customer.customerData.CalculateLikeness(Int32.Parse(currentText.tag)) < -1 && random == 1)
        {
            history.tasteToggleList[Int32.Parse(currentText.tag)].isOn = false;
            Debug.Log("true");
        }

        if (gameObject.GetComponent<SnapZone>() != null)
            currentGrabbable.GetComponent<DragAndDropObjectData>().Return(gameObject.GetComponent<SnapZone>());
        board.userPersonaQuestion.gameObject.SetActive(false);
        board.choices.SetActive(false);
        board.tasteAnswer.SetActive(false);
        board.topics.SetActive(true);
    }

    private void FrustrationAnswerChecker(TMP_Text currentText)
    {

        customerData = GameManager.Instance.personCustomerData;
        history.FindCustomer(customer.customerData);
        for (int i = 0; i < customer.customerData.frustration.Count; i++)
        {
            if (currentText.text == customer.customerData.frustration[i])
            {
                Debug.Log("Frustration True");
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
        board.userPersonaQuestion.gameObject.SetActive(false);
        board.choices.SetActive(false);
        board.topics.SetActive(true);
    }

    private void GoalsAnswerChecker(TMP_Text currentText)
    {
        customerData = GameManager.Instance.personCustomerData;
        history.FindCustomer(customer.customerData);
        for (int i = 0; i < customer.customerData.goals.Count; i++)
        {
            if (currentText.text == customer.customerData.goals[i])
            {
                Debug.Log("Goals True");
                break;
            }
            else
            {
                Debug.Log("False");
            }
        }
        history.GoalsAnswer(currentText);


        if (gameObject.GetComponent<SnapZone>() != null)
            currentGrabbable.GetComponent<DragAndDropObjectData>().Return(gameObject.GetComponent<SnapZone>());

        board.userPersonaQuestion.gameObject.SetActive(false);
        board.choices.SetActive(false);
        board.topics.SetActive(true);
    }
    #endregion
    #region ProblemStatementChecks
    public void TargetUsiaChecker(TMP_Text answer)
    {
        if (answer.text == "Dewasa")
            Debug.Log("usia true");
        else
            Debug.Log("false");
        board.choicesJenisMakanan.SetActive(true);
        board.choicesTargetUsia.SetActive(false);

    }


    public void JenisMakananChecker(TMP_Text answer)
    {
        if (answer.text == "Kue")
            Debug.Log(" jenis makanan true");
        else
            Debug.Log("false");
        board.choicesJenisMakanan.SetActive(false);
        board.choicesUkuran.SetActive(true);
    }

    public void UkuranChecker(TMP_Text answer)
    {
        if (problemStatementUI == null)
            Debug.Log("problemnull");
        Debug.Log(problemStatementUI.UkuranAvg());
        if (answer.text == "Kecil" && problemStatementUI.UkuranAvg()==0 
            || answer.text == "Sedang" && problemStatementUI.UkuranAvg() == 1 
            || answer.text == "Kecil" && problemStatementUI.UkuranAvg() == 2)
        {
            Debug.Log("ukuran true");
        }
        else
        {
            Debug.Log("false");
        }
        board.choicesUkuran.SetActive(false);
        board.tasteAnswer.SetActive(true); 
    }

    public void CakeAnswerChecker(TMP_Text ansewer)
    {
        if (ansewer.text == "Pastry")
        {
            Debug.Log("jenis kue true");
        }
        else
        {
            Debug.Log("false");
        }
        for (int i = 0; i < board.answerList.Count; i++)
        {
            board.answerList[i].Return();
        }
        board.answerList.Clear();
        board.problemStatement.SetActive(false);
        board.choicesJenisKue.SetActive(false);
        board.jobFinishGO.SetActive(true);
    }
    #endregion
    #region VPC Checks
    private void VpcCheck()
    {

        if (currentGrabbable.gameObject.tag == currSnapZone.tag)
            Debug.Log("VPC true");
        else
            Debug.Log("VPC false");

        if (board.answerList.Count == 6)
        {
            for (int i = 0; i < board.answerList.Count; i++)
            {
                board.answerList[i].Return();
            }

            board.answerList.Clear();

            board.vpcCanvas.SetActive(false);
            board.vpcChoices.SetActive(false);
            board.jobFinishGO.SetActive(true);
        }

    }
    #endregion

    public void Test()
    {
        Debug.Log("work");
    }
}
