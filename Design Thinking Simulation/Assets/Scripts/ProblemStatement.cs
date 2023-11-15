using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class ProblemStatement : MonoBehaviour
{
    [SerializeField] private UserPersonaHistory history;
    [SerializeField] GameObject tasteAnswer;
    [SerializeField] TMP_Text statement;
    public List<float> answersStatement1 = new();
    public List<float> likenessRank = new();
    public List<float> likenessRankSorted = new();
    public static int currentIndex = 0;

    public BoardActivityUI board;

    public List<int> ukuranList = new(3);
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
        if (answer == null)
        {
            Debug.Log(null);
            return;
        }
            

        if(history.dictValues[0].value.CalculateLikeness(int.Parse(answer.tag)) == answersStatement1[currentIndex])
        {
            Debug.Log("true");
        }
        else
        {
            Debug.Log("wrong");
        }
        currentIndex++;
        if (currentIndex > 1)
        {
            currentIndex = 0;
            board.tasteAnswer.SetActive(false);
            board.choicesJenisKue.SetActive(true);
        }
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

    public int UkuranAvg()
    {
        ukuranList[0] = ukuranList[1] = ukuranList[2] = 0;
        for(int i=0; i < GameManager.Instance.peopleMet.Count; i++)
        {
            if (GameManager.Instance.peopleMet[i].GetComponentInChildren<People>().customerData.kueFavorit.ukuranKue.ToString() == "Kecil")
            {
                ukuranList[0]++;
            }else if(GameManager.Instance.peopleMet[i].GetComponentInChildren<People>().customerData.kueFavorit.ukuranKue.ToString() == "Sedang")
            {
                ukuranList[1]++;
            }
            else if(GameManager.Instance.peopleMet[i].GetComponentInChildren<People>().customerData.kueFavorit.ukuranKue.ToString() == "Besar")
            {
                ukuranList[2]++;
            }
        }

        if (ukuranList[0] == ukuranList.Max())
            return 0;
        else if (ukuranList[1] == ukuranList.Max())
            return 1;
        else 
            return 2;
    }
}
