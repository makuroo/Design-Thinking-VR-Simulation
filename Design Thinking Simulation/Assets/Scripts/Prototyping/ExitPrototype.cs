using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPrototype : MonoBehaviour
{
    [SerializeField] private GameObject exitUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.hasDonePrototyping)
            exitUI.SetActive(true);

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.hasDonePrototyping)
            exitUI.SetActive(false);
    }

    public void ReturnHome()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
    }
}
