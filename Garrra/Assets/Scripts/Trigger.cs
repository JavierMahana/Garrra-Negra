using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Esta clase tiene data que se envía a la interfaz o bien al GameManager
/// o bien afecta al jugador
/// </summary>
public class Trigger : MonoBehaviour
{
    public enum Effect
    {
        none,
        lethal,
        heal,
        slow
    }

    public enum Function
    {
        none,
        check,
        deactivate,
        activate,
    }

    public enum Goal
    {
        None,
        Start,
        SavePoint,
        Tutorial1,
        Tutorial2
    }

    [Header("Trigger Type")]
    [SerializeField]
    bool OnEnter;
    [SerializeField]
    bool OnExit;
    [SerializeField]
    bool OnStay;

    [Space(10)]

    [SerializeField]
    GameObject[] Activates;
    [SerializeField]
    GameObject[] Deactivates;
    [SerializeField]
    GameObject[] Checks;

    [Header("UI")]
    [SerializeField]
    string instructionText;
    [SerializeField]
    string objectiveText;
    [SerializeField]
    bool ClearInstructions;
    [SerializeField]
    bool ClearObjective;

    [Header("Effect and Function Type")]
    [SerializeField]
    Effect effect;
    [SerializeField]
    Function function;
    [SerializeField]
    Goal goal;

    [Header("Settings")]
    [SerializeField]
    bool DoOnce;
    [SerializeField]
    bool HideTriggerSprite;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [Space(10)]
    [SerializeField]
    Transform TeleportTo;

    PlayerMovement playermov;

    void Awake()
    {
        if (HideTriggerSprite) spriteRenderer.enabled = false;
        if (goal != Goal.None ) DoOnce = false;
        playermov = FindObjectOfType<PlayerMovement>();
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        if (OnStay)
        {
 
            if (collision.gameObject.CompareTag("Player"))
            {

                #region Trigger -> UI
                if (instructionText != "" || ClearInstructions)
                {
                    UI.instance.SetPanelText(UI.PanelType.Instruction, instructionText, ClearInstructions);
                }

                if (objectiveText != "" || ClearObjective)
                {
                    UI.instance.SetPanelText(UI.PanelType.Objective, objectiveText, ClearObjective);
                }
                #endregion

                #region Trigger Function
                switch (function)
                {
                    case Function.check:
                        Check();
                        break;
                }
                #endregion
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (OnExit && collision.gameObject.CompareTag("Player"))
        {
            // el slow se debe revertir al salir
            #region Trigger Effects
            switch (effect)
            {
                case Effect.slow:
                    playermov.moveSpeed = playermov.defaultSpeed;
                    break;
            }
            #endregion

            #region Trigger -> UI
            if (instructionText != "" || ClearInstructions)
            {
                UI.instance.SetPanelText(UI.PanelType.Instruction, instructionText, ClearInstructions);
            }

            if (objectiveText != "" || ClearObjective)
            {
                UI.instance.SetPanelText(UI.PanelType.Objective, objectiveText, ClearObjective);
            }
            #endregion

            #region Trigger Function
            switch (function)
            {
                case Function.activate:
                    Activate();
                    break;
                case Function.deactivate:
                    Deactivate();
                    break;
                case Function.check:
                    Check();
                    break;
            }
            #endregion

            if (DoOnce) Destroy(gameObject);
        }

        if (OnStay && collision.gameObject.CompareTag("Player") && effect == Effect.slow) Debug.Log("Leaving Slow");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (OnEnter && collision.gameObject.CompareTag("Player"))
        {
            UI ui;

            #region Goal
            switch (goal)
            {
                
                case Goal.Start:
                    GameManager.instance.OnStart = true;
                    break;
                case Goal.SavePoint:
                    // Set current region as save point
                    Debug.Log("New Save Point Set -> " + gameObject.name);
                    GameManager.instance.SavePoint = gameObject.transform.position;
                    GameManager.instance.OnStart = false;
                    break;
                case Goal.Tutorial1:
                    ui = UI.instance;
                    UI.instance.SetPanelText(UI.PanelType.Image, ui.ImagesText[ui.currentImage]);
                    SceneController.instance.toExploration();
                    break;
                    
                case Goal.Tutorial2:
                    ui = UI.instance;
                    UI.instance.SetPanelText(UI.PanelType.Image, ui.ImagesText[ui.currentImage]);
                    Debug.Log("JuegoCompleto!!!");
                    break;
                    // Set Scene change to the explorationMap and small + image for story telling (UI)
                    // Here use a gamemanager int to determine to which level the scene changer 
                    // should go to next.
                    // ---> UI.instance.ShowTutorialImage(true);

            }
            #endregion

            #region Trigger Effects
            switch (effect)
            {
                case Effect.lethal: UI.instance.healthbar.TakeDamage(UI.instance.healthbar.maxHealth);
                    Debug.Log("Lethal Damage");
                    break;
                case Effect.heal: Debug.Log("Heal");
                    break;
                case Effect.slow:
                    playermov.moveSpeed = playermov.slowSpeed; 
                    Debug.Log("Slow");
                    break;
                   
            }
            #endregion

            #region Trigger -> UI
            if (instructionText != "" || ClearInstructions)
            {
                UI.instance.SetPanelText(UI.PanelType.Instruction, instructionText, ClearInstructions);
            }

            if (objectiveText != "" || ClearObjective)
            {
                UI.instance.SetPanelText(UI.PanelType.Objective, objectiveText, ClearObjective);
            }
            #endregion

            #region Trigger Function
            switch (function)
            {
                case Function.activate:
                    Activate();
                    break;
                case Function.deactivate:
                    Deactivate();
                    break;
                case Function.check:
                    Check();
                    break;
            }
            #endregion

            if (TeleportTo) collision.gameObject.transform.position = TeleportTo.transform.position;

            if (DoOnce) Destroy(gameObject);
        }
    }

    void Activate()
    {
        foreach (GameObject go in Activates)
        {
            if(go) go.SetActive(true);
        }
    }

    void Deactivate()
    {
        foreach (GameObject go in Deactivates)
        {
            if (go) go.SetActive(false);
        }
    }

    void Check()
    {
        int objects = 0;
        foreach (GameObject go in Checks)
        {
            if (go) objects++;
        }
        if (objects == 0)
        {
            Activate(); 
            Deactivate(); 
            Destroy(gameObject);
        }
    }

}