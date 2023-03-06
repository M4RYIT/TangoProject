using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum InputEnum 
{
    Tracks,
    Side,
    Move,
    Turn,
    UpperTurn,
    Pivot
}

[Serializable]
public struct InputBinding
{
    public InputEnum InputType;
    public KeyCode InputKey1;
    public KeyCode InputKey2;
}

[CreateAssetMenu(fileName="InputAsset", menuName="ScriptableObjects/InputAsset")]
public class InputAsset : ScriptableObject
{
    public List<InputBinding> inputs;
}