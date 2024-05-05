using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateLinkUI : MonoBehaviour
{
    [SerializeField]
    LineRenderer lineRenderer = null;
    [SerializeField]
    TransitionButton transitionButton = null;
    FSMUI fsmui = null;
    StateUI stateOne = null;
    StateUI stateTwo = null;

    public TransitionButton TransitionButton => transitionButton;
    void Start()
    {
    }

    void Update()
    {
        
    }

    public void SetFSMUI(FSMUI _fsmui)
    {
        fsmui = _fsmui;
    }

    void MoveFirstPosition(Vector2 _pos)
    {
        lineRenderer.SetPosition(0, _pos);
    }

    void MoveLastPosition(Vector2 _pos)
    {
        lineRenderer.SetPosition(1, _pos);
    }

    void UpdateTransitionUILocation()
    {
        transitionButton.GetComponent<RectTransform>().localPosition = (lineRenderer.GetPosition(0) + lineRenderer.GetPosition(1)) / 2;
    }

    void MoveLastPositionFromMouse(Vector2 _pos)
    {
        Vector3 _currPos = lineRenderer.GetPosition(1);
        lineRenderer.SetPosition(1, _pos);
    }

    public void SetFirstState(StateUI _state)
    {
        stateOne = _state;
        _state.OnStateMovedContinous += MoveFirstPosition;
        MoveFirstPosition(_state.GetComponent<RectTransform>().localPosition);
        Vector2 _mousePos2D = Input.mousePosition;
        MoveLastPosition(_mousePos2D - new Vector2(Screen.width / 2, Screen.height / 2));
        fsmui.OnMouseMove += MoveLastPositionFromMouse;
    }

    public void SetLastState(StateUI _state)
    {
        stateTwo = _state;
        _state.OnStateMovedContinous += MoveLastPosition;
        MoveLastPosition(_state.GetComponent<RectTransform>().localPosition);
        fsmui.OnMouseMove -= MoveLastPositionFromMouse;
        SpawnTransitionUI();
    }

    void SpawnTransitionUI()
    {
        transitionButton = Instantiate<TransitionButton>(transitionButton, transform, false);
        UpdateTransitionUILocation();
        stateOne.OnStateMovedContinous += (_pos) => UpdateTransitionUILocation();
        stateTwo.OnStateMovedContinous += (_pos) => UpdateTransitionUILocation();
    }
}
