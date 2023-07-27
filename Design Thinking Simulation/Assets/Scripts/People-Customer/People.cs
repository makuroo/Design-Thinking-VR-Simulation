using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public class People : MonoBehaviour
{

    public int index;
    [SerializeField] private CakePreferencesSO cakePreferences;
    public  CustomerDataSO customerData;
    public int questionIndex = 0;
    [SerializeField] private Text textField;
    [HideInInspector] public PlayerScript player;
    [SerializeField] GameObject QuestionCanvas;
    public GameObject UIPertanyaan;
    [SerializeField] private GameObject NameCanvas;
    [SerializeField] float AnsweringTimesInSecond = 3;
    bool isPlayerInRange = false;
    public Text nameTextObj;
    private Text nameText;
    public List<string> reason = new List<string>();
    public GameObject nameQuestionCanvas;
    GameManager gameManager;
    private GameObject[] button = new GameObject[3];

    // Start is called before the first frame update

    private void Awake()
    {
        nameText = nameTextObj.GetComponent<Text>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        QuestionCanvas.SetActive(false);
        NameCanvas.SetActive(false);

        if(GameManager.Instance.peopleMet.Count != 0)
        {
            foreach (GameObject obj in GameManager.Instance.peopleMet)
            {
                if (obj.name == customerData.peopleName)
                {
                    customerData.met = true;
                    break;
                }
            }
        }


        if (customerData.met == false)
            nameText.text = "?????";
        else
            nameText.text = customerData.peopleName;
    }

    public int CalculateLikeness(int index)
    {
        int likeCake = 0;
        int dislikeCake = 0;
        for (int i = 0; i < cakePreferences.LikeCake.Count; i++) 
        { 

            likeCake += cakePreferences.LikeCake[i].taste[index];
        }
        
        for (int i = 0; i < cakePreferences.DislikeCake.Count; i++) 
        { 

            dislikeCake += cakePreferences.DislikeCake[i].taste[index];
        }

        return  likeCake - dislikeCake;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerScript>())
        {
            
            player = other.GetComponent<PlayerScript>();

            if (player.CanAskCheck() && gameManager.isOnActivityTime() && customerData.met)
            {
                QuestionCanvas.SetActive(true);
                NameCanvas.SetActive(true);
                nameQuestionCanvas.SetActive(false);

            }
            else if (player.CanAskCheck() && customerData.met == false)
            {
                QuestionCanvas.SetActive(true);
                NameCanvas.SetActive(true);
                UIPertanyaan.SetActive(false);
            }
            
            else if(!gameManager.isOnActivityTime())
            {
                textField.text = "You Need To Sleep";
            }
            else if(!player.CanAskCheck())
            {
                int randomIndex = Random.Range(0, reason.Count);
                textField.text = reason[randomIndex];
            }

            for (int i = 0; i < 3; i++)
            {
                button[i] = transform.GetChild(0).GetChild(0).GetChild(i + 1).gameObject;
                button[i].GetComponent<Questions>().index = GameManager.Instance.RandomizedType[i];
                button[i].GetComponentInChildren<Text>().text = GameManager.Instance.RandomizedQuestion[i];
            }

            isPlayerInRange = true;
        }
    }
    
    public void Reply()
    {
        if (CalculateLikeness(questionIndex) == 0)
            textField.text = "Neutral";

        if (CalculateLikeness(questionIndex) == 1)
            textField.text = "Like";

        if (CalculateLikeness(questionIndex) > 1)
            textField.text = "Really Like";

        if (CalculateLikeness(questionIndex) < 0)
            textField.text = "Dislike";
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerScript>())
        {
            NameCanvas.SetActive(false);
            player = other.GetComponent<PlayerScript>();
            QuestionCanvas.SetActive(false);
            isPlayerInRange = false;
            textField.text = "";
        }
    }


    public void AnswerSelected()
    {
        QuestionCanvas.SetActive(false);
        NameCanvas.SetActive(false);
        player.PlayerAsk();
        StartCoroutine(DelaySetActiveUI(AnsweringTimesInSecond));
    }

    IEnumerator DelaySetActiveUI(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if(player!= null && player.CanAskCheck() && isPlayerInRange)
        {
            QuestionCanvas.SetActive(true);
            NameCanvas.SetActive(true);
            textField.text = "";
        }
    }
    
}
