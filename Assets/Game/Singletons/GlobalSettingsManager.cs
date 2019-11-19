using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettingsManager : Singleton<GlobalSettingsManager>
{
    public delegate void ChangeAction();
    public event ChangeAction OnCageChange;

    private bool _cageMode;
    public bool CageMode
    {
        get { return _cageMode; }
        set
        {
            _cageMode = value;
            OnCageChange?.Invoke();
        }
    }
}
