using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using BNG;

public class People : MonoBehaviour
{

    public int index;

    public CustomerDataSO customerData;
    public int questionIndex = 0;
    public TextMeshProUGUI jawabanText;
    [HideInInspector] public PlayerScript player;
    [SerializeField] GameObject QuestionCanvasParent;
    public GameObject UIPertanyaan;
    [SerializeField] private GameObject NameCanvas;
    [SerializeField] float AnsweringTimesInSecond = 3;
    bool isPlayerInRange = false;
    public TextMeshProUGUI nameTextObj;
    private TextMeshProUGUI nameText;
    public List<string> reason = new List<string>();
    public GameObject nameQuestionCanvas;
    private GameObject[] button = new GameObject[4];
    private GameObject tandaSeru;
    private Transform customerHead;
    private GameObject playerObj;
    private Transform initialHeadTransform;
    [HideInInspector] public GameObject UIJawaban;

    // Start is called before the first frame update

    private void Awake()
    {
        nameText = nameTextObj.GetComponent<TextMeshProUGUI>();
        customerHead = transform.Find("hips/Root/Spine1/Spine2/Chest/Neck/Head");
        playerObj = GameObject.Find("PlayerController");
        player = playerObj.GetComponent<PlayerScript>();
        UIJawaban = GameObject.Find("UI Jawaban");
        UIJawaban.SetActive(false);
        initialHeadTransform = customerHead.transform;
    }

    private void Start()
    {
        tandaSeru = transform.Find("hips/Root/Spine1/Spine2/Chest/Neck/Head/TandaSeruParent").gameObject;
        Debug.Log("nih tandaserunya ->" + tandaSeru);
        QuestionCanvasParent.SetActive(false);
        NameCanvas.SetActive(false);

        if (GameManager.Instance.peopleMet.Count != 0)
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
        EnableTandaSeru(true);
    }

    private void Update()
    {
        RotateHeadToPlayer();
    }

    public void RotateHeadToPlayer()
    {
        if (isPlayerInRange)
        {
            if (playerObj != null)
            {
                Vector3 lookDirection = playerObj.transform.position - customerHead.position;
                //lookDirection.y = 0; // Optionally, can set the Y component to zero to ensure the character looks horizontally.
                customerHead.transform.forward = lookDirection.normalized;
            }
        }
        else
        {

            customerHead.transform.rotation = initialHeadTransform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerScript>())
        {
            if (GameManager.Instance.peopleMet.Count > 0)
            {
                foreach (GameObject go in GameManager.Instance.peopleMet)
                {
                    go.transform.GetChild(0).GetComponent<People>().customerData.met = true;
                }
            }
            AskUIQuestion();
            Debug.Log("Ontrigger enter jalan");
            isPlayerInRange = true;
            EnableTandaSeru(false);
        }
    }

    public void EnableTandaSeru(bool tempBoolean)
    {
        Debug.Log("EnableTandaSeru Jalan ->" + tempBoolean);
        if (tempBoolean == false)
        {
            tandaSeru.SetActive(false);
        }
        else
        {
            if (GameManager.Instance.questionRemaining>0) // diganti karena kalo pakai CanAskCheck() itu ngetrigger banyak function lain, sedangkan yang kita butuhkan cuma boolean
            {
                tandaSeru.SetActive(true);
            }
            else
            {
                tandaSeru.SetActive(false);
            }
        }
    }

