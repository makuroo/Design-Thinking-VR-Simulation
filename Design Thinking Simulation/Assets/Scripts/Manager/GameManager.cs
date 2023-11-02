using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BNG;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public bool canDoActivity = true;

    public List<GameObject> peopleMet = new List<GameObject>();


    public CustomerDataSO personCustomerData;

    public TMP_Text currentText;

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
    public int cafeCustomerCount;
    public int restaurantCustomerCount;
    public int canteenCustomerCount;
    [SerializeField] List<GameObject> maleNpcList = new();
    [SerializeField] List<GameObject> femaleNpcList = new();
    [SerializeField] int maxCustomerSpawn;
    [SerializeField] int maxCustomer;
    public List<GameObject> customerList = new List<GameObject>();

    public List<GameObject> worldCustomerList = new List<GameObject>();
    public List<GameObject> restaurantCustomerList = new List<GameObject>();
    public List<GameObject> canteenCustomerList = new List<GameObject>();
    public List<GameObject> cafeCustomerList = new List<GameObject>();

    /*bool gameTimeBool;
    public int currentMinute;
    public int currentHour;*/
    public int currentDay = 1;
    public int maxDay = 31;
    /*
    [SerializeField] float playerAwakeOnHour = 4;
    [SerializeField] float playerNeedRestTime = 18;
    [SerializeField] float secondOnRealLifeToChangeMinuteGameTime;
    [SerializeField] float realLifeMinuteGamePlayPerCycle;
    */
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
    /*[HideInInspector] public float minMinute = 0;
    [HideInInspector] public float maxMinute = 1440f;
    [HideInInspector] public float sunriseHour;
    [HideInInspector] public float totalCurrentMinute;
    [HideInInspector] public float sunStartAngle;
    [HideInInspector] public float normalizedValue;
    [HideInInspector] public float angle;
    //bool isSleeping = false;*/

    [SerializeField] float AnswerTimeBed;
    BedScript bedScript;


    //clock ui rotator
    //public GameObject minuteArrow;
    //public GameObject hourArrow;
    public Image clockImage;

    //question player
    public int maxQuestionPerDay;


    public int questionRemaining;

    public int interviewCount = 0;
    public int userPersonaCount = 0;
    private PlayerScript player;
    string previousWorld;


    private void Awake()
    {
        LoadGame();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensures that the game manager persists across scene changes
        }
        else
        {
            Destroy(gameObject); // Destroys any duplicate instances of the game manager
        }

        //WorldCustomerCount = UnityEngine.Random.Range(1, maxCustomerSpawn);

        directionalLight = GameObject.Find("Directional Light");
        /*secondOnRealLifeToChangeMinuteGameTime = ((realLifeMinuteGamePlayPerCycle * 60)/ ((24 - (24 - playerNeedRestTime + playerAwakeOnHour))*60));
        sunriseHour = playerAwakeOnHour;*/

        /*
        if (PlayerPrefs.GetInt("isSaveExist") == 1)
        {
            currentDay = PlayerPrefs.GetInt("CurrentDay");
            questionRemaining = PlayerPrefs.GetInt("QuestionRemaining");
            cafeCustomerCount = PlayerPrefs.GetInt("CafeCustomerCount");
            restaurantCustomerCount = PlayerPrefs.GetInt("RestaurantCustomerCount");
            canteenCustomerCount = PlayerPrefs.GetInt("CanteenCustomerCount");
        }
        else //jika tidak ada savean (new game)
        {
            NewGame();
            SaveGame();
        }*/

        //NPCRandomizer(maxCustomer);
    }

    public void DistributeCustomerCount()
    {
        cafeCustomerCount = UnityEngine.Random.Range(1, Mathf.FloorToInt(maxCustomerSpawn/2)+1);
        restaurantCustomerCount = UnityEngine.Random.Range((maxCustomerSpawn / 2 + 1) - cafeCustomerCount, maxCustomerSpawn / 2);
        canteenCustomerCount = maxCustomerSpawn - cafeCustomerCount - restaurantCustomerCount;
        Debug.Log("Cafe = " + cafeCustomerCount);
        Debug.Log("Restaurant = " + restaurantCustomerCount);
        //Debug.Log("Canteen = " + maxCustomerSpawn + "" + CafeCustomerCount + "" + CanteenCustomerCount);
        Debug.Log("Canteen = " + canteenCustomerCount);
    }

    private void Start()
    {
        // dipindahin ke new game and load game
        //RandomizeQuestion();
        //RandomizeCustomer();
    }

    private void Update()
    {
        /*
        SetGameTime();
        SetDirectionalLightRotation();
        */
    }

    public void NewGame()
    {
        currentDay = 1;
        questionRemaining = maxQuestionPerDay;
        DistributeCustomerCount();
        RandomizeQuestion();
        RandomizeCustomer();
        PlayerPrefs.SetInt("isSaveExist", 0);
        SaveGame();
        Debug.Log("Nih New Game");
    }
    
    
    public void LoadGame()
    {
        /*
        currentMinute = PlayerPrefs.GetInt("SaveCurrentMinute");
        currentHour = PlayerPrefs.GetInt("CurrentHour");*/

        //jika ada saveannya
        if(PlayerPrefs.GetInt("isSaveExist") == 1)
        {
            currentDay = PlayerPrefs.GetInt("CurrentDay");
            questionRemaining = PlayerPrefs.GetInt("QuestionRemaining");
            cafeCustomerCount = PlayerPrefs.GetInt("CafeCustomerCount");
            restaurantCustomerCount = PlayerPrefs.GetInt("RestaurantCustomerCount");
            canteenCustomerCount = PlayerPrefs.GetInt("CanteenCustomerCount");
            RandomizeQuestion();
            RandomizeCustomer();
            Debug.Log("Nih Load Game");
        }
        else //jika tidak ada savean (new game)
        {
            NewGame();
        }
    }

    public void SaveGame()
    {
        /*
        PlayerPrefs.SetInt("SaveCurrentMinute", currentMinute);
        PlayerPrefs.SetInt("CurrentHour", currentHour);
        */
        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.SetInt("QuestionRemaining", questionRemaining);
        PlayerPrefs.SetInt("CafeCustomerCount", cafeCustomerCount);
        PlayerPrefs.SetInt("RestaurantCustomerCount", restaurantCustomerCount);
        PlayerPrefs.SetInt("CanteenCustomerCount", canteenCustomerCount);
        PlayerPrefs.SetInt("isSaveExist", 1);
        Debug.Log("Nih Save Game");
    }

    public static GameManager Instance
    {
        get { return instance; }
    }

    public int RestaurantCustomerCount { get; private set; }
    public int CafeCustomerCount { get; private set; }
    public int CanteenCustomerCount { get; private set; }

    #region Random Question
    public void RandomizeQuestion(){
        Debug.Log("Randomize Question");
        for(int i=0; i<3;i++){
            randomQuestionTypeIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(QuestionType)).Length);
            RandomizedType[i] = randomQuestionTypeIndex;
            if (randomQuestionTypeIndex == 0)
            {
                randomQuestionIndex = UnityEngine.Random.Range(0, manisQuestion.Count);
                //Debug.Log(randomQuestionIndex);
                RandomizedQuestion[i] = manisQuestion[randomQuestionIndex];
                manisQuestion.RemoveAt(randomQuestionIndex);
            }
            else if (randomQuestionTypeIndex == 1)
            {
                randomQuestionIndex = UnityEngine.Random.Range(0, asinQuestion.Count);
                RandomizedQuestion[i] = asinQuestion[randomQuestionIndex];
                asinQuestion.RemoveAt(randomQuestionIndex);
            }
            else if (randomQuestionTypeIndex == 2)
            {
                randomQuestionIndex = UnityEngine.Random.Range(0, asemQuestion.Count);
                RandomizedQuestion[i] = asemQuestion[randomQuestionIndex];
                asemQuestion.RemoveAt(randomQuestionIndex);
            }
            else if (randomQuestionTypeIndex == 3)
            {
                randomQuestionIndex = UnityEngine.Random.Range(0, pahitQuestion.Count);
                RandomizedQuestion[i] = pahitQuestion[randomQuestionIndex];
                pahitQuestion.RemoveAt(randomQuestionIndex);
            }
            else if (randomQuestionTypeIndex == 4)
            {
                randomQuestionIndex = UnityEngine.Random.Range(0, susuQuestion.Count);
                RandomizedQuestion[i] = susuQuestion[randomQuestionIndex];
                susuQuestion.RemoveAt(randomQuestionIndex);
            }
            else if (randomQuestionTypeIndex == 5)
            {
                randomQuestionIndex = UnityEngine.Random.Range(0, coklatQuestion.Count);
                RandomizedQuestion[i] = coklatQuestion[randomQuestionIndex];
                coklatQuestion.RemoveAt(randomQuestionIndex);
            }
            else if (randomQuestionTypeIndex == 6)
            {
                randomQuestionIndex = UnityEngine.Random.Range(0, vanilaQuestion.Count);
                RandomizedQuestion[i] = vanilaQuestion[randomQuestionIndex];
                vanilaQuestion.RemoveAt(randomQuestionIndex);
            }
        }
    }
    #endregion
    #region Random Customer
    public void RandomizeCustomer()
    {
        Debug.Log("Randomize Customer jalan");
        List<GameObject> customerListCopy = new List<GameObject>(customerList);
        //for (int i = 0; i < WorldCustomerCount; i++)
        //{
        //    int randomCustomerIndex = UnityEngine.Random.Range(0, customerListCopy.Count);
        //    worldCustomerList.Add(customerListCopy[randomCustomerIndex]);
        //    customerListCopy.RemoveAt(randomCustomerIndex);
        //}
        for (int i = 0; i < restaurantCustomerCount; i++)
        {
            int randomCustomerIndex = UnityEngine.Random.Range(0, customerListCopy.Count);
            restaurantCustomerList.Add(customerListCopy[randomCustomerIndex]);
            customerListCopy.RemoveAt(randomCustomerIndex);
        }

        for (int i = 0; i < cafeCustomerCount; i++)
        {
            int randomCustomerIndex = UnityEngine.Random.Range(0, customerListCopy.Count);
            cafeCustomerList.Add(customerListCopy[randomCustomerIndex]);
            customerListCopy.RemoveAt(randomCustomerIndex);
        }

        for (int i = 0; i < canteenCustomerCount; i++)
        {
            int randomCustomerIndex = UnityEngine.Random.Range(0, customerListCopy.Count);
            canteenCustomerList.Add(customerListCopy[randomCustomerIndex]);
            customerListCopy.RemoveAt(randomCustomerIndex);
        }
    }
    #endregion

    public void PlayerSleep()
    {
        /*if (currentHour >= playerNeedRestTime || currentHour < playerAwakeOnHour) //jika masuk waktu tidur
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
        */
        if (CanAskCheck())
        {
            bedScript.ActivateBedAnswer(AnswerTimeBed);
        }
        else
        {
            player.DoFadeInFadeOutFunction();
;           NextDay();
            player.SetDayText();
            //SetDirectionalLight(true);
            SaveGame();
            CanAskCheck();
        }
    }

    public void GetPlayerRef()
    {
        player = GameObject.Find("PlayerController").GetComponent<PlayerScript>();
        player.SetChanceText();
        player.SetDayText();
    }

    /*public void SetGameTime()
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
            SetClock(currentMinute, currentHour);
            if (currentHour >= playerNeedRestTime || currentHour < playerAwakeOnHour) //jika masuk waktu tidur
            {
                SetImageColorRGBA(clockImage, 1f, 0f, 0f, 1f);
            }
            else if(currentHour>=playerAwakeOnHour && currentHour<playerNeedRestTime)
            {
                SetImageColorRGBA(clockImage, 0f, 1f, 0f, 1f);
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
    }*/

    /*
    public void SetDirectionalLight(bool isMorning)
    {
        if(isMorning)
        {
            directionalLight.transform.rotation = Quaternion.Euler(37f, 0f, 0f);
        }
        else
        {
            directionalLight.transform.rotation = Quaternion.Euler(210f, 0f, 0f);
        }
    }*/


    /*public bool isOnActivityTime()
    {
        if(currentHour >= playerAwakeOnHour && currentHour< playerNeedRestTime)
        {
            return true;
        }
        return false;
    }*/




    void ResetQuestionRemaining()
    {
        questionRemaining = maxQuestionPerDay;
    }

    public void GetDirectionalLight()
    {
        directionalLight = GameObject.Find("Directional Light");
        if (directionalLight != null)
        {
            // Object found, do something with it
            // ...
        }
        else
        {
            // Object not found
            Debug.LogError("Directional Light object not found!");
        }
    }

    public void GetClockReference()
    {
        //minuteArrow = GameObject.Find("MinuteArrow");
        //hourArrow = GameObject.Find("HourArrow");
        if(GameObject.Find("ClockImage"))
        clockImage = GameObject.Find("ClockImage").GetComponent<Image>();
    }

    /*public void SetClock(int minute, int hour)
    {
        minuteArrow.transform.localRotation = Quaternion.Euler(0f, 180f, minute * 6);
        hourArrow.transform.localRotation = Quaternion.Euler(0f, 180f, hour * 30);
    }*/

    void SetImageColorRGBA(Image imageComponent, float r, float g, float b, float a)
    {
        Color color = new Color(r, g, b, a);
        imageComponent.color = color;
    }

    public void GetBedScript()
    {
        bedScript = GameObject.Find("BedScripted").GetComponent<BedScript>();
    }

    public void ClearCustomer()
    {

        cafeCustomerList.Clear();
        canteenCustomerList.Clear();
        restaurantCustomerList.Clear();
        /*Debug.Log(cafeCustomerList.Count);
        Debug.Log(canteenCustomerList.Count);
        Debug.Log(restaurantCustomerList.Count);*/
        Debug.Log("ClearCustomer Jalan");
    }

    public bool CanAskCheck()
    {
        if (questionRemaining <= 0)
        {
            //SetDirectionalLight(false);
            player.ControllerVibrate(false);
            player.ChangeClockColorToRed();
        }
        else
        {
            //SetDirectionalLight(true);
            player.ChangeClockColorToGreen();
        }
        return questionRemaining > 0;
    }

    IEnumerator VibrateControllerMultipleTimes()
    {
        player.ControllerVibrate(false);
        yield return new WaitForSeconds(1);
        player.ControllerVibrate(false);
        yield return new WaitForSeconds(1);
    }


    public void UseAskChance()
    {
        questionRemaining = Mathf.Clamp(questionRemaining - 1, 0, maxQuestionPerDay);
        CanAskCheck();
        player.SetChanceText();
        SaveGame();
    }

    public void NextDay()
    {
        BoardActivityUI board = GameObject.Find("BoardActivityUI").GetComponent<BoardActivityUI>();
        if (board != null)
        {
            board.jobFinishGO.SetActive(false);
            board.boardActivityUI.SetActive(true);
        }
        else
        {
            Debug.LogError("here");
            return;
        }
        currentDay += 1;
        ResetQuestionRemaining();
        player.SetChanceText();
        SaveGame();
    }

    private void NPCRandomizer(int maxNPC)
    {
        List<GameObject> MaleNPCListCopy = new List<GameObject>(maleNpcList);
        List<GameObject> FemaleNPCListCopy = new List<GameObject>(femaleNpcList);
        for (int i = 0; i < maxNPC; i++)
        {
            int randomGender = UnityEngine.Random.Range(0, 2);
            if (randomGender == 0)
            {
                int randomNPCIndex = UnityEngine.Random.Range(0, MaleNPCListCopy.Count);
                customerList[i] = MaleNPCListCopy[randomNPCIndex];
                MaleNPCListCopy.RemoveAt(randomNPCIndex);
            }
            else
            {
                int randomNPCIndex = UnityEngine.Random.Range(0, FemaleNPCListCopy.Count);
                customerList[i] = FemaleNPCListCopy[randomNPCIndex];
                FemaleNPCListCopy.RemoveAt(randomNPCIndex);
            }
        }
    }





}
