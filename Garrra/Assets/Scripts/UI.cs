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
        MainMenuPanel,
        Image
        
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
        MainMenuPanel,
        ImagePanel;

    [SerializeField]
    Image
        Image;
    [SerializeField]
    Sprite[]
        images;

    [TextArea(3, 6)]
    public string[]
        ImagesText;

    [SerializeField]
    Text
        InstructionText,
        ObjectiveText,
        ImageText;

    [SerializeField]
    Dropdown
        Resolutions;
    public HealthBar healthbar;

    [HideInInspector]
    public int currentImage = 0;

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

        if (ImagePanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            ShowPanel(PanelType.Image, false);
            if (currentImage < 2) currentImage++;
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

    public void ShowPanel(PanelType panel, bool show)
    {
        int time;
        switch (panel)
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
                time = (show == true) ? 0 : 1;
                Time.timeScale = time;
                Debug.Log("Menu -> TimeScale: "+time);
                break;
            case PanelType.Image:
                time = (show == true) ? 0 : 1;
                Time.timeScale = time;
                ImagePanel.SetActive(show);
                Debug.Log("Image -> TimeScale: " + time);
                Image.sprite = images[currentImage];
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
            case PanelType.Image:
                if (ImagePanel) ImageText.text = text;
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
        if (SceneController.instance.InMainMenu()) 
        {
            currentImage = 0;
            SetPanelText(PanelType.Image, ImagesText[currentImage]);
            SceneController.instance.toTutorial1();  
        }
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
