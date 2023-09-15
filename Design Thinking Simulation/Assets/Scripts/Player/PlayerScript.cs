using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI chanceText;

    private void Awake()
    {
        
    }
    void Start()
    {
        GameManager.Instance.GetPlayerRef();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerAsk()
    {
        GameManager.Instance.UseAskChance();
    }

    public void SetDayText()
    {
        dayText.text = GameManager.Instance.currentDay.ToString();
    }

    public void SetChanceText()
    {
        chanceText.text = GameManager.Instance.questionRemaining.ToString();
    }
}
