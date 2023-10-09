using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserPersonaUI : MonoBehaviour
{
    [SerializeField] private int customerIndex = 0;
    [SerializeField] private DragAndDropAnswerChecker checker;
    [SerializeField] GameObject tasteAnswers;
    [SerializeField] GameObject userPersonaQuestion;
    [SerializeField] BoardActivityUI board;
    public List<TMP_Text> choicesGameObjectText = new List<TMP_Text>();
    public UserPersonaCategory userPersonaChecker;
    [SerializeField] private GameObject prevNextButtons;
    
    
    public enum UserPersonaCategory
    {
        FavouriteCake,
        Goals,
        Frustration,
        Taste
    }

    private void Update()
    {
        if (GameManager.Instance.peopleMet.Count != 0 && transform.GetComponentInChildren<Text>() != null && customerIndex > -1 && customerIndex < GameManager.Instance.peopleMet.Count)
        {
            Text uiText = transform.GetComponentInChildren<Text>();
            People people = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
            uiText.text = people.customerData.peopleName;
        }
    }

    public void Prev()
    {
        if (customerIndex != 0)
            customerIndex--;
        else
            return;
    }

    public void Next()
    {
        if (customerIndex > GameManager.Instance.peopleMet.Count)
            return;
        else
            customerIndex++;
    }

    public void Confirm()
    {
        prevNextButtons.SetActive(false);
        board.topics.SetActive(true);
    }

    public void ChooseGoals()
    {
        checker.userPersonaUI = this;
        checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
        userPersonaQuestion.SetActive(true);
        userPersonaQuestion.GetComponentInChildren<Text>().text = "Apa goal " + GameManager.Instance.peopleMet[customerIndex].GetComponentInChildren<People>().customerData.peopleName + " ?";
        userPersonaChecker = UserPersonaCategory.Goals;
        board.choices.SetActive(true);
        board.AddGoalsChoices(customerIndex, this);
        board.topics.SetActive(false);
    }

    public void ChooseFrustration()
    {
        checker.userPersonaUI = this;
        checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
        userPersonaQuestion.SetActive(true);
        userPersonaChecker = UserPersonaCategory.Frustration;
        userPersonaQuestion.GetComponentInChildren<Text>().text = "Apa frustration " + GameManager.Instance.peopleMet[customerIndex].GetComponentInChildren<People>().customerData.peopleName + " ?";
        board.choices.SetActive(true);
        board.AddFrustrationChoices(customerIndex, this);
        board.topics.SetActive(false);
    }

    public void ChooseTaste()
    {
        checker.userPersonaUI = this;
        userPersonaChecker = UserPersonaCategory.Taste;
        checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
        int randomIndex = Random.Range(0, 2);
        checker.index = randomIndex;
        userPersonaQuestion.SetActive(true);
        if(randomIndex == 0)
            userPersonaQuestion.GetComponentInChildren<Text>().text = "Apa rasa yang paling disukai oleh " + GameManager.Instance.peopleMet[customerIndex].GetComponentInChildren<People>().customerData.peopleName + " ?";
        else
            userPersonaQuestion.GetComponentInChildren<Text>().text = "Apa rasa yang paling tidak dusukai oleh " + GameManager.Instance.peopleMet[customerIndex].GetComponentInChildren<People>().customerData.peopleName + " ?";
        if (board.choices.activeInHierarchy)
            board.choices.SetActive(false);
        board.topics.SetActive(false);
        tasteAnswers.gameObject.SetActive(true);
    }

    //public void ChooseManis()
    //{
    //    userPersonaChecker = UserPersonaCategory.Taste;
    //    checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
    //    checker.index = 0;
    //    board.AddTasteChoices(customerIndex, this);
    //}    
    //public void ChooseAsin()
    //{
    //    userPersonaChecker = UserPersonaCategory.Taste;
    //    checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
    //    checker.index = 1;
    //    board.AddTasteChoices(customerIndex, this);
    //}    
    //public void ChooseAsem()
    //{
    //    userPersonaChecker = UserPersonaCategory.Taste;
    //    checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
    //    checker.index = 2;
    //    board.AddTasteChoices(customerIndex, this);
    //}    
    //public void ChoosePahit()
    //{
    //    userPersonaChecker = UserPersonaCategory.Taste;
    //    checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
    //    checker.index = 3;
    //    board.AddTasteChoices(customerIndex, this);
    //}    
    //public void ChooseSusu()
    //{
    //    userPersonaChecker = UserPersonaCategory.Taste;
    //    checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
    //    checker.index = 4;
    //    board.AddTasteChoices(customerIndex, this);
    //}    
    //public void ChooseCoklat()
    //{
    //    userPersonaChecker = UserPersonaCategory.Taste;
    //    checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
    //    checker.index = 5;
    //    board.AddTasteChoices(customerIndex, this);
    //}    
    //public void ChooseVanila()
    //{
    //    userPersonaChecker = UserPersonaCategory.Taste;
    //    checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
    //    checker.index = 6;
    //    board.AddTasteChoices(customerIndex, this);
    //}

    public void ChooseFavouriteCake()
    {
        checker.userPersonaUI = this;
        userPersonaChecker = UserPersonaCategory.FavouriteCake;
        userPersonaQuestion.GetComponentInChildren<Text>().text = "Apa kue favorit dari " + GameManager.Instance.peopleMet[customerIndex].GetComponentInChildren<People>().customerData.peopleName + " ?";
        checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
        board.AddFavouriteCakeChoices(customerIndex, this);
        board.choices.SetActive(true);
        board.topics.SetActive(false);
    }
}
