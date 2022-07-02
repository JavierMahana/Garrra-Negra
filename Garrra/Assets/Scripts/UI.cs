using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI instance { get; private set; }

    public enum PanelType
    {
        Instruction,
        Objective,
        Confirmation,
        Configuration,
        MainMenuPanel
        
    }

    [SerializeField]
    Canvas
        UI_Cavnas;

    [SerializeField]
    GameObject
        InstructionPanel,
        ObjectivePanel,
        ConfirmationPanel,
        ConfigurationPanel,
        MainMenuPanel;

    [SerializeField]
    Image
        Image;
    [SerializeField]
    Sprite[]
        images;
    
    [SerializeField]
    Text
        InstructionText,
        ObjectiveText;

    [SerializeField]
    Dropdown
        Resolutions;
    public HealthBar healthbar;

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

    private void Update()
    {
        if (!SceneController.instance.InMainMenu() && Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (MainMenuPanel.activeSelf!=true) ShowPanel(PanelType.MainMenuPanel, true);
            else ShowPanel(PanelType.MainMenuPanel, false);
        }
    }

    public bool ImageUI()
    {
        if (Image.gameObject.activeSelf) return true;
        return false;
    }

    public void NextImage()
    {
        for(int i = 0; i < images.Length; i++)
        {
            if (images[i] == Image)
            {
                Image.sprite = images[i++];
                break;
            }
        }
    }

    public void ShowTutorialImage(bool show)
    {
        Image.gameObject.SetActive(show);
    }

    public void ShowPanel(PanelType panel, bool show)
    {   
        switch(panel)
        {
            case PanelType.Instruction:
                InstructionPanel.SetActive(show);
                break;
            case PanelType.Objective:
                ObjectivePanel.SetActive(show);
                break;
            case PanelType.Confirmation:
                ConfirmationPanel.SetActive(show);
                break;
            case PanelType.Configuration:
                ConfigurationPanel.SetActive(show);
                break;
            case PanelType.MainMenuPanel:
                if (ConfigurationPanel.activeSelf) ConfigurationPanel.SetActive(false);
                MainMenuPanel.SetActive(show);
                int time = (show == true) ? 0 : 1;
                Time.timeScale = time;
                Debug.Log("TimeScale: "+time);
                break;
        }
    }

    public void SetPanelText(PanelType panel, string text = "", bool clear = false)
    {
        text = text.Replace("(line)", "\n");
        if (text != "") ShowPanel(panel, true);

        switch (panel)
        {
            case PanelType.Instruction:
                if (InstructionPanel) InstructionText.text = text;
                break;

            case PanelType.Objective:
                if (ObjectivePanel) ObjectiveText.text = text;
                break;
           
        }
        // si se limpia un panel tambien se oculta
        if (clear) ShowPanel(panel, !clear);
    }

    public void HideConfirmation() { ConfirmationPanel.SetActive(false); }

    public void ConfirmationNextTutorial2()
    {
        SceneController.instance.toTutorial2();
    }

    public void ShowMainMenu(bool show)
    {
        ShowPanel(PanelType.MainMenuPanel, show);
    }
    public void MenuPlay()
    {
        if (SceneController.instance.InMainMenu()) { SceneController.instance.toTutorial1(); }
        ShowPanel(PanelType.MainMenuPanel, false);
    }

    public void MenuExit()
    {
        SceneController.instance.ExitGame();
    }

    public void ShowConfiguration()
    {
        if(ConfigurationPanel.activeSelf) ShowPanel(PanelType.Configuration, false);
        else { ShowPanel(PanelType.Configuration, true); }

    }

    public void SetFullScreen(bool set)
    {
        Screen.fullScreen = set;       
    }

    public void SetResolution()
    {
        string resolution = Resolutions.options[Resolutions.value].text;
        Debug.Log(resolution);
        string[] size = resolution.Split(char.Parse("x"));

        int width = int.Parse(size[0]);
        int height = int.Parse(size[1]);

        Debug.Log($"Resoliution: width:{width} height:{height}");

        Screen.SetResolution(width, height, true);
    }
}
