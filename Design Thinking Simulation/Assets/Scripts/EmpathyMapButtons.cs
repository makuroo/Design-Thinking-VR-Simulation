using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmpathyMapButtons : MonoBehaviour
{
    [SerializeField] private int customerIndex = 0;
    [SerializeField] private DragAndDropAnswerChecker checker;
    public List<TMP_Text> choicesGameObjectText = new List<TMP_Text>();
    public EmpathyType empathyChecker;
    public enum EmpathyType
    {
        Think,
        Feels,
        Does,
        Says
    }

    private void Update()
    {
        if (GameManager.Instance.peopleMet.Count != 0 && transform.GetComponentInChildren<Text>() != null && customerIndex > -1 && customerIndex < GameManager.Instance.peopleMet.Count)
        {
            Text uiText = transform.GetComponentInChildren<Text>();
            People people = GameManager.Instance.peopleMet[customerIndex].transform.GetComponentInChildren<People>();
            uiText.text = people.peopleName;
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

    public void ChooseThink()
    {
        empathyChecker = EmpathyType.Think;
        checker.empathyMapButtons = this;
        GameManager.Instance.AddThinkChoices(customerIndex,this);
    }

    public void ChooseDoes()
    {
        empathyChecker = EmpathyType.Does;
        checker.empathyMapButtons = this;
        GameManager.Instance.AddThinkChoices(customerIndex,this);
    }

    public void ChooseFeels()
    {
        empathyChecker = EmpathyType.Feels;
        checker.empathyMapButtons = this;
        GameManager.Instance.AddThinkChoices(customerIndex,this);
    }

    public void ChooseSays()
    {
        empathyChecker = EmpathyType.Says;
        checker.empathyMapButtons = this;
        GameManager.Instance.AddThinkChoices(customerIndex,this);
    }
}
