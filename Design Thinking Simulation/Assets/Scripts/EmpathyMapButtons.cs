using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpathyMapButtons : MonoBehaviour
{
    [SerializeField] private int customerIndex = 0;
    [SerializeField] private DragAndDropAnswerChecker checker;
    public EmpathyType empathyChecker;
    public enum EmpathyType
    {
        Think,
        Feels,
        Does,
        Says
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
        GameManager.Instance.AddThinkChoices(customerIndex);
    }

    public void ChooseDoes()
    {
        empathyChecker = EmpathyType.Does;
        checker.empathyMapButtons = this;
        GameManager.Instance.AddThinkChoices(customerIndex);
    }

    public void ChooseFeels()
    {
        empathyChecker = EmpathyType.Feels;
        checker.empathyMapButtons = this;
        GameManager.Instance.AddThinkChoices(customerIndex);
    }

    public void ChooseSays()
    {
        empathyChecker = EmpathyType.Says;
        checker.empathyMapButtons = this;
        GameManager.Instance.AddThinkChoices(customerIndex);
    }
}
