using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
    public static List<int> indexList = new List<int>();
    public QuestionType type;
    [SerializeField] private People person;
    [SerializeField] private GameObject canvas;

    public void Question()
    {
        person = transform.parent.parent.parent.GetComponent<People>();
        person.questionIndex = index;
        person.CalculateLikeness();
        person.AnswerSelected();
    }

    public void NameQuestion()
    {
        person = transform.parent.parent.parent.GetComponent<People>();
        if (indexList.Count == 0)
        {
            indexList.Add(person.index);
            GameManager.Instance.peopleMet.Add(GameManager.Instance.customerList[person.index]);
            person.met = true;
        }
        else if (indexList.Count >= 1)
        {
            for (int i = 0; i < indexList.Count; i++)
            {
                bool found;
                if (person.index == indexList[i])
                {
                    found = true;
                }
                else
                {
                    found = false;
                }

                if (i == indexList.Count - 1 && found == false)
                {
                    indexList.Add(person.index);
                    GameManager.Instance.peopleMet.Add(GameManager.Instance.customerList[person.index]);
                    GameManager.Instance.customerList[i].GetComponentInChildren<People>().met = true;
                    person.met = true;
                }
            }
        }

        person.UIPertanyaan.SetActive(true);
        person.nameTextObj.text = person.peopleName;
        person.nameQuestionCanvas.SetActive(false);
    }
}
