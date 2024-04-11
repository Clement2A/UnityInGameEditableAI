using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourLibrary : Singleton<ColourLibrary>
{
    [SerializeField] Color inactiveStateColor = Color.grey;
    [SerializeField] Color selectedStateColor = Color.green;
    [SerializeField] Color activeStateColor = Color.blue;

    public Color InactiveStateColor => inactiveStateColor;
    public Color SelectedStateColor => selectedStateColor;
    public Color ActiveStateColor => activeStateColor;
}
