using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor;
using UnityEngine.EventSystems;

public class FSMUI : MonoBehaviour
{
    public event Action<StateUI> OnStateSelected = null;

    [SerializeField]
    StateUI UIState = null;
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
    TMP_InputField aiNameInput = null;
    [SerializeField]
    StateSelectionMenu stateSelectionMenu = null;

    EditableAI currentAI = null;

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

    void UnubscribeToAIOnClick()
    {
        EditableAIManager.Instance.OnAIClicked -= DisplayUI;
    }

    void DisplayUI(EditableAI _ai)
    {
        UnubscribeToAIOnClick();
        currentAI = _ai;
        gameObject.SetActive(true);
        aiNameInput.text = _ai.name;

        for (int i = 0; i < _ai.Fsm.AllStates.Count; i++)
        {
            StateUI _newState = CreateStateUI(_ai.Fsm.AllStates[i].Behaviour);
            _newState.OwnRectTransform.offsetMax = _ai.FsmUiData.StatePositions[i];
            _newState.StateIndex = i;
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
        OnStateSelected = null;
    }

    void RenameCurrentAI(string _name)
    {
        currentAI.name = _name;
    }

    StateUI CreateStateUI(Behaviour _behaviour)
    {
        StateUI _newState = Instantiate<StateUI>(UIState);
        _newState.StateIndex = stateContainer.transform.childCount - 1;
        _newState.transform.SetParent(stateContainer.transform);
        _newState.OwnRectTransform.offsetMin = Vector2.zero;
        _newState.OwnRectTransform.offsetMax = Vector2.zero;
        _newState.OnStateMoved += UpdateStatePosition;
        _newState.OnStateSelected += (state) => { OnStateSelected?.Invoke(state); };
        _newState.SetName(_behaviour.StateName);
        _newState.SetFSMUI(this);
        return _newState;
    }

    private void ShowStateSelectionMenu()
    {
        StateSelectionMenu _ssm = Instantiate(stateSelectionMenu);
        _ssm.transform.SetParent(stateContainer.transform);
        _ssm.GetComponent<RectTransform>().pivot = Vector2.zero;
        Vector2 _halfButtonSize = addStateButton.GetComponent<RectTransform>().sizeDelta / 2;
        _ssm.transform.position = addStateButton.transform.position + new Vector3(_halfButtonSize.x, _halfButtonSize.y, 0);
        _ssm.OnBehaviourSelected += AddStateToAI;
    }

    void AddStateToAI(Behaviour _behaviour)
    {
        CreateStateUI(_behaviour);
        Behaviour _instancedBehaviour = Instantiate(_behaviour);
        currentAI.AddState(Vector2.zero, _instancedBehaviour);
    }

    void UpdateStatePosition(int _stateIndex, Vector2 _newPos)
    {
        Debug.Log("Moving state with index " + _stateIndex);
        currentAI.FsmUiData.StatePositions[_stateIndex] = _newPos;
    }

    void StartFsm()
    {
        currentAI.Fsm.StartFSM();
    }
}
