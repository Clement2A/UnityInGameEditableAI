using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionWindow : MonoBehaviour
{
    [SerializeField]
    Button closeButton = null;
    [SerializeField] 
    TransitionUI aToBTransition = null;
    [SerializeField] 
    TransitionUI bToATransition = null;

    State StateOne { get; set; }
    State StateTwo { get; set; }
    void Start()
    {
        closeButton.onClick.AddListener(CloseWindow);
    }

    void Update()
    {
        
    }

    public void SetStates(State _stateOne, State _stateTwo)
    {
        StateOne = _stateOne;
        StateTwo = _stateTwo;
        aToBTransition.SetupMenu(StateOne, StateTwo);
        bToATransition.SetupMenu(StateTwo, StateOne);
    }

    void CloseWindow()
    {
        gameObject.SetActive(false);
        StateOne = null;
        StateTwo = null;
        aToBTransition.Reset();
        bToATransition.Reset();
    }
}
