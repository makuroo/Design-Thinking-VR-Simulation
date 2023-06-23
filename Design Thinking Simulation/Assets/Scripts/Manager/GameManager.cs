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

    bool gameTimeBool;
    public int currentMinute;
    public int currentHour;
    public int currentDay;
    [SerializeField] float playerAwakeOnHour = 4;
    [SerializeField] float playerNeedRestTime = 18;
    [SerializeField] float secondOnRealLifeToChangeMinuteGameTime;
    [SerializeField] float realLifeMinuteGamePlayPerCycle;
    //rumus buat nge set ini, intinya berapa second buat +1 minute di game.. cara ngitungnya, tinggal total hours on day - playerTime mulai beraktifitas - PlayerTime tidak bisa beraktifitas
    //nah itu kan dapet total game time di dalem gamenya yg bisa beraktifitas.. lalu tinggal tentuin satu hari aktivitas di game ingin berapa menit di real life, tinggal di bikin skala perbandingan waktunya
    // jadi rumusnya = ((24-playerRestTime)*60 Minute) / (realLifeMinuteGameplayPercycle * 60seconds) 
    // jadi rumusnya = ((realLifeMinuteGameplayPercycle*60) / (24-(24-playerNeedRestTime)+(playerAwakeOnHour))*60 minute)) // rumus yang dipake di start
    // jadi rumusnya = ((15*60) / (24-((24-18)+(4))*60 minute)
    // jadi rumusnya = ( 900 / (24-10)*60)
    // jadi rumusnya = 900/840
    //jadi rumusnya = 1.07142

    //variable untuk directional light auto rotate
    [SerializeField] GameObject directionalLight;
    [HideInInspector] public float minMinute = 0;
    [HideInInspector] public float maxMinute = 1440f;
    [HideInInspector] public float sunriseHour;
    [HideInInspector] public float totalCurrentMinute;
    [HideInInspector] public float sunStartAngle;
    [HideInInspector] public float normalizedValue;
    [HideInInspector] public float angle;
    bool isSleeping = false;

    [SerializeField] float AnswerTimeBed;
    BedScript bedScript;



    //question player
    public int maxQuestionPerDay;
    [HideInInspector] public int questionRemaining;

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
        
        bedScript = GameObject.Find("Bed").GetComponent<BedScript>();

        directionalLight = GameObject.Find("Directional Light");
        secondOnRealLifeToChangeMinuteGameTime = ((realLifeMinuteGamePlayPerCycle * 60)/ ((24 - (24 - playerNeedRestTime + playerAwakeOnHour))*60));
        sunriseHour = playerAwakeOnHour;


        //jika tidak punya savean..
        questionRemaining = maxQuestionPerDay;
    }


    private void Update()
    {
        SetGameTime();
        SetDirectionalLightRotation();
    }

    public void LoadGame()
    {
        currentMinute = PlayerPrefs.GetInt("SaveCurrentMinute");
        currentHour = PlayerPrefs.GetInt("CurrentHour");
        currentDay = PlayerPrefs.GetInt("CurrentDay");
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("SaveCurrentMinute", currentMinute);
        PlayerPrefs.SetInt("CurrentHour", currentHour);
        PlayerPrefs.SetInt("CurrentDay", currentDay);
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
        for (int i = 0; i < RestaurantCustomerCount; i++)
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

    public void PlayerSleep()
    {
        if (currentHour >= playerNeedRestTime || currentHour < playerAwakeOnHour) //jika masuk waktu tidur
        {
            if (currentHour < 24 && currentHour >= playerNeedRestTime) //jika tidur sebelum jam 12 maka day +1 ... kalau dia begadang otomatis udah change day dari function SetGameTime();
            {
                currentDay += 1;
            }
            ResetQuestionRemaining();
            currentHour = Mathf.FloorToInt(playerAwakeOnHour);
            //isSleeping = true;
        }
        else if(currentHour<playerNeedRestTime && currentHour >= playerAwakeOnHour)
        {
            bedScript.ActivateBedAnswer(AnswerTimeBed);
        }
    }

    public void SetGameTime()
    {

        //Debug.Log(currentGameTime);
        if(!isSleeping)
        {
            if (currentMinute >= 60)
            {
                currentMinute = 0;
                currentHour += 1;
                if(currentHour >= 24)
                {
                    currentHour = 0;
                    currentDay += 1;
                    //masukin function buat ngerandom customer lagi karena ganti hari
                }
            }
            if (gameTimeBool == false)
            {
                gameTimeBool = true;
                StartCoroutine(PlusMinute());
            }
        }
    }

    IEnumerator PlusMinute()
    {
        yield return new WaitForSeconds(secondOnRealLifeToChangeMinuteGameTime);
        if (gameTimeBool == true)
        {
            currentMinute += 1;
            SetGameTime();
            gameTimeBool = false;
        }
    }

    public void SetDirectionalLightRotation()
    {
        totalCurrentMinute = currentMinute + (currentHour * 60);
        sunStartAngle = -((sunriseHour * 60) / 4); //dibagi 4 karena per 4 minute turn 1 angle
        normalizedValue = (totalCurrentMinute - minMinute) / (maxMinute - minMinute);
        angle = Mathf.Lerp(sunStartAngle, 360+sunStartAngle, normalizedValue); //kecepatan sunset ditentuin oleh value(y) makin kecil makin lambat
        if(directionalLight != null)
        {
            directionalLight.transform.rotation = Quaternion.Euler(angle, 0f, 0f);
        }
    }

    public bool isOnActivityTime()
    {
        if(currentHour >= playerAwakeOnHour && currentHour< playerNeedRestTime)
        {
            return true;
        }
        return false;
    }




    void ResetQuestionRemaining()
    {
        questionRemaining = maxQuestionPerDay;
    }

    public void GetDirectionalLight()
    {
        Debug.Log("GetDirectionalLight");
        Debug.Log(GameObject.Find("Directional Light"));
        directionalLight = GameObject.Find("Directional Light");
        if (directionalLight != null)
        {
            // Object found, do something with it
            // ...
            Debug.Log("DirectionalLight Found");
        }
        else
        {
            // Object not found
            Debug.LogError("Directional Light object not found!");
        }
    }
}
