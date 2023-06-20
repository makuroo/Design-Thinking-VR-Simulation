using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

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

    
    public int randomQuestionIndex;
    public int randomQuestionTypeIndex;
    
    public string[] RandomizedQuestion;
    public int[] RandomizedType;

    [SerializeField] private List<string> manisQuestion = new List<string>();
    [SerializeField] private List<string> asinQuestion = new List<string>();
    [SerializeField] private List<string> asemQuestion = new List<string>();
    [SerializeField] private List<string> pahitQuestion = new List<string>();
    [SerializeField] private List<string> susuQuestion = new List<string>();
    [SerializeField] private List<string> coklatQuestion = new List<string>();
    [SerializeField] private List<string> vanilaQuestion = new List<string>();

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

    private void Start()
    {
        RandomizeQuestion();
    }

    public void AddThinkChoices(int index)
    {
        personCustomerEmpathy = peopleMet[index].GetComponent<People>().customerEmpathy;
        
        tempList = new List<string>(personCustomerEmpathy.Thinks);
        tempListRandom = randomOptions;
        for (int i = 0; i < 5; i++)
        {
            int playerOrRandom = UnityEngine.Random.Range(0, 2);
            if (playerOrRandom != 0 && tempList.Count != 0)
            {
                int thinkIndex =UnityEngine.Random.Range(0, tempList.Count);
                choicesGameObjectText[i].text = tempList[thinkIndex];
                tempList.RemoveAt(thinkIndex);
            }
            else
            {
                int randomIndex =UnityEngine.Random.Range(0, tempListRandom.Count);
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
            int playerOrRandom = UnityEngine.Random.Range(0, 2);
            if (playerOrRandom != 0 && tempList.Count != 0)
            {
                int doesIndex = UnityEngine.Random.Range(0, tempList.Count);
                choicesGameObjectText[i].text = tempList[doesIndex];
                tempList.RemoveAt(doesIndex);
            }
            else
            {
                int randomIndex = UnityEngine.Random.Range(0, tempListRandom.Count);
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
            int playerOrRandom = UnityEngine.Random.Range(0, 2);
            if (playerOrRandom != 0 && tempList.Count != 0)
            {
                int feelsIndex = UnityEngine.Random.Range(0, tempList.Count);
                choicesGameObjectText[i].text = tempList[feelsIndex];
                tempList.RemoveAt(feelsIndex);
            }
            else
            {
                int randomIndex =UnityEngine.Random.Range(0, tempListRandom.Count);
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
            int playerOrRandom =UnityEngine.Random.Range(0, 2);
            if (playerOrRandom != 0 && tempList.Count != 0)
            {
                int saysIndex =UnityEngine.Random.Range(0, tempList.Count);
                choicesGameObjectText[i].text = tempList[saysIndex];
                tempList.RemoveAt(saysIndex);
                Debug.Log(personCustomerEmpathy.Says.Count);
            }
            else
            {
                int randomIndex =UnityEngine.Random.Range(0, tempListRandom.Count);
                choicesGameObjectText[i].text = tempListRandom[randomIndex];
                tempListRandom.RemoveAt(randomIndex);
            }
        }
    }

    private void RandomizeQuestion(){
        for(int i=0; i<3;i++){
            randomQuestionTypeIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(QuestionType)).Length);
            RandomizedType[i] =randomQuestionTypeIndex;
            if(randomQuestionTypeIndex == 0)
            {
                randomQuestionIndex = UnityEngine.Random.Range(0, manisQuestion.Count);
                Debug.Log(randomQuestionIndex);
                RandomizedQuestion[i] = manisQuestion[randomQuestionIndex];
                manisQuestion.RemoveAt(randomQuestionIndex);
            }else if(randomQuestionTypeIndex == 1)
            {
                randomQuestionIndex = UnityEngine.Random.Range(0, asinQuestion.Count);
                RandomizedQuestion[i] = asinQuestion[randomQuestionIndex];
                asinQuestion.RemoveAt(randomQuestionIndex);
            }else if (randomQuestionTypeIndex == 2)
            {
                randomQuestionIndex = UnityEngine.Random.Range(0, asemQuestion.Count);
                RandomizedQuestion[i] = asemQuestion[randomQuestionIndex];
                asemQuestion.RemoveAt(randomQuestionIndex);
            }else if(randomQuestionTypeIndex == 3)
            {
                randomQuestionIndex = UnityEngine.Random.Range(0, pahitQuestion.Count);
                RandomizedQuestion[i] = pahitQuestion[randomQuestionIndex];
                pahitQuestion.RemoveAt(randomQuestionIndex);
            }else if(randomQuestionTypeIndex == 4)
            {
                randomQuestionIndex = UnityEngine.Random.Range(0, susuQuestion.Count);
                RandomizedQuestion[i] = susuQuestion[randomQuestionIndex];
                susuQuestion.RemoveAt(randomQuestionIndex);
            }else if(randomQuestionTypeIndex == 5)
            {
                randomQuestionIndex = UnityEngine.Random.Range(0, coklatQuestion.Count);
                RandomizedQuestion[i] = coklatQuestion[randomQuestionIndex];
                coklatQuestion.RemoveAt(randomQuestionIndex);
            }else if(randomQuestionTypeIndex == 6)
            {
                randomQuestionIndex = UnityEngine.Random.Range(0, vanilaQuestion.Count);
                RandomizedQuestion[i] = vanilaQuestion[randomQuestionIndex];
                vanilaQuestion.RemoveAt(randomQuestionIndex);
            }
        }
    } 


}
