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
        damage,
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

    void Awake()
    {
        if (HideTriggerSprite) spriteRenderer.enabled = false;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(OnStay)
        {
            #region Trigger Effects
            switch (effect)
            {
                case Effect.slow:
                    break;
            }
            #endregion

            if (collision.gameObject.CompareTag("Player"))
            {
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
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(OnEnter && collision.gameObject.CompareTag("Player"))
        {
            #region Trigger Effects
            switch (effect)
            {
                case Effect.damage:
                    break;
                case Effect.heal:
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
        foreach (GameObject go in Activates) go.SetActive(true);
    }

    void Deactivate()
    {
        foreach (GameObject go in Activates) go.SetActive(false);
    }

    void Check()
    {
        if (Checks.Length < 0) Activate(); Deactivate();
        return;
    }


}
