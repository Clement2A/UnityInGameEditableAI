using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class FSMUI : MonoBehaviour, IPointerMoveHandler
{
    public event Action<StateUI> OnStateSelected = null;
    public event Action<Vector2> OnMouseMove = null;

    [FormerlySerializedAs("UIState")] [SerializeField]
    StateUI uiState = null;
    [SerializeField]
    Button quitButton = null;
    [SerializeField]
    Button addStateButton = null;
    [SerializeField]
    Button startFsmButton = null;
    [SerializeField]
    Button deleteStateButton = null;
    [SerializeField]
    GameObject stateContainer = null;
    [SerializeField]
    GameObject lineContainer = null;
    [SerializeField]
    TMP_InputField aiNameInput = null;
    [SerializeField]
    StateSelectionMenu stateSelectionMenu = null;
    [SerializeField]
    TransitionUI transitionUIPrefab = null;

    EditableAI currentAI = null;

    EditableAI CurrentAI => currentAI;
    StateUI CurrentHoveredState { get; set; } = null;
    TransitionUI CurrentTransition { get; set; } = null;

    void Start()
    {
        gameObject.SetActive(false);
        SubscribeToAIOnClick();
        quitButton.onClick.AddListener(HideUI);
        aiNameInput.onDeselect.AddListener(RenameCurrentAI);
        addStateButton.onClick.AddListener(ShowStateSelectionMenu);
        startFsmButton.onClick.AddListener(StartFsm);
    }

    void Update()
    {
        
    }

    void SubscribeToAIOnClick()
    {
        EditableAIManager.Instance.OnAIClicked += DisplayUI;
    }

    void UnsubscribeToAIOnClick()
    {
        EditableAIManager.Instance.OnAIClicked -= DisplayUI;
    }

    void DisplayUI(EditableAI _ai)
    {
        UnsubscribeToAIOnClick();
        currentAI = _ai;
        gameObject.SetActive(true);
        aiNameInput.text = _ai.name;

        List<State> _allCheckedStates = new List<State>();

        for (int i = 0; i < _ai.Fsm.AllStates.Count; i++)
        {
            State _currState = _ai.Fsm.AllStates[i];
            StateUI _newState = CreateStateUI(_currState, _currState.Behaviour);
            _newState.OwnRectTransform.offsetMax = _ai.FsmUiData.StatePositions[i];
            _allCheckedStates.Add(_currState);
        }

        for (int i = 0; i < _ai.Fsm.AllStates.Count; i++)
        {
            State _currState = _ai.Fsm.AllStates[i];
            foreach (Transition _currTransition in _currState.Transitions)
            {
                State _nextState = _currTransition.NextState;
                if(!_allCheckedStates.Contains(_nextState))
                    continue;
                int _stateIndex = _allCheckedStates.IndexOf(_nextState);
                CreateFullTransition(stateContainer.transform.GetChild(_stateIndex).GetComponent<StateUI>(), stateContainer.transform.GetChild(i).GetComponent<StateUI>());
            }
        }
    }

    void HideUI()
    {
        SubscribeToAIOnClick();
        currentAI = null;
        gameObject.SetActive(false);
        foreach(Transform _child in stateContainer.transform)
        {
            Destroy(_child.gameObject);
        }
        foreach(Transform _child in lineContainer.transform)
        {
            Destroy(_child.gameObject);
        }
        OnStateSelected = null;
    }

    void RenameCurrentAI(string _name)
    {
        currentAI.name = _name;
    }

    StateUI CreateStateUI(State _state, Behaviour _behaviour)
    {
        StateUI _newState = Instantiate<StateUI>(uiState, stateContainer.transform, false);
        _newState.OwnRectTransform.offsetMin = Vector2.zero;
        _newState.OwnRectTransform.offsetMax = Vector2.zero;
        _newState.OnStateMoved += UpdateStatePosition;
        _newState.OnStateSelected += (_state) => { OnStateSelected?.Invoke(_state); };
        _newState.OnStateStartCreateTransition += StartTransitionCreation;
        _newState.OnStateEndCreateTransition += TryCreateTransition;
        _newState.OnStateDeleted += DeleteState;
        _newState.OnHover += (_state) => { CurrentHoveredState = _state;};
        _newState.OnUnhover += () => { CurrentHoveredState = null;};
        _newState.SetName(_behaviour.StateName);
        _newState.SetFSMUI(this);
        _newState.SetState(_state);
        return _newState;
    }

    void DeleteState(int _stateIndex)
    {
        currentAI.RemoveState(_stateIndex);
    }

    private void ShowStateSelectionMenu()
    {
        StateSelectionMenu _ssm = Instantiate(stateSelectionMenu, stateContainer.transform);
        _ssm.GetComponent<RectTransform>().pivot = Vector2.zero;
        Vector2 _halfButtonSize = addStateButton.GetComponent<RectTransform>().sizeDelta / 2;
        _ssm.transform.position = addStateButton.transform.position /* + new Vector3(_halfButtonSize.x, _halfButtonSize.y, 0)*/;
        Debug.Log(_halfButtonSize);
        _ssm.OnBehaviourSelected += AddStateToAI;
    }

    void AddStateToAI(Behaviour _behaviour)
    {
        Behaviour _instancedBehaviour = Instantiate(_behaviour);
        State _newState = currentAI.AddState(Vector2.zero, _instancedBehaviour);
        CreateStateUI(_newState, _behaviour);
    }

    void UpdateStatePosition(int _stateIndex, Vector2 _newPos)
    {
        currentAI.FsmUiData.StatePositions[_stateIndex] = _newPos;
    }

    void StartFsm()
    { 
        currentAI.Fsm.StartFSM();
    }

    void StartTransitionCreation(StateUI _startState)
    {
        CurrentTransition = Instantiate(transitionUIPrefab, lineContainer.transform, false);
        CurrentTransition.SetFSMUI(this);
        CurrentTransition.SetFirstState(_startState);
    }

    void TryCreateTransition(StateUI _state)
    {
        if (CurrentHoveredState != null)
        {
            CurrentTransition.SetLastState(CurrentHoveredState);
            _state.LinkedState.AddTransition(CurrentHoveredState.LinkedState);
        }
        else
        {
            Destroy(CurrentTransition.gameObject);
            CurrentTransition = null;
            Debug.LogWarning("Cannot create transition");
            OnMouseMove = null;
        }
    }

    void CreateFullTransition(StateUI _startState, StateUI _endState)
    {
        CurrentTransition = Instantiate(transitionUIPrefab, lineContainer.transform, false);
        CurrentTransition.SetFSMUI(this);
        CurrentTransition.SetFirstState(_startState);
        CurrentTransition.SetLastState(_endState);
    }

    public void OnPointerMove(PointerEventData _eventData)
    {
        OnMouseMove?.Invoke(_eventData.position-new Vector2(Screen.width / 2, Screen.height / 2));
    }
}
