using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionUIScript : MonoBehaviour
{
    [SerializeField] float DelayActiveUI = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnswerSelected()
    {
        this.gameObject.SetActive(false);
        StartCoroutine(DelaySetActiveUI(DelayActiveUI));
    }

    IEnumerator DelaySetActiveUI(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
        this.gameObject.SetActive(true);
    }
}
