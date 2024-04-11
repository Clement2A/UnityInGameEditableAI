using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StateUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    public event Action<int, Vector2> OnStateMoved = null;
    public event Action<StateUI> OnStateSelected = null;

    [SerializeField] Image border = null;
    [SerializeField] TMP_Text stateName = null;
    FSMUI fsmui = null;

    public RectTransform OwnRectTransform { get; set; }
    public int StateIndex { get; set; } = 0;

    private void Awake()
    {
        OwnRectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        border.color = ColourLibrary.Instance.InactiveStateColor;
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        OnStateMoved = null;
        OnStateSelected = null;
    }

    public void OnDrag(PointerEventData _eventData)
    {
        Vector2 _newPos = OwnRectTransform.offsetMax + new Vector2(_eventData.delta.x, _eventData.delta.y) * 2;
        Vector2 _containerSize = transform.parent.GetComponent<RectTransform>().rect.size / 2;
        
        if (_newPos.x < -_containerSize.x + OwnRectTransform.rect.size.x)
        {
            _newPos.x = -_containerSize.x + OwnRectTransform.rect.size.x;
        }
        if(_newPos.x > _containerSize.x)
        {
            _newPos.x = _containerSize.x;
        }
        if(_newPos.y < -_containerSize.y + OwnRectTransform.rect.size.y)
        {
            _newPos.y = -_containerSize.y + OwnRectTransform.rect.size.y;
        }
        if(_newPos.y > _containerSize.y)
        {
            _newPos.y = _containerSize.y;
        }
        
        OwnRectTransform.offsetMax = _newPos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnStateMoved?.Invoke(StateIndex, OwnRectTransform.offsetMax * 2 - OwnRectTransform.rect.size);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnStateSelected?.Invoke(this);
        SelectState();
    }

    public void SetFSMUI(FSMUI _fsmui)
    {
        fsmui = _fsmui;
    }

    void SelectState()
    {
        border.color = ColourLibrary.Instance.SelectedStateColor;
        fsmui.OnStateSelected += UnselectState;
    }
    void UnselectState(StateUI stateUI)
    {
        border.color = ColourLibrary.Instance.InactiveStateColor;
        fsmui.OnStateSelected -= UnselectState;
    }

    public void SetName(string _stateName)
    {
        stateName.SetText(_stateName);
    }
}
