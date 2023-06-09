using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class People : MonoBehaviour
{
    [SerializeField] private string peopleName;
    [SerializeField] private CakePreferencesSO cakePreferences;
    public  EmpathyMapSO customerEmpathy;
    public int questionIndex = 0;
    [SerializeField] private Text textField;
    [HideInInspector] public PlayerScript player;
    [SerializeField] private GameObject QuestionCanvas;
    [SerializeField] float delayActiveUI = 3;
    bool isPlayerInRange = false;
    private List<string> reason = new List<string>();
    // Start is called before the first frame update

    private void Awake()
    {
        
    }

    private void Start()
    {
        QuestionCanvas.SetActive(false);

    }

    public void CalculateLikeness()
    {
        
        int likeCake = 0;
        int dislikeCake = 0;
        for (int i = 0; i < cakePreferences.LikeCake.Count; i++) 
        { 

            likeCake += cakePreferences.LikeCake[i].taste[questionIndex - 1];
        }
        
        for (int i = 0; i < cakePreferences.DislikeCake.Count; i++) 
        { 

            dislikeCake += cakePreferences.DislikeCake[i].taste[questionIndex - 1];
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
            if(player.CanAskCheck())
            {
                QuestionCanvas.SetActive(true);
            }
            else if(!player.CanAskCheck())
            {
                int randomIndex = Random.Range(0, reason.Count);
                //textField.text = reason[randomIndex];
            }
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerScript>())
        {
            player = other.GetComponent<PlayerScript>();
            QuestionCanvas.SetActive(false);
            isPlayerInRange = false;
            textField.text = "";
        }
    }

    public void AnswerSelected()
    {
        QuestionCanvas.SetActive(false);
        player.PlayerAsk();
        StartCoroutine(DelaySetActiveUI(delayActiveUI));
    }

    IEnumerator DelaySetActiveUI(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if(player!= null && player.CanAskCheck() && isPlayerInRange)
        {
            QuestionCanvas.SetActive(true);
        }
    }

}
