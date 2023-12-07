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
    //[SerializeField] UserPersonaHistory history;
    [SerializeField] ProblemStatement statement;
    [SerializeField] BoardActivityUI board;
    public CheckType checkerType;
    public GameObject currSnapZone;
    private int problemStatementScore;
    private int vpcScore;
    private int problemStatementTrue =0;
    private int vpcTrue =0 ;
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

    private void OnEnable()
    {
        if(gameObject.GetComponent<SnapZone>().HeldItem != null)
        {
            gameObject.GetComponent<SnapZone>().HeldItem.gameObject.GetComponent<DragAndDropObjectData>().Return();
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

        //if (checkerType == CheckType.UserPersona)
        //{
        //    GameManager.Instance.hasDoneUserPersona = true;
        //    Debug.Log(GameManager.Instance.history);

        //    GameManager.Instance.history.CakePreferenceAnswer(customer.customerData.cakePreferences);
            
        //    if (userPersonaUI.userPersonaChecker == UserPersonaUI.UserPersonaCategory.Goals)
        //        GoalsAnswerChecker(currentText);
        //    else if (userPersonaUI.userPersonaChecker == UserPersonaUI.UserPersonaCategory.Frustration)
        //        FrustrationAnswerChecker(currentText);
        //    else if (userPersonaUI.userPersonaChecker == UserPersonaUI.UserPersonaCategory.Taste)
        //        TasteAnswerChecker(currentText.transform.parent.parent.gameObject, index, customer);
        //    else
        //        FavouriteCakeAnswerChecker(currentText);

        //    for(int i=0; i < board.topicButtons.Count; i++)
        //    {
        //        if (board.topicButtons[i].interactable == true)
        //            return;

        //        if(board.topicButtons[^1].interactable == false)
        //        {
        //            board.topics.SetActive(false);
        //            board.jobFinishGO.SetActive(true);
        //        }
        //    }
        //}
        if (checkerType == CheckType.ProblemStatement)
        {
            GameManager.Instance.hasDoneProblemStatement = true;
            if (gameObject.tag == "Taste")
            {
              problemStatementTrue+= problemStatementUI.Statement1(currentGrabbable.gameObject);
            }
            else if (currentGrabbable.gameObject.CompareTag("JenisMakanan"))
            {
                JenisMakananChecker(currentText);
            }else if (currentGrabbable.gameObject.CompareTag("JenisKue"))
            {
                for (int i = 0; i < board.answerList.Count; i++)
                {
                    board.answerList[i].Return();
                }
                problemStatementScore = Mathf.RoundToInt(problemStatementTrue / 3);
                Debug.Log(problemStatementTrue);
                GameManager.Instance.problemStatementScore = problemStatementScore*100;
                board.answerList.Clear();
                currSnapZone.GetComponent<GrabbablesInTrigger>().ValidGrabbables.Clear();
                currSnapZone.GetComponent<GrabbablesInTrigger>().ClosestGrabbable = null;
                currSnapZone.GetComponent<GrabbablesInTrigger>().NearbyGrabbables.Clear();
                currSnapZone = null;
                currentText = null;
                board.problemStatement.SetActive(false);
                board.choicesTargetUsia.SetActive(false);
                board.jobFinishGO.SetActive(true);
            }

            if (board.answerList.Count == 3)
            {
                for (int i = 0; i < board.answerList.Count; i++)
                {
                    board.answerList[i].Return();
                }
                problemStatementScore = Mathf.RoundToInt(problemStatementTrue / 3);
                Debug.Log(problemStatementTrue);
                GameManager.Instance.problemStatementScore = problemStatementScore;
                board.answerList.Clear();
                currSnapZone.GetComponent<GrabbablesInTrigger>().ValidGrabbables.Clear();
                currSnapZone.GetComponent<GrabbablesInTrigger>().ClosestGrabbable = null;
                currSnapZone.GetComponent<GrabbablesInTrigger>().NearbyGrabbables.Clear();
                currSnapZone = null;
                currentText = null;
                board.problemStatement.SetActive(false);
                board.choicesTargetUsia.SetActive(false);
                board.jobFinishGO.SetActive(true);
            }
        }
        else if(checkerType == CheckType.VPC)
        {

            if (gameObject.GetComponent<SnapZone>().HeldItem != null)
            {
                currentGrabbable = gameObject.GetComponent<SnapZone>().HeldItem;

                currentGrabbable.transform.parent = gameObject.GetComponent<SnapZone>().transform;
                currentText = currentGrabbable.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
                if (board.answerList.Find(x => x.name == currentGrabbable.gameObject.name))
                {

                    Debug.Log("found");
                    //return;
                }

                else
                    board.answerList.Add(currentGrabbable.gameObject.GetComponent<DragAndDropObjectData>());
            }

            VpcCheck();
        }

    }

    #region UserPersona
    private void FavouriteCakeAnswerChecker(TMP_Text currText)
    {
        if (customer.customerData.kueFavorit.cakeName == currText.text)
        {
            Debug.Log("fav cake true");

        }
        else
        {
            Debug.Log("false");
        }
        Debug.Log(customer.customerData);
        GameManager.Instance.history.FindCustomer(customer.customerData);
        GameManager.Instance.history.FavoritCakeAnswer(customer.customerData);
        if (gameObject.GetComponent<SnapZone>() != null)
        {
            currentGrabbable.GetComponent<DragAndDropObjectData>().Return(gameObject.GetComponent<SnapZone>());
            Debug.Log("fav cake");
        }
        board.answerList.Clear();
        currSnapZone.GetComponent<GrabbablesInTrigger>().ValidGrabbables.Clear();
        currSnapZone.GetComponent<GrabbablesInTrigger>().ClosestGrabbable = null;
        currSnapZone.GetComponent<GrabbablesInTrigger>().NearbyGrabbables.Clear();
        currSnapZone = null;
        currentText = null;
        board.userPersonaQuestion.gameObject.SetActive(false);
        board.choices.SetActive(false);
        board.topics.SetActive(true);
    }

    //private void TasteAnswerChecker(GameObject currText, int random, People customer)
    //{

    //    GameManager.Instance.history.FindCustomer(customer.customerData);
    //    GameManager.Instance.history.AddToDict(customer.customerData.peopleName, customer.customerData);
    //    //return;
    //    if (customer.customerData.CalculateLikeness(Int32.Parse(currText.tag)) == 1 || customer.customerData.CalculateLikeness(Int32.Parse(currText.tag)) > 1 && random == 0)
    //    {
    //        //GameManager.Instance.history.tasteToggleList[Int32.Parse(currentText.tag)].isOn = true;
    //        if(GameManager.Instance.history.likeTasteToggleList[Int32.Parse(currText.tag)].isOn)
    //            Debug.Log(" taste true");
    //    }
    //    else if (customer.customerData.CalculateLikeness(Int32.Parse(currText.tag)) == -1 || customer.customerData.CalculateLikeness(Int32.Parse(currText.tag)) < -1 && random == 1)
    //    {
    //        //GameManager.Instance.history.tasteToggleList[Int32.Parse(currentText.tag)].isOn = false;
    //        if(GameManager.Instance.history.dislikeTasteToggleList[Int32.Parse(currText.tag)].isOn)
    //            Debug.Log("true");
    //    }

    //    if (gameObject.GetComponent<SnapZone>() != null)
    //    {
    //        currentGrabbable.GetComponent<DragAndDropObjectData>().Return(gameObject.GetComponent<SnapZone>());
    //        Debug.Log("taste");
    //    }
    //    board.answerList.Clear();
    //    currSnapZone.GetComponent<GrabbablesInTrigger>().ValidGrabbables.Clear();
    //    currSnapZone.GetComponent<GrabbablesInTrigger>().ClosestGrabbable = null;
    //    currSnapZone.GetComponent<GrabbablesInTrigger>().NearbyGrabbables.Clear();
    //    currSnapZone = null;
    //    currentText = null;
    //    board.userPersonaQuestion.gameObject.SetActive(false);
    //    board.choices.SetActive(false);
    //    board.tasteAnswer.SetActive(false);
    //    board.topics.SetActive(true);
    //}

    private void FrustrationAnswerChecker(TMP_Text currText)
    {

        customerData = GameManager.Instance.personCustomerData;
        GameManager.Instance.history.FindCustomer(customer.customerData);
        for (int i = 0; i < customer.customerData.frustration.Count; i++)
        {
            if (currText.text == customer.customerData.frustration[i])
            {
                Debug.Log("Frustration True");
                break;
            }
            else
            {
                Debug.Log("False");
            }
        }

        GameManager.Instance.history.FrustrationAnswer(currText);
        if (gameObject.GetComponent<SnapZone>() != null)
        {
            currentGrabbable.GetComponent<DragAndDropObjectData>().Return(gameObject.GetComponent<SnapZone>());
            Debug.Log("frustration");
        }
        board.answerList.Clear();
        currSnapZone.GetComponent<GrabbablesInTrigger>().ValidGrabbables.Clear();
        currSnapZone.GetComponent<GrabbablesInTrigger>().ClosestGrabbable = null;
        currSnapZone.GetComponent<GrabbablesInTrigger>().NearbyGrabbables.Clear();
        currSnapZone = null;
        currentText = null;
        board.userPersonaQuestion.gameObject.SetActive(false);
        board.choices.SetActive(false);
        board.topics.SetActive(true);
    }

    private void GoalsAnswerChecker(TMP_Text currText)
    {
        customerData = GameManager.Instance.personCustomerData;
        GameManager.Instance.history.FindCustomer(customer.customerData);
        for (int i = 0; i < customer.customerData.goals.Count; i++)
        {
            if (currText.text == customer.customerData.goals[i])
            {
                Debug.Log("Goals True");
                break;
            }
            else
            {
                Debug.Log("False");
            }
        }
        GameManager.Instance.history.GoalsAnswer(currText);


        if (gameObject.GetComponent<SnapZone>() != null)
        {
            currentGrabbable.GetComponent<DragAndDropObjectData>().Return(gameObject.GetComponent<SnapZone>());
            Debug.Log("goal");
        }
        currSnapZone.GetComponent<GrabbablesInTrigger>().ValidGrabbables.Clear();
        currSnapZone.GetComponent<GrabbablesInTrigger>().ClosestGrabbable = null;
        currSnapZone.GetComponent<GrabbablesInTrigger>().NearbyGrabbables.Clear();
        board.answerList.Clear();
        currSnapZone = null;
        currentText = null;
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
        currSnapZone.GetComponent<GrabbablesInTrigger>().ValidGrabbables.Clear();
        currSnapZone.GetComponent<GrabbablesInTrigger>().ClosestGrabbable = null;
        currSnapZone.GetComponent<GrabbablesInTrigger>().NearbyGrabbables.Clear();
        currSnapZone = null;
        currentText = null;
        StartCoroutine(DelayDeactivate("TargetUsia"));
        board.choicesJenisMakanan.SetActive(true);
    }

    public void JenisMakananChecker(TMP_Text answer)
    {
        if (answer.text == "Kue")
        {
            Debug.Log(" jenis makanan true");
            problemStatementTrue++;
        }     
        else
            Debug.Log("false");
        //currSnapZone.GetComponent<GrabbablesInTrigger>().ValidGrabbables.Clear();
        //currSnapZone.GetComponent<GrabbablesInTrigger>().ClosestGrabbable = null;
        //currSnapZone.GetComponent<GrabbablesInTrigger>().NearbyGrabbables.Clear();
        //currSnapZone = null;
        //currentText = null;
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
        //currSnapZone.GetComponent<GrabbablesInTrigger>().ValidGrabbables.Clear();
        //currSnapZone.GetComponent<GrabbablesInTrigger>().ClosestGrabbable = null;
        //currSnapZone.GetComponent<GrabbablesInTrigger>().NearbyGrabbables.Clear();
        //currSnapZone = null;
        //currentText = null;
        StartCoroutine(DelayDeactivate("Ukuran"));
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

        //for (int i = 0; i < board.answerList.Count; i++)
        //{
        //    board.answerList[i].Return();
        //}

        board.answerList.Clear();
        currSnapZone.GetComponent<GrabbablesInTrigger>().ValidGrabbables.Clear();
        currSnapZone.GetComponent<GrabbablesInTrigger>().ClosestGrabbable = null;
        currSnapZone.GetComponent<GrabbablesInTrigger>().NearbyGrabbables.Clear();
        currSnapZone = null;
        currentText = null;
        board.problemStatement.SetActive(false);
        board.choicesJenisKue.SetActive(false);
        board.jobFinishGO.SetActive(true);
    }
    #endregion
    #region VPC Checks
    private void VpcCheck()
    {
        if (currentGrabbable.gameObject.tag == currSnapZone.tag)
            vpcTrue++;
        else
            Debug.Log("VPC false");

        if (board.answerList.Count == 6)
        {
            vpcScore = Mathf.RoundToInt(vpcTrue / 6);
            GameManager.Instance.vpcScore = vpcScore*100;
            for (int i = 0; i < board.answerList.Count; i++)
            {
                board.answerList[i].Return();
            }

            board.answerList.Clear();
            currSnapZone.GetComponent<GrabbablesInTrigger>().ValidGrabbables.Clear();
            currSnapZone.GetComponent<GrabbablesInTrigger>().ClosestGrabbable = null;
            currSnapZone.GetComponent<GrabbablesInTrigger>().NearbyGrabbables.Clear();
            currSnapZone = null;
            currentText = null;
            board.vpcCanvas.SetActive(false);
            board.vpcChoices.SetActive(false);
            board.jobFinishGO.SetActive(true);
            GameManager.Instance.hasDoneVPC = true;
        }

    }
    #endregion

    private IEnumerator DelayDeactivate(string problemStatementQuestion)
    {
        yield return new WaitForSeconds(.1f);
        switch (problemStatementQuestion)
        {
            case "TargetUsia":
                board.choicesTargetUsia.SetActive(false);
                break;
            case "JenisMakanan":
                board.choicesJenisMakanan.SetActive(false);
                break;
            case "JenisKue":
                board.choicesJenisKue.SetActive(false);
                break;
            default:
                break;
        }
        
    }
}
