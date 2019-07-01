using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TreeBase))]

public class TestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); //default

        if(GUILayout.Button("Generate Tree Map"))
        {
            Debug.Log("pressed");
        }
    }
}
