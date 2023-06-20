using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public enum QuestionType
    {
        Manis,
        Asin,
        Asem,
        Pahit,
        Susu,
        Coklat,
        Vanila
    }


public class Questions : MonoBehaviour
{
    public int index;
    public QuestionType type;
    [SerializeField] private People person;
    [SerializeField] private GameObject canvas;

    public void Question()
    {
        person = transform.parent.parent.parent.GetComponent<People>();
        if(person == null)
        {
            Debug.Log("person kosong");
            return;
        }
        GameManager.Instance.peopleMet.Add(person.gameObject);
        person.questionIndex = index;
        person.CalculateLikeness();
        // disable commentb line dibwh kalau mau bs nnya nonstop
        //canvas.gameObject.SetActive(false);
        person.AnswerSelected();
    }
}
