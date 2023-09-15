using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProblemStatement : MonoBehaviour
{
    [SerializeField] private UserPersonaHistory history;
    [SerializeField] GameObject tasteAnswer;
    [SerializeField] TMP_Text statement;
    [SerializeField] List<float> answersStatement1 = new();
    public List<float> likenessRank = new();
    public List<float> likenessRankSorted = new();
    public static int currentIndex = 0;
    int x = 0;
    private void Start()
    {
        if (history!= null)
        {
            CalculateAnswer(history);
            answersStatement1[0] = likenessRankSorted[0];
            answersStatement1[1] = likenessRankSorted[likenessRankSorted.Count-1];
 
        }

    }

    public void Statement1(GameObject answer)
    {
        if(currentIndex>1)
            currentIndex = 0;
        string inputString = "Favourite flavour is _ Most dislike flavour is _";
        statement.text = inputString;
        if(history.dictValues[0].value.CalculateLikeness(int.Parse(answer.tag)) == answersStatement1[currentIndex])
        {
            Debug.Log("true");

        }
        else
        {
            Debug.Log("wrong");
        }
        currentIndex++;
    }

    

    private void CalculateAnswer(UserPersonaHistory history)
    {
        Debug.Log(history);
        for (int i = 0; i < history.dictValues[0].value.cakePreferences.LikeCake[0].taste.Count; i++)
        {
            int total = 0;
            for (int j = 0; j < history.dictValues.Count; j++)
            {
                Debug.Log("values" + j + " " + history.dictValues[j].value.cakePreferences.LikeCake[0].taste[i]);
                total += history.dictValues[j].value.CalculateLikeness(i);
                Debug.Log(total);

            }
            likenessRank[i] = total / history.dictValues.Count;
            Debug.Log(total / history.dictValues.Count);
        }
        likenessRankSorted = new List<float>(likenessRank);
        likenessRankSorted.Sort((x, y) => y.CompareTo(x));
    }
}
