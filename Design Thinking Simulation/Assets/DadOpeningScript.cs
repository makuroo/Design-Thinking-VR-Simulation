using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.SceneManagement;

public class DadOpeningScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform dadGoal;
    Animator anim;
    NavMeshAgent agent;
    float distanceToGoal;

    float toleranceGoalAchieve = 1f;
    TextMeshProUGUI chatDad;
    PlayerScript playerScript;

    bool isDoneTalking;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        chatDad = GameObject.Find("ChatDad").GetComponent<TextMeshProUGUI>();
        playerScript = GameObject.Find("PlayerController").GetComponent<PlayerScript>();
        dadGoal = GameObject.Find("DadGoal").transform;
        StartCoroutine(SetDadChat());
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

    IEnumerator SetDadChat()
    {
        chatDad.text = "";
        yield return new WaitForSeconds(1f);
        ChangeWeightAnimationHand(.5f);
        chatDad.text = "Terkadang, anakku, hidup memberi kita kesempatan untuk berkreasi.";
        yield return new WaitForSeconds(5f);
        ChangeWeightAnimationHand(0);
        chatDad.text = "";
        yield return new WaitForSeconds(.5f);
        ChangeWeightAnimationHand(.5f);
        chatDad.text = "Aku yakin, membuka toko kue adalah langkah besar, dan aku mendukungmu sepenuhnya.";
        yield return new WaitForSeconds(7f);
        ChangeWeightAnimationHand(0);
        chatDad.text = "";
        yield return new WaitForSeconds(.5f);
        ChangeWeightAnimationHand(.5f);
        chatDad.text = "Semoga sukses, Nak.";
        yield return new WaitForSeconds(3f);
        ChangeWeightAnimationHand(0);
        chatDad.text = "";
        yield return new WaitForSeconds(1f);
        isDoneTalking = true;
        yield return new WaitForSeconds(1f);
        playerScript.DoFadeIn();
    }
}
