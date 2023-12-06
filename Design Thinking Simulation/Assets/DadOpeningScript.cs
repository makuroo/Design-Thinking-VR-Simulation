using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.SceneManagement;

public class DadOpeningScript : MonoBehaviour
{
    // Start is called before the first frame update

    Transform dadGoal;
    Animator anim;
    NavMeshAgent agent;
    float distanceToGoal;

    float toleranceGoalAchieve = 1f;
    TextMeshProUGUI chatDad;
    PlayerScript playerScript;
    GameObject chatBox;
    int indexChat = 0;

    AudioSource audioSource;
    GameObject buttonUI;


    bool isDoneTalking;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        chatDad = GameObject.Find("ChatDad").GetComponent<TextMeshProUGUI>();
        playerScript = GameObject.Find("PlayerController").GetComponent<PlayerScript>();
        dadGoal = GameObject.Find("DadGoal").transform;
        buttonUI = GameObject.Find("ButtonLanjutkan");
        buttonUI.SetActive(false);
        chatBox = GameObject.Find("ChatBox");
        audioSource = GetComponent<AudioSource>();
        SetActiveChatBox(false);
        StartCoroutine(SetDadChat(indexChat));
    }

    // Update is called once per frame
    void Update()
    {
        if(isDoneTalking)
        {
            MoveTo();
        }
    }

    public void ChangeWeightAnimationHand(float value)
    {
        anim.SetLayerWeight(1, value);
    }

    public void ChangeWeightAnimationDisappointed(float value)
    {
        anim.SetLayerWeight(2, value);
    }

    public void MoveTo()
    {
        distanceToGoal = Vector3.Distance(this.gameObject.transform.position, dadGoal.position);
        if (distanceToGoal > toleranceGoalAchieve)
        {
            if (dadGoal)
            {
                agent.SetDestination(dadGoal.position);
                anim.SetBool("walk", true);
            }
        }
        else
        {
            SceneManager.LoadSceneAsync("Home");
            Destroy(this.gameObject);
        }
    }

    public void SetActiveChatBox(bool isActive)
    {
        chatBox.SetActive(isActive);
        //sound pop
    }


    IEnumerator SetDadChat(int indexChat)
    {
        if(indexChat == 0)
        {
            chatDad.text = "";
            yield return new WaitForSeconds(1f);
            ChangeWeightAnimationHand(1f);
            chatDad.text = "Terkadang, anakku, hidup memberi kita kesempatan untuk berkreasi.";
            SetActiveChatBox(true);
            yield return new WaitForSeconds(5f);
            ChangeWeightAnimationHand(0);
            buttonUI.SetActive(true);
        }

        else if(indexChat == 1)
        {
            yield return new WaitForSeconds(.5f);
            ChangeWeightAnimationHand(1f);
            chatDad.text = "Aku yakin, membuka toko kue adalah langkah besar, dan aku mendukungmu sepenuhnya.";
            SetActiveChatBox(true);
            yield return new WaitForSeconds(7f);
            ChangeWeightAnimationHand(0);
            buttonUI.SetActive(true);
        }

        else if(indexChat == 2)
        {
            yield return new WaitForSeconds(.5f);
            ChangeWeightAnimationHand(1f);
            chatDad.text = "Semoga sukses, Nak.";
            SetActiveChatBox(true);
            yield return new WaitForSeconds(3f);
            ChangeWeightAnimationHand(0);
            buttonUI.SetActive(true);
        }

        else if(indexChat == 3)
        {
            yield return new WaitForSeconds(1f);
            isDoneTalking = true;
            yield return new WaitForSeconds(1f);
            playerScript.DoFadeIn();
            buttonUI.SetActive(true);
        }
    }

    public void NextChat()
    {
        buttonUI.SetActive(false);
        SetActiveChatBox(false);
        indexChat += 1;
        StartCoroutine(SetDadChat(indexChat));
    }
}