    public void AskUIQuestion()
    {
        if (GameManager.Instance.CanAskCheck())
        {
            if (customerData.met)
            {

                Debug.Log("Udah Nanya Nama");
                //Debug.Log(customerData.met);
                //Debug.Log(QuestionCanvas.transform.GetChild(0).gameObject.name);
                QuestionCanvasParent.SetActive(true);
                NameCanvas.SetActive(true);
                nameQuestionCanvas.SetActive(false);
                UIPertanyaan.SetActive(true);
            }
            else if (customerData.met == false)
            {
                Debug.Log("belum nanya nama");
                QuestionCanvasParent.SetActive(true);
                NameCanvas.SetActive(true);
                nameQuestionCanvas.SetActive(true);
                UIPertanyaan.SetActive(false);
            }
        }
        else if (!GameManager.Instance.CanAskCheck())
        {
            int randomIndex = Random.Range(0, reason.Count);
            jawabanText.text = reason[randomIndex];
            UIJawaban.SetActive(true);
        }

        for (int i = 0; i < 4; i++)
        {
            //button[i] = transform.GetChild(0).GetChild(0).GetChild(i + 1).gameObject; //codingan ricat
            Debug.Log("i nya ke -> " + i);
            button[i] = transform.Find("QuestionCanvasParent/UI Pertanyaan").GetChild(i + 4).gameObject;
            button[i].GetComponent<Questions>().index = GameManager.Instance.RandomizedType[i];
            button[i].GetComponentInChildren<TextMeshProUGUI>().text = GameManager.Instance.RandomizedQuestion[i];
        }
    }

    public void Reply()
    {
        
        if (GameManager.Instance.peopleMet.Count > 0 && GameManager.Instance.interviewCount<3)
        {
            foreach(GameObject go in GameManager.Instance.peopleMet)
            {
                if (go.transform.GetChild(0).GetComponent<People>().customerData.name == customerData.name)
                    break;
            }
        }

        if (customerData.CalculateLikeness(questionIndex) == 0)
        {
            jawabanText.text = "Saya Kurang Suka";
            UIJawaban.SetActive(true);
        }

        if (customerData.CalculateLikeness(questionIndex) == 1)
        {
            jawabanText.text = "Saya suka kok";
            UIJawaban.SetActive(true);
        }
            

        if (customerData.CalculateLikeness(questionIndex) > 1)
        {
            jawabanText.text = "Saya Sangat Suka";
            UIJawaban.SetActive(true);
        }
            

        if (customerData.CalculateLikeness(questionIndex) < 0)
        {
            jawabanText.text = "Saya Tidak Suka";
            UIJawaban.SetActive(true);
        }
            

        GameManager.Instance.CanAskCheck();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerScript>())
        {
            NameCanvas.SetActive(false);
            QuestionCanvasParent.SetActive(false);
            isPlayerInRange = false;
            UIJawaban.SetActive(false);
            jawabanText.text = "";
            EnableTandaSeru(true);
        }
        if (GameManager.Instance.peopleMet.Count > 0)
        {
            foreach (GameObject go in GameManager.Instance.peopleMet)
            {
                go.transform.GetChild(0).GetComponent<People>().customerData.met = false;
            }
        }
    }


    public void AnswerSelected()
    {
        QuestionCanvasParent.SetActive(false);
        NameCanvas.SetActive(false);
        Debug.Log(player);
        player.PlayerAsk();
        StartCoroutine(DelaySetActiveUI(AnsweringTimesInSecond));
    }

    public void AnswerNameSelected()
    {
        NameCanvas.SetActive(true);
        nameQuestionCanvas.SetActive(false);
        QuestionCanvasParent.SetActive(false);
        StartCoroutine(DelayAnswerNameTimeInSecond(AnsweringTimesInSecond));
    }

    IEnumerator DelayAnswerNameTimeInSecond(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (player != null && GameManager.Instance.CanAskCheck() && isPlayerInRange)
        {
            Debug.Log("masuk DelayAnswerNameTimeInSecond()");
            QuestionCanvasParent.SetActive(true);
            UIPertanyaan.SetActive(true);
        }
    }

    public void DelaySetActiveUIFunction()
    {
        StartCoroutine(DelaySetActiveUI(AnsweringTimesInSecond));
    }

    IEnumerator DelaySetActiveUI(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        jawabanText.text = "";
        UIJawaban.SetActive(false);
        if (player != null && GameManager.Instance.CanAskCheck() && isPlayerInRange)
        {
            QuestionCanvasParent.SetActive(true);
            NameCanvas.SetActive(true);
        }
        else
        {
            NameCanvas.SetActive(false);
        }
    }

}