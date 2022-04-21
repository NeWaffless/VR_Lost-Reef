using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScreenCap))]
public class ScreenCapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ScreenCap myScript = (ScreenCap)target;
        if (GUILayout.Button("Take Screenshot"))
        {
            myScript.Capture();
        }
    }
}
