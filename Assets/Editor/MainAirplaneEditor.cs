using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MainAirplane))]

public class MainAirplaneEditor : Editor {

    SerializedProperty speedProp;
    private Texture2D texture;
     
    void OnEnable()
    {
        speedProp = serializedObject.FindProperty("speed");
        texture = Resources.Load<Texture2D>("MainPlane");
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Box(texture, GUILayout.Width(101), GUILayout.Height(101));

        

        base.OnInspectorGUI();
        serializedObject.Update();

        EditorGUILayout.Slider(speedProp, 0f, 100f, new GUIContent("SPEED"));

        if (!speedProp.hasMultipleDifferentValues)
            ProgressBar(speedProp.floatValue / 100.0f, "SPEED");

        serializedObject.ApplyModifiedProperties();
    }

    void ProgressBar(float value, string label)
    {
        // Get a rect for the progress bar using the same margins as a textfield:
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space();
    }
}
