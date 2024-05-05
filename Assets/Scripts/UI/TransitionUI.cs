using UnityEngine;
using Button = UnityEngine.UI.Button;

public class TransitionUI : MonoBehaviour
{
    [SerializeField] Button addTransitionButton = null;
    State firstState = null;
    State secondState = null;
    void Start()
    {
        addTransitionButton.onClick.AddListener(OpenTransitionSelectionMenu);
    }

    void Update()
    {
        
    }

    public void SetupMenu(State _firstState, State _secondState)
    {
        firstState = _firstState;
        secondState = _secondState;
    }

    void OpenTransitionSelectionMenu()
    {
        throw new System.NotImplementedException();
    }
}
