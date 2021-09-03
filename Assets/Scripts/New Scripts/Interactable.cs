using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    protected Button button;
    public ActionSO action;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        if(button != null)
        button.onClick.AddListener(delegate { ExecuteAction(); });
    }


    public virtual void ExecuteAction() { 
    
    }

    public virtual void CheckRequirements() { 
    
    }

    private void OnEnable()
    {
        GameManager.Instance.updateGameState += CheckRequirements;
    }

    private void OnDisable()
    {
        GameManager.Instance.updateGameState -= CheckRequirements;
    }

    public virtual void ActivateButton()
    {
    }

    public virtual void DisableButton()
    {
    }


    public virtual void Selected()
    {
        MenuManager.Instance.DisplayDescriptionWindow(action);
        CursorScript.Instance.Hover();
    }

    public virtual void Deselected()
    {
        MenuManager.Instance.HideDescriptionWindow();
        CursorScript.Instance.ResetCursor();
    }
}
