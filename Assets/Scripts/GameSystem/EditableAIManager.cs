using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditableAIManager : Singleton<EditableAIManager>
{
    public event Action<EditableAI> OnAIClicked = null;
    
    public void RegisterAI(EditableAI _ai)
    {
        _ai.OnClick += BroadcastAI;
    }

    void BroadcastAI(EditableAI _ai)
    {
        OnAIClicked?.Invoke(_ai);
    }
}
