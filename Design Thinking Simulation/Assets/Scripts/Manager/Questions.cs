using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEditor;

    public enum QuestionType
    {
        Manis,
        Asin,
        Asem,
        Pahit
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
        person.Reply();
        person.AnswerSelected();
    }

    public void NameQuestion()
    {
        person = transform.parent.parent.parent.GetComponent<People>();
        person.player.PlayerAsk();
        if (indexList.Count == 0)
        {
            indexList.Add(person.index);
            GameManager.Instance.peopleMet.Add(GameManager.Instance.customerList[person.index]);
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
                }
            }
        }

        if(GameManager.Instance.CanAskCheck())
        {
            person.AnswerNameSelected();
            person.nameTextObj.text = person.customerData.peopleName;
        }
        //person.UIPertanyaan.SetActive(true);
        //person.nameQuestionCanvas.SetActive(false);
    }
}
