using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageToggler : MonoBehaviour
{
    [SerializeField]
    private Color enabledColor = Color.magenta;

    private MenuNode node;

    public void ToggleCage()
    {
        GlobalSettingsManager.Instance.CageMode = !GlobalSettingsManager.Instance.CageMode;
    }

    void Start()
    {
        node = GetComponent<MenuNode>();

        GlobalSettingsManager.Instance.OnCageChange += HandleCageChange;
        HandleCageChange();
    }

    void OnDestroy()
    {
        GlobalSettingsManager.Instance.OnCageChange -= HandleCageChange;
    }

    private void HandleCageChange()
    {
        bool value = GlobalSettingsManager.Instance.CageMode;
        if (value)
            node.OutlineColor = enabledColor;
        else
            node.ResetColor();
    }
}
