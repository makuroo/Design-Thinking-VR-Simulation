using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questions : MonoBehaviour
{
    public int questionIndex;
    [SerializeField]private People person;
    [SerializeField] private GameObject canvas;

    public void Question()
    {
        Debug.Log(questionIndex);
        person.questionIndex = questionIndex;
        person.CalculateLikeness();
        // disable commentb line dibwh kalau mau bs nnya nonstop
        canvas.gameObject.SetActive(false);
    }
}
