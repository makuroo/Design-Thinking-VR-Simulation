using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class BoardActivityUI : MonoBehaviour
{
    public GameObject userPersonaUI;
    public GameObject problemStatement;
    public GameObject userPersonaQuestion;
    public GameObject choices;
    [SerializeField] private GameObject tasteAnswer;
    public GameObject boardActivityUI;
    private CustomerDataSO personCustomerData;
    [SerializeField] private List<string> tempAnswer;
    [SerializeField] private List<string> tempListRandom;
    [SerializeField] private List<string> randomOptions;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UserPersona()
    {
        GameManager.Instance.canDoActivity = false;
        userPersonaUI.SetActive(true);
        choices.SetActive(true);
        boardActivityUI.SetActive(false);
    }

    public void ProblemStatement()
    {
        GameManager.Instance.canDoActivity = false;
        problemStatement.SetActive(true);
        tasteAnswer.SetActive(true);
        boardActivityUI.SetActive(false);
    }    

    #region Choices
    public void AddGoalsChoices(int index, UserPersonaUI userPersonaUI)
    {
        Debug.Log(index);
        personCustomerData = GameManager.Instance.peopleMet[index].GetComponentInChildren<People>().customerData;
        tempAnswer = new List<string>(personCustomerData.goals);
        tempListRandom = new List<string>(randomOptions);
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
                int randomIndex = UnityEngine.Random.Range(0, tempListRandom.Count);
                userPersonaUI.choicesGameObjectText[i].text = tempListRandom[randomIndex];
                tempListRandom.RemoveAt(randomIndex);
            }
        }

    }

    public void AddFrustrationChoices(int index, UserPersonaUI userPersonaUI)
    {
        Debug.Log(index);
        personCustomerData = GameManager.Instance.peopleMet[index].GetComponentInChildren<People>().customerData;
        tempAnswer = new List<string>(personCustomerData.goals);
        tempListRandom = new List<string>(randomOptions);
        tempListRandom = randomOptions;
        for (int i = 0; i < 5; i++)
        {
            int randomOrNot = UnityEngine.Random.Range(0, 2);
            int answerIndex = UnityEngine.Random.Range(0, tempAnswer.Count);
            if (randomOrNot == 0)
            {
                userPersonaUI.choicesGameObjectText[i].text = personCustomerData.frustration[answerIndex];
                tempAnswer.RemoveAt(answerIndex);
            }
            else
            {
                int randomIndex = UnityEngine.Random.Range(0, tempListRandom.Count);
                userPersonaUI.choicesGameObjectText[i].text = tempListRandom[randomIndex];
                tempListRandom.RemoveAt(randomIndex);
            }
        }

    }

    //public void AddTasteChoices(int index, UserPersonaUI userPersonaUI)
    //{
    //    personCustomerData = GameManager.Instance.peopleMet[index].GetComponentInChildren<People>().customerData;

    //    userPersonaUI.choicesGameObjectText[0].text = "Like";
    //    userPersonaUI.choicesGameObjectText[1].text = "Really Like";
    //    userPersonaUI.choicesGameObjectText[2].text = "Neutral";
    //    userPersonaUI.choicesGameObjectText[3].text = "Dislike";
    //    userPersonaUI.choicesGameObjectText[4].text = "Really Dislike";
    //}


    public void AddFavouriteCakeChoices(int index, UserPersonaUI userPersonaUI)
    {
        personCustomerData = GameManager.Instance.peopleMet[index].GetComponentInChildren<People>().customerData;
        tempListRandom = new List<string>(randomOptions);
        int answerIndex = UnityEngine.Random.Range(0, 5);
        for (int i = 0; i < 5; i++)
        {
            if (i == answerIndex)
            {
                userPersonaUI.choicesGameObjectText[i].text = personCustomerData.kueFavorit;
            }
            else
            {
                int randomIndex = UnityEngine.Random.Range(0, tempListRandom.Count);
                userPersonaUI.choicesGameObjectText[i].text = tempListRandom[randomIndex];
                tempListRandom.RemoveAt(randomIndex);
            }
        }
    }
    #endregion
}
