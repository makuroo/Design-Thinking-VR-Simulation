using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int maxQuestionPerDay;
    [SerializeField] int questionRemaining;
    void Start()
    {
        ResetQuestionRemaining();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerAsk()
    {
        questionRemaining = Mathf.Clamp(questionRemaining-1, 0, maxQuestionPerDay);
    }

    public bool CanAskCheck()
    {
        return questionRemaining > 0;
    }

    void ResetQuestionRemaining()
    {
        questionRemaining = maxQuestionPerDay;
    }
}
