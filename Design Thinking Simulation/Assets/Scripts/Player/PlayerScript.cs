using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerAsk()
    {
        gameManager.questionRemaining = Mathf.Clamp(gameManager.questionRemaining-1, 0, gameManager.maxQuestionPerDay);
    }

    public bool CanAskCheck()
    {
        return gameManager.questionRemaining > 0;
    }
}
