using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject UImessage;
    public void toExploration()
    {
        SceneManager.LoadScene("Exploracion");
    }
    public void toCombat()
    {
        SceneManager.LoadScene("Combate");
    }
    public void toMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void dismissMessage()
    {
        UImessage.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
