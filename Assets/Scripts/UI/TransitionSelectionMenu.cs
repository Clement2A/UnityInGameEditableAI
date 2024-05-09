using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransitionSelectionMenu : MonoBehaviour
{
    public event Action<Condition> OnConditionSelected = null;

    [SerializeField] Condition[] allConditions;
    [SerializeField] Button buttonTemplate;
    [SerializeField] VerticalLayoutGroup buttonContainer;

    void Start()
    {
        foreach (Condition _condition in allConditions)
        {
            Button _newButton = Instantiate(buttonTemplate, buttonContainer.transform, false);
            _newButton.onClick.AddListener(() => CreateTransition(_condition) );
            _newButton.GetComponentInChildren<TMP_Text>().SetText(_condition.ConditionName);
        }
    }

    void CreateTransition(Condition _behaviour)
    {
        Condition _instanceBehaviour = Instantiate(_behaviour);
        OnConditionSelected.Invoke(_instanceBehaviour);
    }

    void Update()
    {
        
    }
}
