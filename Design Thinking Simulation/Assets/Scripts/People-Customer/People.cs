using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class People : MonoBehaviour
{
    public string peopleName;
    public int index;
    [SerializeField] private CakePreferencesSO cakePreferences;
    public  EmpathyMapSO customerEmpathy;
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
    private GameObject[] button = new GameObject[3];
    public bool met = false;
    // Start is called before the first frame update

    private void Awake()
    {
        nameText = nameTextObj.GetComponent<Text>();
    }

    private void Start()
    {
        QuestionCanvas.SetActive(false);
        NameCanvas.SetActive(false);
        if (met == false)
            nameText.text = "?????";
        else
            nameText.text = peopleName;
    }

    public void CalculateLikeness()
    {
        int likeCake = 0;
        int dislikeCake = 0;
        for (int i = 0; i < cakePreferences.LikeCake.Count; i++) 
        { 

            likeCake += cakePreferences.LikeCake[i].taste[questionIndex];
        }
        
        for (int i = 0; i < cakePreferences.DislikeCake.Count; i++) 
        { 

            dislikeCake += cakePreferences.DislikeCake[i].taste[questionIndex];
        }

        int totalLikeness = likeCake - dislikeCake;

        if (totalLikeness == 0)
            textField.text = "Neutral";

        if (totalLikeness == 1)
            textField.text = "Like";

        if (totalLikeness > 1)
            textField.text = "Really Like";

        if (totalLikeness < 0)
            textField.text = "Dislike";
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerScript>())
        {
            
            player = other.GetComponent<PlayerScript>();

            //if(GameManager.Instance.peopleMet.Count == 0)
            //{
            //    nameQuestionCanvas.gameObject.SetActive(true);
            //}
            //else
            //{
            //    for(int i=0; i<GameManager.Instance.peopleMet.Count; i++)
            //    {
            //        if(gameObject.name == GameManager.Instance.peopleMet[i].name)
            //        {
            //            nameText.text = peopleName;
            //            break;
            //        }else if(i == GameManager.Instance.peopleMet.Count-1 && gameObject.name != GameManager.Instance.peopleMet[i].name)
            //        {
            //            nameQuestionCanvas.gameObject.SetActive(true);
            //            break;
            //        }
            //    }
            //}

            if (player.CanAskCheck() && met)
            {
                QuestionCanvas.SetActive(true);
                NameCanvas.SetActive(true);

            }else if (player.CanAskCheck() && met == false)
            {
                Debug.Log("aaa");
                QuestionCanvas.SetActive(true);
                NameCanvas.SetActive(true);
                UIPertanyaan.SetActive(false);
            }
            else if (!player.CanAskCheck())
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
