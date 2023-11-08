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
    [SerializeField] private TMP_Text nameText;
    
    
    public enum UserPersonaCategory
    {
        FavouriteCake,
        Goals,
        Frustration,
        Taste
    }

    private void Update()
    {
        if (GameManager.Instance.peopleMet.Count != 0 && customerIndex > -1 && customerIndex < GameManager.Instance.peopleMet.Count)
        {
            People people = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
            nameText.text = people.customerData.peopleName;
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
        nameText.gameObject.SetActive(false);
        prevNextButtons.SetActive(false);
        board.topics.SetActive(true);
    }

    public void ChooseGoals()
    {
        checker.userPersonaUI = this;
        checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
        userPersonaQuestion.SetActive(true);
        userPersonaQuestion.GetComponentInChildren<TMP_Text>().text = "Apa goal " + GameManager.Instance.peopleMet[customerIndex].GetComponentInChildren<People>().customerData.peopleName + " ?";
        userPersonaChecker = UserPersonaCategory.Goals;
        board.choices.SetActive(true);
        board.AddGoalsChoices(customerIndex, this);
        board.topics.SetActive(false);
        DragAndDropObjectData[] dragAndDropObjects = board.choices.GetComponentsInChildren<DragAndDropObjectData>();
        foreach (DragAndDropObjectData d in dragAndDropObjects)
        {
            if (d.gameObject.activeInHierarchy)
                d.Return();
        }
    }

    public void ChooseFrustration()
    {

        checker.userPersonaUI = this;
        checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
        userPersonaQuestion.SetActive(true);
        userPersonaChecker = UserPersonaCategory.Frustration;
        userPersonaQuestion.GetComponentInChildren<TMP_Text>().text = "Apa frustration " + GameManager.Instance.peopleMet[customerIndex].GetComponentInChildren<People>().customerData.peopleName + " ?";
        board.choices.SetActive(true);
        board.AddFrustrationChoices(customerIndex, this);
        board.topics.SetActive(false);
        DragAndDropObjectData[] dragAndDropObjects = board.choices.GetComponentsInChildren<DragAndDropObjectData>();
        foreach (DragAndDropObjectData d in dragAndDropObjects)
        {
            if (d.gameObject.activeInHierarchy)
                d.Return();
        }
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
            userPersonaQuestion.GetComponentInChildren<TMP_Text>().text = "Apa rasa yang paling disukai oleh " + GameManager.Instance.peopleMet[customerIndex].GetComponentInChildren<People>().customerData.peopleName + " ?";
        else
            userPersonaQuestion.GetComponentInChildren<TMP_Text>().text = "Apa rasa yang paling tidak dusukai oleh " + GameManager.Instance.peopleMet[customerIndex].GetComponentInChildren<People>().customerData.peopleName + " ?";
        if (board.choices.activeInHierarchy)
            board.choices.SetActive(false);
        board.topics.SetActive(false);
        tasteAnswers.gameObject.SetActive(true);
        DragAndDropObjectData[] dragAndDropObjects = board.tasteAnswer.GetComponentsInChildren<DragAndDropObjectData>();
        foreach (DragAndDropObjectData d in dragAndDropObjects)
        {
            if (d.gameObject.activeInHierarchy)
                d.Return();
        }
    }

    public void ChooseFavouriteCake()
    {
        checker.userPersonaUI = this;
        transform.GetChild(0).gameObject.SetActive(false);
        userPersonaChecker = UserPersonaCategory.FavouriteCake;
        userPersonaQuestion.SetActive(true);
        userPersonaQuestion.GetComponentInChildren<TMP_Text>().text = "Apa kue favorit dari " + GameManager.Instance.peopleMet[customerIndex].GetComponentInChildren<People>().customerData.peopleName + " ?";
        checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
        board.AddFavouriteCakeChoices(customerIndex, this);
        board.choices.SetActive(true);
        board.topics.SetActive(false);
        DragAndDropObjectData[] dragAndDropObjects = board.choices.GetComponentsInChildren<DragAndDropObjectData>();
        Debug.Log(dragAndDropObjects.Length);
        foreach (DragAndDropObjectData d in dragAndDropObjects)
        {
           if(d.gameObject.activeInHierarchy)
                d.Return();
        }
    }
}
