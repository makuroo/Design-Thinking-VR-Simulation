using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPersonHistoryUI : MonoBehaviour
{
    bool historyCanvasOn = false;
    bool viewHistoryCanvasOn = false;
    // Start is called before the first frame update
    [SerializeField] Canvas viewHistoryCanvas;
    [SerializeField] Canvas historyCanvas;

    public void ViewHistory()
    {
        viewHistoryCanvas.gameObject.SetActive(false);
        viewHistoryCanvasOn = false;
        historyCanvas.gameObject.SetActive(true);
        historyCanvasOn = true;
    }

    public void Close()
    {
        viewHistoryCanvas.gameObject.SetActive(true);
        viewHistoryCanvasOn = true;
        historyCanvas.gameObject.SetActive(false);
        historyCanvasOn = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (viewHistoryCanvasOn)
                viewHistoryCanvas.gameObject.SetActive(true);

            if (historyCanvasOn)
                historyCanvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            viewHistoryCanvas.gameObject.SetActive(false);
            viewHistoryCanvas.gameObject.SetActive(false);
        }
    }
}
