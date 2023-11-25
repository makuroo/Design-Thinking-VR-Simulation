using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using BNG;

public class BoardActivityUI : MonoBehaviour
{
    public GameObject userPersonaUI;
    public GameObject problemStatement;
    public GameObject userPersonaQuestion;
    public GameObject vpcCanvas;
    public GameObject choices;
    public GameObject choicesTargetUsia;
    public GameObject choicesUkuran;
    public GameObject choicesJenisMakanan;
    public GameObject choicesJenisKue;
    public GameObject tasteAnswer;
    public GameObject vpcChoices;
    public GameObject boardActivityUI;
    public GameObject jobFinishGO;

    [SerializeField] private CustomerDataSO personCustomerData;
    [SerializeField] private List<string> tempAnswer;
    [SerializeField] private List<string> tempListRandom;
    [SerializeField] private List<string> randomGoalsOptions;
    [SerializeField] private List<string> randomFrustrationsOptions;
    [SerializeField] private List<string> randomgFavouriteCakeOptions;
    [SerializeField] private List<string> usiaTarget;
    [SerializeField] private List<string> jenisMakanan;

    public List<UnityEngine.UI.Button> topicButtons;
    public List<UnityEngine.UI.Button> boardActivityButtons;

    public List<DragAndDropObjectData> answerList;

    public GameObject topics;

    public Grabber[] handGrabber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        if (GameManager.Instance.canDoActivity && jobFinishGO.activeInHierarchy)
        {
            jobFinishGO.SetActive(false);
            boardActivityUI.SetActive(true);
        }

        if (GameManager.Instance.interviewCount>=3)
            boardActivityButtons[0].interactable = true;

        if (GameManager.Instance.hasDoneUserPersona)
             boardActivityButtons[1].interactable = true;

