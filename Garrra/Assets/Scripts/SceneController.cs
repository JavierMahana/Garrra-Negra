using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance { get; private set; }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void toExploration()
    {
        GameManager.instance.OnStart = true;
        SceneManager.LoadScene("Exploracion");
    }
    public void toCombat()
    {
        SceneManager.LoadScene("Combate");
    }
    public void toTutorial1()
    {
        GameManager.instance.OnStart = true;
        SceneManager.LoadScene("Tutorial01");
    }
    public void toTutorial2()
    {
        GameManager.instance.OnStart = true;
        SceneManager.LoadScene("Tutorial02");
    }
    public void toMenu()
    {
        GameManager.instance.OnStart = true;
        SceneManager.LoadScene("Main Menu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Restart()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public bool InMainMenu()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu") return true;
        return false;
    }
}
