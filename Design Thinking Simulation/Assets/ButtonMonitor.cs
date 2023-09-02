using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMonitor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        GameManager.Instance.NewGame();
        SceneManager.LoadSceneAsync("Home");
    }

    public void LoadGame()
    {
        GameManager.Instance.LoadGame();
        SceneManager.LoadSceneAsync("Home");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
