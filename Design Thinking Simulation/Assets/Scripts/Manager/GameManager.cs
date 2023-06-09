using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public List<GameObject> peopleMet = new List<GameObject>();

    public List<string> randomOptions = new List<string>();

    //temp list is used to be a pool for strings where choosen strings will be deleted from temp list to avoid duplicates
    public List<string> tempList;
    public List<string> tempListRandom = new List<string>();

    public List<TMP_Text> choicesGameObjectText = new List<TMP_Text>();

    public EmpathyMapSO personCustomerEmpathy;

    public TMP_Text currentText;

    public bool canDoActivity = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensures that the game manager persists across scene changes
        }
        else
        {
            Destroy(gameObject); // Destroys any duplicate instances of the game manager
        }
    }

    public static GameManager Instance
    {
        get { return instance; }
    }

    public void AddThinkChoices(int index)
    {
        personCustomerEmpathy = peopleMet[index].GetComponent<People>().customerEmpathy;
        
        tempList = new List<string>(personCustomerEmpathy.Thinks);
        tempListRandom = randomOptions;
        for (int i = 0; i < 5; i++)
        {
            int playerOrRandom = Random.Range(0, 2);
            if (playerOrRandom != 0 && tempList.Count != 0)
            {
                int thinkIndex = Random.Range(0, tempList.Count);
                choicesGameObjectText[i].text = tempList[thinkIndex];
                tempList.RemoveAt(thinkIndex);
            }
            else
            {
                int randomIndex = Random.Range(0, tempListRandom.Count);
                choicesGameObjectText[i].text = tempListRandom[randomIndex];
                tempListRandom.RemoveAt(randomIndex);
            }
        }

    }

    public void AddDoesChoices(int index)
    {
        personCustomerEmpathy = peopleMet[index].GetComponent<People>().customerEmpathy;

        tempList = new List<string>(personCustomerEmpathy.Does);
        tempListRandom = randomOptions;
        for (int i = 0; i < 5; i++)
        {
            int playerOrRandom = Random.Range(0, 2);
            if (playerOrRandom != 0 && tempList.Count != 0)
            {
                int doesIndex = Random.Range(0, tempList.Count);
                choicesGameObjectText[i].text = tempList[doesIndex];
                tempList.RemoveAt(doesIndex);
            }
            else
            {
                int randomIndex = Random.Range(0, tempListRandom.Count);
                choicesGameObjectText[i].text = tempListRandom[randomIndex];
                tempListRandom.RemoveAt(randomIndex);
            }
        }

    }

    public void AddFeelsChoices(int index)
    {
        personCustomerEmpathy = peopleMet[index].GetComponent<People>().customerEmpathy;

        tempList = new List<string>(personCustomerEmpathy.Feels);
        tempListRandom = randomOptions;
        for (int i = 0; i < 5; i++)
        {
            int playerOrRandom = Random.Range(0, 2);
            if (playerOrRandom != 0 && tempList.Count != 0)
            {
                int feelsIndex = Random.Range(0, tempList.Count);
                choicesGameObjectText[i].text = tempList[feelsIndex];
                tempList.RemoveAt(feelsIndex);
            }
            else
            {
                int randomIndex = Random.Range(0, tempListRandom.Count);
                choicesGameObjectText[i].text = tempListRandom[randomIndex];
                tempListRandom.RemoveAt(randomIndex);
            }
        }

    }

    public void AddSaysChoices(int index)
    {
        personCustomerEmpathy = peopleMet[index].GetComponent<People>().customerEmpathy;

        tempList = new List<string>(personCustomerEmpathy.Says);
        tempListRandom = randomOptions;
        for (int i = 0; i < 5; i++)
        {
            int playerOrRandom = Random.Range(0, 2);
            if (playerOrRandom != 0 && tempList.Count != 0)
            {
                int saysIndex = Random.Range(0, tempList.Count);
                choicesGameObjectText[i].text = tempList[saysIndex];
                tempList.RemoveAt(saysIndex);
                Debug.Log(personCustomerEmpathy.Says.Count);
            }
            else
            {
                int randomIndex = Random.Range(0, tempListRandom.Count);
                choicesGameObjectText[i].text = tempListRandom[randomIndex];
                tempListRandom.RemoveAt(randomIndex);
            }
        }

    }

}
