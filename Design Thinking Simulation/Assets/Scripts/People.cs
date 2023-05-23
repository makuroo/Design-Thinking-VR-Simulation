using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class People : MonoBehaviour
{
    [SerializeField] private string peopleName;
    [SerializeField] private CakeSO[] cakePreference;
    public int questionIndex = 0;
    [SerializeField] private Text textField;
    // Start is called before the first frame update
    public void CalculateLikeness()
    {
        int likeness = 0;
        for(int i =0; i<cakePreference.Length; i++)
        {
            if (i == cakePreference.Length - 1)
            {
                likeness -= cakePreference[i].taste[questionIndex - 1];
            }
            else
            {
                likeness += cakePreference[i].taste[questionIndex - 1];
            }

        }

        if (likeness == 0)
            textField.text = "Neutral";

        if (likeness == 1)
            textField.text = "Like";

        if (likeness > 1)
            textField.text = "Really Like";

        if (likeness < 0)
            textField.text = "Dislike";
    }
}
