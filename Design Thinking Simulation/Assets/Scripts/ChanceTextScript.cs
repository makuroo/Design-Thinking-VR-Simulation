using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChanceTextScript : MonoBehaviour
{
    // Start is called before the first frame update

    TextMeshProUGUI chanceTextOnCustomer;

    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        chanceTextOnCustomer = GetComponent<TextMeshProUGUI>();
        chanceTextOnCustomer.text = GameManager.Instance.questionRemaining.ToString();
    }
}
