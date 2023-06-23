using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public List<GameObject> peopleMet = new List<GameObject>();

    [Header("Drag and Drop")]
    public List<string> randomOptions = new List<string>();
    //temp list is used to be a pool for strings where choosen strings will be deleted from temp list to avoid duplicates
    public List<string> tempList;
    public List<string> tempListRandom = new List<string>();

    public EmpathyMapSO personCustomerEmpathy;

    public TMP_Text currentText;

    public bool canDoActivity = true;

    [Header("Random Question")]
    public int randomQuestionIndex;
    public int randomQuestionTypeIndex;
    
    public string[] RandomizedQuestion;
    public int[] RandomizedType;

    #region Question List
    [SerializeField] private List<string> manisQuestion = new List<string>();
    [SerializeField] private List<string> asinQuestion = new List<string>();
    [SerializeField] private List<string> asemQuestion = new List<string>();
    [SerializeField] private List<string> pahitQuestion = new List<string>();
    [SerializeField] private List<string> susuQuestion = new List<string>();
    [SerializeField] private List<string> coklatQuestion = new List<string>();
    [SerializeField] private List<string> vanilaQuestion = new List<string>();
    #endregion

    [Header("Customer Spawn")]
    public int WorldCustomerCount;
    public int CafeCustomerCount;
    public int RestaurantCustomerCount;
    public int CanteenCustomerCount;

    [SerializeField] int MaxWorldCustomerCount;
    [SerializeField] int MaxCafeCustomerCount;
    [SerializeField] int MaxRestaurantCustomerCount;
    [SerializeField] int MaxCanteenCustomerCount;

    public List<GameObject> customerList = new List<GameObject>();

    public List<GameObject> worldCustomerList = new List<GameObject>();
    public List<GameObject> restaurantCustomerList = new List<GameObject>();
    public List<GameObject> canteenCustomerList = new List<GameObject>();
    public List<GameObject> cafeCustomerList = new List<GameObject>();

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

        WorldCustomerCount = UnityEngine.Random.Range(1, MaxWorldCustomerCount);
        CafeCustomerCount = UnityEngine.Random.Range(1, MaxCafeCustomerCount);
        RestaurantCustomerCount = UnityEngine.Random.Range(1, MaxRestaurantCustomerCount);
        CanteenCustomerCount = UnityEngine.Random.Range(1, MaxCanteenCustomerCount);
    }

    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Start()
    {
        RandomizeQuestion();
        RandomizeCustomer();
    }

    #region Choices
    public void AddThinkChoices(int index, EmpathyMapButtons empathyMap)
    {
        Debug.Log(index);
        personCustomerEmpathy = peopleMet[index].GetComponentInChildren<People>().customerEmpathy;
        tempList = new List<string>(personCustomerEmpathy.Thinks);
        tempListRandom = randomOptions;
        for (int i = 0; i < 5; i++)
        {
            int playerOrRandom = UnityEngine.Random.Range(0, 2);
            if (playerOrRandom != 0 && tempList.Count != 0)
            {
                int thinkIndex =UnityEngine.Random.Range(0, tempList.Count);
                empathyMap.choicesGameObjectText[i].text = tempList[thinkIndex];
                tempList.RemoveAt(thinkIndex);
            }
            else
            {
                int randomIndex =UnityEngine.Random.Range(0, tempListRandom.Count);
                empathyMap.choicesGameObjectText[i].text = tempListRandom[randomIndex];
                tempListRandom.RemoveAt(randomIndex);
            }
        }

    }

    public void AddDoesChoices(int index)
    {
        personCustomerEmpathy = peopleMet[index].GetComponent<People>().customerEmpathy;
        EmpathyMapButtons empathyMap = transform.parent.GetComponent<EmpathyMapButtons>();
        tempList = new List<string>(personCustomerEmpathy.Does);
        tempListRandom = randomOptions;
        for (int i = 0; i < 5; i++)
        {
            int playerOrRandom = UnityEngine.Random.Range(0, 2);
            if (playerOrRandom != 0 && tempList.Count != 0)
            {
                int doesIndex = UnityEngine.Random.Range(0, tempList.Count);
                empathyMap.choicesGameObjectText[i].text = tempList[doesIndex];
                tempList.RemoveAt(doesIndex);
            }
            else
            {
                int randomIndex = UnityEngine.Random.Range(0, tempListRandom.Count);
                empathyMap.choicesGameObjectText[i].text = tempListRandom[randomIndex];
                tempListRandom.RemoveAt(randomIndex);
            }
        }

    }

    public void AddFeelsChoices(int index)
    {
        personCustomerEmpathy = peopleMet[index].GetComponent<People>().customerEmpathy;
        EmpathyMapButtons empathyMap = transform.parent.GetComponent<EmpathyMapButtons>();
        tempList = new List<string>(personCustomerEmpathy.Feels);
        tempListRandom = randomOptions;
        for (int i = 0; i < 5; i++)
        {
            int playerOrRandom = UnityEngine.Random.Range(0, 2);
            if (playerOrRandom != 0 && tempList.Count != 0)
            {
                int feelsIndex = UnityEngine.Random.Range(0, tempList.Count);
                empathyMap.choicesGameObjectText[i].text = tempList[feelsIndex];
                tempList.RemoveAt(feelsIndex);
            }
            else
            {
                int randomIndex =UnityEngine.Random.Range(0, tempListRandom.Count);
                empathyMap.choicesGameObjectText[i].text = tempListRandom[randomIndex];
                tempListRandom.RemoveAt(randomIndex);
            }
        }

    }

    public void AddSaysChoices(int index)
    {
        personCustomerEmpathy = peopleMet[index].GetComponent<People>().customerEmpathy;
        EmpathyMapButtons empathyMap = transform.parent.GetComponent<EmpathyMapButtons>();
        tempList = new List<string>(personCustomerEmpathy.Says);
        tempListRandom = randomOptions;
        for (int i = 0; i < 5; i++)
        {
            int playerOrRandom =UnityEngine.Random.Range(0, 2);
            if (playerOrRandom != 0 && tempList.Count != 0)
            {
                int saysIndex =UnityEngine.Random.Range(0, tempList.Count);
                empathyMap.choicesGameObjectText[i].text = tempList[saysIndex];
                tempList.RemoveAt(saysIndex);
                Debug.Log(personCustomerEmpathy.Says.Count);
            }
            else
            {
                int randomIndex =UnityEngine.Random.Range(0, tempListRandom.Count);
                empathyMap.choicesGameObjectText[i].text = tempListRandom[randomIndex];
                tempListRandom.RemoveAt(randomIndex);
            }
        }
    }
    #endregion

    #region Random Question
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
    #endregion
    #region Random Customer
    private void RandomizeCustomer()
    {
        List<GameObject> customerListCopy = new List<GameObject>(customerList);
        for (int i = 0; i < WorldCustomerCount; i++)
        {
            int randomCustomerIndex = UnityEngine.Random.Range(0, customerListCopy.Count);
            worldCustomerList.Add(customerListCopy[randomCustomerIndex]);
            customerListCopy.RemoveAt(randomCustomerIndex);
        }

        for (int i=0; i<RestaurantCustomerCount; i++)
        {
            int randomCustomerIndex = UnityEngine.Random.Range(0, customerListCopy.Count);
            restaurantCustomerList.Add(customerListCopy[randomCustomerIndex]);
            customerListCopy.RemoveAt(randomCustomerIndex);
        }

        for (int i = 0; i < CafeCustomerCount; i++)
        {
            int randomCustomerIndex = UnityEngine.Random.Range(0, customerListCopy.Count);
            cafeCustomerList.Add(customerListCopy[randomCustomerIndex]);
            customerListCopy.RemoveAt(randomCustomerIndex);
        }

        for (int i = 0; i < CanteenCustomerCount; i++)
        {
            int randomCustomerIndex = UnityEngine.Random.Range(0, customerListCopy.Count);
            canteenCustomerList.Add(customerListCopy[randomCustomerIndex]);
            customerListCopy.RemoveAt(randomCustomerIndex);
        }
    }
    #endregion
}
