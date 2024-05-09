using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class TransitionUI : MonoBehaviour
{
    [SerializeField] Button addTransitionButton = null;
    [SerializeField] TMP_Text transitionText = null;
    [SerializeField] TransitionSelectionMenu transitionSelectionMenu = null;
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
    }

    public void SetupMenu(State _firstState, State _secondState)
    {
        firstState = _firstState;
        secondState = _secondState;
        transitionText.text = firstState.Behaviour.StateName + " -> " + secondState.Behaviour.StateName;
    }

    void OpenTransitionSelectionMenu()
    {
        transitionSelectionMenu.gameObject.SetActive(!transitionSelectionMenu.gameObject.activeSelf);
    }

    void CreateTransition(Condition _condition)
    {
        transitionSelectionMenu.gameObject.SetActive(false);
        firstState.AddTransition(secondState, _condition);
    }
}
