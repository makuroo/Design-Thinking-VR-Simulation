using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class People : MonoBehaviour
{
    [SerializeField] private string peopleName;
    [SerializeField] private CakeSO[] cakePreference;
    public int questionIndex = 0;
    [SerializeField] private Text textField;
    [HideInInspector] public PlayerScript player;
    [SerializeField] private GameObject QuestionCanvas;
    [SerializeField] private GameObject NameCanvas;
    [SerializeField] float DelayActiveUI = 3;
    bool isPlayerInRange = false;
    public Text nameTextObj;
    private Text nameText;
    // Start is called before the first frame update

    private void Awake()
    {
        QuestionCanvas.SetActive(false);
        NameCanvas.SetActive(false);
        nameText = nameTextObj.GetComponent<Text>();
    }

    private void Start()
    {
        nameText.text = peopleName;
    }

    public void CalculateLikeness()
    {
        int likeness = 0;
        for(int i =0; i<cakePreference.Length; i++)
        {
            if (i == cakePreference.Length - 1)
            {
                likeness -= cakePreference[i].taste[questionIndex - 1];
            }
            else
            {
                likeness += cakePreference[i].taste[questionIndex - 1];
            }

        }

        if (likeness == 0)
            textField.text = "Neutral";

        if (likeness == 1)
            textField.text = "Like";

        if (likeness > 1)
            textField.text = "Really Like";

        if (likeness < 0)
            textField.text = "Dislike";
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerScript>())
        {
            player = other.GetComponent<PlayerScript>();
            if(player.CanAskCheck())
            {
                QuestionCanvas.SetActive(true);
                NameCanvas.SetActive(true);
            }
            else if(!player.CanAskCheck())
            {
                textField.text = "Aku ingin makan dulu, Jangan ganggu";
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
        StartCoroutine(DelaySetActiveUI(DelayActiveUI));
    }

    IEnumerator DelaySetActiveUI(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if(player!= null)
        {
            if(player.CanAskCheck() && isPlayerInRange)
            {
                QuestionCanvas.SetActive(true);
                NameCanvas.SetActive(true);
            }
        }
    }


    
}