        if (GameManager.Instance.hasDoneProblemStatement)
            boardActivityButtons[2].interactable = true;
    }

    public void UserPersona()
    {
        GameManager.Instance.canDoActivity = false;
        userPersonaUI.SetActive(true);
        boardActivityUI.SetActive(false);
        foreach(UnityEngine.UI.Button b in topicButtons)
        {
            b.interactable = true;
        }
    }

    public void ProblemStatement()
    {
        GameManager.Instance.canDoActivity = false;
        problemStatement.SetActive(true);
        choicesTargetUsia.SetActive(true);
        boardActivityUI.SetActive(false);
    }    

    public void VPC()
    {
        GameManager.Instance.canDoActivity = false;
        vpcCanvas.SetActive(true);
        vpcChoices.SetActive(true);
        boardActivityUI.SetActive(false);
    }

    #region Choices
    public void AddGoalsChoices(int index, UserPersonaUI userPersonaUI)
    {
        Debug.Log(index);
        personCustomerData = GameManager.Instance.peopleMet[index].GetComponentInChildren<People>().customerData;
        tempAnswer = new List<string>(personCustomerData.goals);
        tempListRandom = new List<string>(randomGoalsOptions);
        for (int i = 0; i < 5; i++)
        {
            int randomOrNot = UnityEngine.Random.Range(0, 2);
            int answerIndex = UnityEngine.Random.Range(0, tempAnswer.Count);
            Debug.Log(randomOrNot);
            //int randomAnswerIndex = Random.Range(0,5);
            if (randomOrNot == 0 && tempAnswer.Count != 0)
            {
                userPersonaUI.choicesGameObjectText[i].text = tempAnswer[answerIndex];
                tempAnswer.RemoveAt(answerIndex);
            }
            else
            {
                int randomIndex =UnityEngine.Random.Range(0, tempListRandom.Count);
                if (tempListRandom[randomIndex] == personCustomerData.goals[answerIndex])
                {
                    if (randomIndex != tempListRandom.Count - 1)
                        randomIndex++;
                    else
                        randomIndex--;
                }
                userPersonaUI.choicesGameObjectText[i].text = tempListRandom[randomIndex];
                tempListRandom.RemoveAt(randomIndex);
            }
        }

    }

    public void AddFrustrationChoices(int index, UserPersonaUI userPersonaUI)
    {
        Debug.Log(index);
        personCustomerData = GameManager.Instance.peopleMet[index].GetComponentInChildren<People>().customerData;
        tempAnswer = new List<string>(personCustomerData.frustration);
        tempListRandom = new List<string>(randomFrustrationsOptions);
        tempListRandom = randomFrustrationsOptions;
        for (int i = 0; i < 5; i++)
        {
            int randomOrNot = UnityEngine.Random.Range(0, 2);
            int answerIndex = UnityEngine.Random.Range(0, tempAnswer.Count);
            Debug.Log(tempAnswer.Count);
            if (randomOrNot == 0 && tempAnswer.Count>0)
            {
                userPersonaUI.choicesGameObjectText[i].text = personCustomerData.frustration[answerIndex];
                tempAnswer.RemoveAt(answerIndex);
            }
            else
            {
                int randomIndex = UnityEngine.Random.Range(0, tempListRandom.Count);
                if(tempListRandom[randomIndex] == personCustomerData.frustration[answerIndex])
                {
                    if (randomIndex != tempListRandom.Count - 1)
                        randomIndex++;
                    else
                        randomIndex--;
                }
                userPersonaUI.choicesGameObjectText[i].text = tempListRandom[randomIndex];
                tempListRandom.RemoveAt(randomIndex);
            }
        }

    }

    public void AddTargetUsiaChoices(UserPersonaUI userPersonaUI)
    {
        tempListRandom = new List<string>(usiaTarget);
        int answerIndex =UnityEngine.Random.Range(0, 5);
        for (int i = 0; i < 5; i++)
        {
            if (i == answerIndex)
            {
                userPersonaUI.choicesGameObjectText[i].text = "Dewasa";
            }
            else
            {
                int randomIndex =UnityEngine.Random.Range(0, usiaTarget.Count);
                userPersonaUI.choicesGameObjectText[i].text = tempListRandom[randomIndex];
                tempListRandom.RemoveAt(randomIndex);
            }
        }
    }

    public void AddJenisMakananChoices(UserPersonaUI userPersonaUI)
    {
        tempListRandom = new List<string>(jenisMakanan);
        int answerIndex =UnityEngine.Random.Range(0, 5);
        for (int i = 0; i < 5; i++)
        {
            if (i == answerIndex)
            {
                userPersonaUI.choicesGameObjectText[i].text = "Kue";
            }
            else
            {
                int randomIndex =UnityEngine.Random.Range(0, usiaTarget.Count);
                userPersonaUI.choicesGameObjectText[i].text = tempListRandom[randomIndex];
                tempListRandom.RemoveAt(randomIndex);
            }
        }
    }

    public void AddUkuranKueChoices(int index, UserPersonaUI userPersonaUI)
    {
        Debug.Log(index);
        personCustomerData = GameManager.Instance.peopleMet[index].GetComponentInChildren<People>().customerData;
        CakeSO tempCake = personCustomerData.kueFavorit;
        
        for (int i = 0; i < 5; i++)
        {
            switch (i)
            {
                case 0:
                    userPersonaUI.choicesGameObjectText[i].text = Ukuran.SangatKecil.ToString();
                    break;
                case 1:
                    userPersonaUI.choicesGameObjectText[i].text = Ukuran.Kecil.ToString();
                    break;
                case 2:
                    userPersonaUI.choicesGameObjectText[i].text = Ukuran.Sedang.ToString();
                    break;
                case 3:
                    userPersonaUI.choicesGameObjectText[i].text = Ukuran.Besar.ToString();
                    break;
                case 4:
                    userPersonaUI.choicesGameObjectText[i].text = Ukuran.SangatBesar.ToString();
                    break;
                default:
                    break;
            }
            
        }

    }

    public void AddFavouriteCakeChoices(int index, UserPersonaUI userPersonaUI)
    {
        personCustomerData = GameManager.Instance.peopleMet[index].GetComponentInChildren<People>().customerData;
        tempListRandom = new List<string>(randomgFavouriteCakeOptions);
        int answerIndex = UnityEngine.Random.Range(0, 5);
        for (int i = 0; i < 5; i++)
        {
            if (i == answerIndex)
            {
                userPersonaUI.choicesGameObjectText[i].text = personCustomerData.kueFavorit.cakeName;
                Debug.Log(personCustomerData.kueFavorit.cakeName);
            }
            else
            {
                int randomIndex =UnityEngine.Random.Range(0, tempListRandom.Count);
                if (tempListRandom[randomIndex] == personCustomerData.kueFavorit.cakeName)
                {
                    if (randomIndex != tempListRandom.Count - 1)
                        randomIndex++;
                    else
                        randomIndex--;
                }
                userPersonaUI.choicesGameObjectText[i].text = tempListRandom[randomIndex];
                tempListRandom.RemoveAt(randomIndex);
            }
        }
    }
    #endregion
}
