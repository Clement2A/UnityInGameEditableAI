using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TransitionButton : MonoBehaviour, IPointerClickHandler
{
    public event Action<State, State> OnButtonClick = null;
    public StateLinkUI link = null;
    TMP_Text transitionText = null;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnButtonClick?.Invoke(link.StateOne.LinkedState, link.StateTwo.LinkedState);
    }
}
