using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DadEndingScript : MonoBehaviour
{
    // Start is called before the first frame update

    Transform dadGoal;
    Animator anim;
    NavMeshAgent agent;
    float distanceToGoal;

    float toleranceGoalAchieve = 1f;
    TextMeshProUGUI chatDad;
    PlayerScript playerScript;

    Animator playerAnim;
    TextMeshProUGUI problemStatementText;
    bool isUpdatingProblemStatementScore;
    TextMeshProUGUI VPCText;
    bool isUpdatingVPCScore;
    TextMeshProUGUI prototypingText;
    bool isUpdatingPrototypingScore;
    TextMeshProUGUI testingText;
    bool isUpdatingTestingScore;
    bool isStamping;
    RawImage scoreStampImage;
    GameObject chatBox;


    float speedAnim = .5f;
    float tempScore = 0f;

    [SerializeField] Texture2D AImage;
    [SerializeField] Texture2D AMinImage;
    [SerializeField] Texture2D BPlusImage;
    [SerializeField] Texture2D BImage;
    [SerializeField] Texture2D BMinImage;
    [SerializeField] Texture2D CImage;
    [SerializeField] Texture2D DImage;
    [SerializeField] Texture2D EImage;
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        chatDad = GameObject.Find("ChatDad").GetComponent<TextMeshProUGUI>();
        playerScript = GameObject.Find("PlayerController").GetComponent<PlayerScript>();
        dadGoal = GameObject.Find("DadGoal").transform;
        //StartCoroutine(SetDadChat());
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
        StartCoroutine(PlayAnimWithDelay());
        problemStatementText = GameObject.Find("ProblemStatementText").GetComponent<TextMeshProUGUI>();
        VPCText = GameObject.Find("ValuePropotitionText").GetComponent<TextMeshProUGUI>();
        prototypingText = GameObject.Find("PrototypingText").GetComponent<TextMeshProUGUI>();
        testingText = GameObject.Find("TestingText").GetComponent<TextMeshProUGUI>();
        scoreStampImage = GameObject.Find("ScoreStampImage").GetComponent<RawImage>();
        chatBox = GameObject.Find("ChatBox");
        chatBox.SetActive(false);
        

        GameManager.Instance.CalculateTotalScore();
        ChangeScoreStampImage();
        scoreStampImage.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isUpdatingProblemStatementScore)
        {
            AnimateProblemStatementScoreInt();
        }
        else if(isUpdatingVPCScore)
        {
            AnimateVPCScoreInt();
        }
        else if (isUpdatingPrototypingScore)
        {
            AnimatePrototypingScoreInt();
        }
        else if (isUpdatingTestingScore)
        {
            AnimateTestingScoreInt();
        }
        else if(isStamping)
        {
            StartCoroutine(PlayAnimWithDelay2());
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

    public void ChangeWeightAnimationHandClap(float value)
    {
        anim.SetLayerWeight(3, value);
    }

    public void ChangeWeightAnimationThumbsUp(float value)
    {
        anim.SetLayerWeight(4, value);
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


    IEnumerator PlayAnimWithDelay()
    {
        yield return new WaitForSeconds(1f);
        playerAnim.Play("PaperComeAnim");
        yield return new WaitForSeconds(1f);
        isUpdatingProblemStatementScore = true;
    }

    IEnumerator PlayAnimWithDelay2()
    {
        isStamping = false;
        playerAnim.Play("StampAnim");
        yield return new WaitForSeconds(2f);
        playerAnim.Play("PaperOutFrameAnim");
        yield return new WaitForSeconds(.5f);
        StartCoroutine(EndingAnim());
    }

    IEnumerator EndingAnim()
    {
        if(GameManager.Instance.totalScore < 65)
        {
            ChangeWeightAnimationDisappointed(1f);
            yield return new WaitForSeconds(1f);
            chatBox.SetActive(true);
            chatDad.text = "Tidak apa apa, Kita coba lagi ya..";
        }
        else
        {
            ChangeWeightAnimationHandClap(1f);
            yield return new WaitForSeconds(1f);
            chatBox.SetActive(true);
            chatDad.text = "Selamat atas kelulusanmu, Nak! Ayah sangat bangga padamu.";
            ChangeWeightAnimationThumbsUp(1f);

        }
        yield return new WaitForSeconds(4f);
        chatBox.SetActive(false);
        playerScript.DoFadeIn();
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }

    public void AnimateProblemStatementScoreInt()
    {
        if (tempScore + speedAnim >= GameManager.Instance.problemStatementScore)
        {
            tempScore = GameManager.Instance.problemStatementScore;
            isUpdatingProblemStatementScore = false;
            isUpdatingVPCScore = true;
            problemStatementText.text = "Problem Statement: " + Mathf.FloorToInt(tempScore).ToString();
            tempScore = 0f;
        }
        else
        {
            tempScore += speedAnim;
            problemStatementText.text = "Problem Statement: " + Mathf.FloorToInt(tempScore).ToString();
        }
    }
    public void AnimateVPCScoreInt()
    {
        if (tempScore + speedAnim >= GameManager.Instance.vpcScore)
        {
            tempScore = GameManager.Instance.vpcScore;
            isUpdatingVPCScore = false;
            isUpdatingPrototypingScore = true;
            VPCText.text = "VPC: " + Mathf.FloorToInt(tempScore).ToString();
            tempScore = 0f;
        }
        else
        {
            tempScore += speedAnim;
            VPCText.text = "VPC: " + Mathf.FloorToInt(tempScore).ToString();
        }
    }

    public void AnimatePrototypingScoreInt()
    {
        if (tempScore + speedAnim >= GameManager.Instance.prototypingScore)
        {
            tempScore = GameManager.Instance.prototypingScore;
            isUpdatingPrototypingScore = false;
            isUpdatingTestingScore = true;
            prototypingText.text = "Prototyping: " + Mathf.FloorToInt(tempScore).ToString();
            tempScore = 0f;
        }
        else
        {
            tempScore += speedAnim;
            prototypingText.text = "Prototyping: " + Mathf.FloorToInt(tempScore).ToString();
        }
    }

    public void AnimateTestingScoreInt()
    {
        if (tempScore + speedAnim >= GameManager.Instance.testingScore)
        {
            tempScore = GameManager.Instance.testingScore;
            isUpdatingTestingScore = false;
            isStamping = true;
            testingText.text = "Testing: " + Mathf.FloorToInt(tempScore).ToString();
            tempScore = 0f;
        }
        else
        {
            tempScore += speedAnim;
            testingText.text = "Testing: " + Mathf.FloorToInt(tempScore).ToString();
        }
    }

    public void ChangeScoreStampImage()
    {
        if(GameManager.Instance.totalScore >= 90)
            scoreStampImage.texture = AImage;
        else if (GameManager.Instance.totalScore >= 85)
            scoreStampImage.texture = AMinImage;
        else if(GameManager.Instance.totalScore >= 80)
            scoreStampImage.texture = BPlusImage;
        else if(GameManager.Instance.totalScore >= 75)
            scoreStampImage.texture = BImage;
        else if(GameManager.Instance.totalScore >= 70)
            scoreStampImage.texture = BMinImage;
        else if(GameManager.Instance.totalScore >= 65)
            scoreStampImage.texture = CImage;
        else if(GameManager.Instance.totalScore >= 50)
            scoreStampImage.texture = DImage;
        else scoreStampImage.texture = EImage;
    }
}
