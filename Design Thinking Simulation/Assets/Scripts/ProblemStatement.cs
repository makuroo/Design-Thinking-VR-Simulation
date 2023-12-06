using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using System;

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
            CalculateAnswer();
            answersStatement1[0] = likenessRankSorted[0];
            answersStatement1[1] = likenessRankSorted[likenessRankSorted.Count-1];
        }

    }

    public int Statement1(GameObject answer)
    {
        bool result;
        if (answer == null)
        {
            Debug.Log(null);
        }

        try
        {
            // If the parsing is successful, 'result' will contain the parsed integer.
            // You can continue with the logic here.
            if (int.Parse(answer.tag) == answersStatement1[currentIndex])
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }
        catch (FormatException)
        {
            result = false;
        }

        currentIndex++;
        if (currentIndex > 1)
        {
            currentIndex = 0;          
        }

        if (result == true)
        {
            Debug.Log(true);
            return 1;
        } 
        else
            return 0;
    }

    private void CalculateAnswer()
    {
        if (GameManager.Instance.peopleMet.Count <= 0)
            return;

        for (int i = 0; i < GameManager.Instance.peopleMet[0].transform.GetChild(0).GetComponent<People>().customerData.cakePreferences.LikeCake[0].taste.Count; i++)
        {
            int total = 0;
            for (int j = 0; j < GameManager.Instance.peopleMet.Count; j++)
            {
                Debug.Log("values" + j + " " + GameManager.Instance.peopleMet[j].transform.GetChild(0).GetComponent<People>().customerData.cakePreferences.LikeCake[0].taste[i]);
                total += GameManager.Instance.peopleMet[j].transform.GetChild(0).GetComponent<People>().customerData.CalculateLikeness(i);
                Debug.Log(total);

            }
            likenessRank[i] = total / GameManager.Instance.peopleMet.Count;
            Debug.Log(total / GameManager.Instance.peopleMet.Count);
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


    private IEnumerator DelayDeactivate()
    {
        yield return new WaitForSeconds(.1f);
        board.tasteAnswer.SetActive(false);
        board.choicesJenisKue.SetActive(true);
    }
}
