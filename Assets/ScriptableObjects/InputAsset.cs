using UnityEngine;

[CreateAssetMenu(fileName="InputAsset", menuName="ScriptableObjects/InputAsset")]
public class InputAsset : ScriptableObject
{
    public KeyCode ForwardKey;
    public KeyCode BackwardKey;
    public KeyCode TracksSwitchKey;
    public KeyCode OchosKey;
    public string TurnAxis;
    public string SideAxis;
    public string UpperTurnAxis;
    public string MedialunaAxis;
}