using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserPersonaUI : MonoBehaviour
{
    [SerializeField] private int customerIndex = 0;
    [SerializeField] private DragAndDropAnswerChecker checker;
    public List<TMP_Text> choicesGameObjectText = new List<TMP_Text>();
    public UserPersonaCategory userPersonaChecker;
    public Canvas tasteUICanvas;
    
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

    public void ChooseGoals()
    {
        userPersonaChecker = UserPersonaCategory.Goals;
        checker.userPersonaUI = this;
        GameManager.Instance.AddGoalsChoices(customerIndex, this);
    }

    public void ChooseFrustration()
    {
        userPersonaChecker = UserPersonaCategory.Frustration;
        checker.userPersonaUI = this;
        GameManager.Instance.AddFrustrationChoices(customerIndex, this);
    }

    public void ChooseTaste()
    {
        tasteUICanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ChooseManis()
    {
        userPersonaChecker = UserPersonaCategory.Taste;
        checker.userPersonaUI = this;
        checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
        checker.index = 0;
        GameManager.Instance.AddTasteChoices(customerIndex, this);
    }    
    public void ChooseAsin()
    {
        userPersonaChecker = UserPersonaCategory.Taste;
        checker.userPersonaUI = this;
        checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
        checker.index = 1;
        GameManager.Instance.AddTasteChoices(customerIndex, this);
    }    
    public void ChooseAsem()
    {
        userPersonaChecker = UserPersonaCategory.Taste;
        checker.userPersonaUI = this;
        checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
        checker.index = 2;
        GameManager.Instance.AddTasteChoices(customerIndex, this);
    }    
    public void ChoosePahit()
    {
        userPersonaChecker = UserPersonaCategory.Taste;
        checker.userPersonaUI = this;
        checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
        checker.index = 3;
        GameManager.Instance.AddTasteChoices(customerIndex, this);
    }    
    public void ChooseSusu()
    {
        userPersonaChecker = UserPersonaCategory.Taste;
        checker.userPersonaUI = this;
        checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
        checker.index = 4;
        GameManager.Instance.AddTasteChoices(customerIndex, this);
    }    
    public void ChooseCoklat()
    {
        userPersonaChecker = UserPersonaCategory.Taste;
        checker.userPersonaUI = this;
        checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
        checker.index = 5;
        GameManager.Instance.AddTasteChoices(customerIndex, this);
    }    
    public void ChooseVanila()
    {
        userPersonaChecker = UserPersonaCategory.Taste;
        checker.userPersonaUI = this;
        checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
        checker.index = 6;
        GameManager.Instance.AddTasteChoices(customerIndex, this);
    }

    public void ChooseFavouriteCake()
    {
        userPersonaChecker = UserPersonaCategory.FavouriteCake;
        checker.userPersonaUI = this;
        checker.customer = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
        GameManager.Instance.AddFavouriteCakeChoices(customerIndex, this);
    }
}
