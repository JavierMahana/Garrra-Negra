using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance { get; private set; }
    void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else { instance = this; }
    }

    public void toExploration()
    {
        SceneManager.LoadScene("Exploracion");
    }
    public void toCombat()
    {
        SceneManager.LoadScene("Combate");
    }
    public void toTutorial1()
    {
        SceneManager.LoadScene("Tutorial01");
    }
    public void toTutorial2()
    {
        SceneManager.LoadScene("Tutorial02");
    }
    public void toMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
