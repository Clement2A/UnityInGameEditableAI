using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StateSelectionMenu : MonoBehaviour
{
    public event Action<Behaviour> OnBehaviourSelected = null;

    [SerializeField] Behaviour[] allBehaviour;
    [SerializeField] Button buttonTemplate;
    [SerializeField] VerticalLayoutGroup buttonContainer;

    void Start()
    {
        foreach (Behaviour _behaviour in allBehaviour)
        {
            Button _newButton = Instantiate(buttonTemplate);
            _newButton.transform.SetParent(buttonContainer.transform, false);
            _newButton.onClick.AddListener(() => CreateState(_behaviour) );
            _newButton.onClick.AddListener(() => Destroy(gameObject) );
            _newButton.GetComponentInChildren<TMP_Text>().SetText(_behaviour.StateName);
        }
    }

    void CreateState(Behaviour _behaviour)
    {
        OnBehaviourSelected.Invoke(_behaviour);
    }

    void Update()
    {
        
    }
}
