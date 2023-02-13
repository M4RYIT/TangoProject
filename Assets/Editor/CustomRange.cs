using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(IkControllerData))]
public class CustomRange : Editor
{
    float minDistance = 0f;
    float maxDistance = 1f;

    public override void OnInspectorGUI()
    {
        IkControllerData ikData = (IkControllerData)target;

        GUILayout.Space(10);

        #region MIN_MAX_SLIDER
        GUILayout.BeginHorizontal();   

        minDistance = EditorGUILayout.FloatField(minDistance, GUILayout.Width(50), GUILayout.Height(20));

        GUILayout.Space(10);

        EditorGUILayout.MinMaxSlider(ref ikData.FirstStepDistance, ref ikData.SecondStepDistance, minDistance, maxDistance);

        GUILayout.Space(10);

        maxDistance = EditorGUILayout.FloatField(maxDistance, GUILayout.Width(50), GUILayout.Height(20));

        GUILayout.EndHorizontal();
        #endregion MIN_MAX_SLIDER

        GUILayout.Space(10);

        #region SLIDER_LABELS
        EditorGUILayout.BeginHorizontal();

        ikData.FirstStepDistance = EditorGUILayout.FloatField("Step Distance 1", ikData.FirstStepDistance);
        ikData.SecondStepDistance = EditorGUILayout.FloatField("Step Distance 2", ikData.SecondStepDistance);

        EditorGUILayout.EndHorizontal();
        #endregion SLIDER_LABELS

        DrawDefaultInspector();
    }
}
