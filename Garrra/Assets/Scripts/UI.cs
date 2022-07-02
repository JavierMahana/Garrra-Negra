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
        Objective
    }

    [SerializeField]
    Canvas
        UI_Cavnas;

    [SerializeField]
    GameObject
        InstructionPanel,
        ObjectivePanel;

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


    void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else { instance = this; }
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
}
