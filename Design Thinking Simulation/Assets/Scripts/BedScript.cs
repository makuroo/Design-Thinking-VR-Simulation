using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gameManager;
    GameObject canvasBed;
    GameObject canvasBedAnswer;


    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        canvasBed = GameObject.Find("CanvasBed");
        canvasBedAnswer = GameObject.Find("CanvasBedAnswer");
        GameManager.Instance.GetBedScript();
    }

    void Start()
    {
        canvasBed.SetActive(false);
        canvasBedAnswer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerScript>())
        {
            canvasBed.SetActive(true);
            canvasBedAnswer.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerScript>())
        {
            canvasBed.SetActive(false);
            canvasBedAnswer.SetActive(false);
        }
    }

    public void ActivateBedAnswer(float seconds)
    {
        StartCoroutine(ActivateBedAnswerDelay(seconds));
    }


    IEnumerator ActivateBedAnswerDelay(float seconds)
    {
        canvasBedAnswer.SetActive(true);
        yield return new WaitForSeconds(seconds);
        canvasBedAnswer.SetActive(false);
    }
}
