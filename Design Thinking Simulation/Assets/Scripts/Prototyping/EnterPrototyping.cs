using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterPrototyping : MonoBehaviour
{
    public GameObject prototypeUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && GameManager.Instance.hasDoneVPC)
        {
            prototypeUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            prototypeUI.SetActive(false);
        }
    }

    public void PrototypeYesClicked()
    {
        GameManager.Instance.canDoActivity = false;
        SceneManager.LoadScene("Home Prototyping");
    }
}
