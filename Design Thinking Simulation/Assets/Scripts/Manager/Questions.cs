using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questions : MonoBehaviour
{
    public enum QuestionType
    {
        Flavor,
        Topping,
        Texture,
        Variations,
        KeyIngredients,
        ServingStyle,
        Crust,
        Enhancements,
        Seasoning
    }

    public int questionIndex;
    public QuestionType type;
    [SerializeField]private People person;
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
        person.questionIndex = questionIndex;
        person.CalculateLikeness();
        // disable commentb line dibwh kalau mau bs nnya nonstop
        //canvas.gameObject.SetActive(false);
        person.AnswerSelected();
    }
}
