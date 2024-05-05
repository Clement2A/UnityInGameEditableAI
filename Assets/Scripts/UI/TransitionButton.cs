using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TransitionButton : MonoBehaviour, IPointerClickHandler
{
    public event Action OnButtonClick = null;
    TMP_Text transitionText = null;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        OnButtonClick?.Invoke();
    }
}
