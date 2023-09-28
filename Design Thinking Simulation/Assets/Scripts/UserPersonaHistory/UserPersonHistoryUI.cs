using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPersonHistoryUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Canvas viewHistoryCanvas;
    [SerializeField] Canvas historyCanvas;

    public void ViewHistory()
    {
        viewHistoryCanvas.gameObject.SetActive(false);
        historyCanvas.gameObject.SetActive(true);
    }

    public void Close()
    {
        viewHistoryCanvas.gameObject.SetActive(true);
        historyCanvas.gameObject.SetActive(false);
    }
}
