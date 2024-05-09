using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class TransitionUI : MonoBehaviour
{
    [SerializeField] Button addTransitionButton = null;
    [SerializeField] TMP_Text transitionText = null;
    [SerializeField] TransitionSelectionMenu transitionSelectionMenu = null;
    [SerializeField] VerticalLayoutGroup activeTransitionContainer = null;
    [SerializeField] TransitionListElement transitionListElementPrefab = null;
    State firstState = null;
    State secondState = null;
    void Start()
    {
        addTransitionButton.onClick.AddListener(OpenTransitionSelectionMenu);
        transitionSelectionMenu.gameObject.SetActive(false);
        transitionSelectionMenu.OnConditionSelected += CreateTransition;
    }

    void Update()
    {
        
    }

    public void Reset()
    {
        firstState = null;
        secondState = null;
        foreach(Transform _child in activeTransitionContainer.transform)
        {
            Destroy(_child.gameObject);
        }
    }

    public void SetupMenu(State _firstState, State _secondState)
    {
        firstState = _firstState;
        secondState = _secondState;
        transitionText.text = firstState.Behaviour.StateName + " -> " + secondState.Behaviour.StateName;
        foreach (Transition _transition in _firstState.Transitions)
        {
            if(_transition.NextState == secondState)
                CreateTransitionUI(_transition.Condition);
        }
    }

    void OpenTransitionSelectionMenu()
    {
        transitionSelectionMenu.gameObject.SetActive(!transitionSelectionMenu.gameObject.activeSelf);
    }

    void CreateTransition(Condition _condition)
    {
        CreateTransitionUI(_condition);
        transitionSelectionMenu.gameObject.SetActive(false);
        firstState.AddTransition(secondState, _condition);
    }

    void CreateTransitionUI(Condition _condition)
    {
        TransitionListElement _transitionListElement = Instantiate(transitionListElementPrefab, activeTransitionContainer.transform, false);
        _transitionListElement.SetTransitionName(_condition.ConditionName);
        _transitionListElement.OnDelete += DeleteTransition;
    }

    void DeleteTransition(int _index)
    {
        firstState.RemoveTransition(_index);
    }
}
