using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransitionListElement : MonoBehaviour
{
    public event Action<int> OnDelete = null;

    [SerializeField] TMP_Text transitionText = null;
    [SerializeField] Button deleteTransitionButton = null;
    void Start()
    {
        deleteTransitionButton.onClick.AddListener(BroadcastTransitionDeletion);
    }

    void BroadcastTransitionDeletion()
    {
        OnDelete?.Invoke(transform.GetSiblingIndex());
        Destroy(gameObject);
    }

    public void SetTransitionName(string _conditionConditionName)
    {
        transitionText.SetText(_conditionConditionName);
    }

}
