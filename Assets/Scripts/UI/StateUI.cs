using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StateUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action OnDeletion = null;
    public event Action<int, Vector2> OnStateMoved = null;
    public event Action<Vector2> OnStateMovedContinous = null;
    public event Action<StateUI> OnStateSelected = null;
    public event Action<StateUI> OnStateStartCreateTransition = null;
    public event Action<StateUI> OnStateEndCreateTransition = null;
    public event Action<int> OnStateDeleted = null;
    public event Action<StateUI> OnHover = null;
    public event Action OnUnhover = null;
    public State LinkedState { get; set; }

    [SerializeField] Image border = null;
    [SerializeField] TMP_Text stateName = null;
    FSMUI fsmui = null;

    bool isBeingDragged = false;

    public RectTransform OwnRectTransform { get; private set; }

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
        if (_eventData.button != PointerEventData.InputButton.Left) return;
        
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
        OnStateMovedContinous?.Invoke(GetComponent<RectTransform>().localPosition);
    }

    public void OnBeginDrag(PointerEventData _eventData)
    {
        switch (_eventData.button)
        {
            case PointerEventData.InputButton.Left:
                isBeingDragged = true;
                break;
            case PointerEventData.InputButton.Right:
                OnStateStartCreateTransition?.Invoke(this);
                isBeingDragged = true;
                break;
        }
    }

    public void OnEndDrag(PointerEventData _eventData)
    {
        switch (_eventData.button)
        {
            case PointerEventData.InputButton.Left:
                isBeingDragged = false;
                OnStateMoved?.Invoke(transform.GetSiblingIndex(), OwnRectTransform.offsetMax * 2 - OwnRectTransform.rect.size);
                break;
            case PointerEventData.InputButton.Right:
                OnStateEndCreateTransition?.Invoke(this);
                isBeingDragged = false;
                break;
        }
    }

    public void OnPointerClick(PointerEventData _eventData)
    {
        switch (_eventData.button)
        {
            case PointerEventData.InputButton.Left:
                OnStateSelected?.Invoke(this);
                SelectState();
                break;
            case PointerEventData.InputButton.Right:
                if (!isBeingDragged)
                    Debug.Log("Open state menu");
                break;
            case PointerEventData.InputButton.Middle:
                DeleteState();
                break;
        }

        isBeingDragged = false;
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
    void UnselectState(StateUI _stateUI)
    {
        border.color = ColourLibrary.Instance.InactiveStateColor;
        fsmui.OnStateSelected -= UnselectState;
    }

    public void SetName(string _stateName)
    {
        stateName.SetText(_stateName);
    }

    void DeleteState()
    {
        OnStateDeleted.Invoke(transform.GetSiblingIndex());
        OnDeletion?.Invoke();
        Debug.Log("OIII destroy");
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData _eventData)
    {
        OnHover?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnUnhover?.Invoke();
    }

    public void SetState(State _state)
    {
        LinkedState = _state;
    }

}
