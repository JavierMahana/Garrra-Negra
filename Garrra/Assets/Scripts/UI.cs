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
    Text
        InstructionText,
        ObjectiveText;


    void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else { instance = this; }
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

    public void SetPanelText(PanelType panel, string text, bool clear = false)
    {
        if (clear) text = "";
        switch (panel)
        {
            case PanelType.Instruction:
                if (InstructionPanel) InstructionText.text = text;
                break;

            case PanelType.Objective:
                if (ObjectivePanel) ObjectiveText.text = text;
                break;
        }
    }
}
