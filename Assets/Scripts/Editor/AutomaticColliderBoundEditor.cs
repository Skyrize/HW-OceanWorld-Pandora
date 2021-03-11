using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AutomaticColliderBound))]
class AutomaticColliderBoundEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        if(GUILayout.Button("Rebound")) {
            AutomaticColliderBound myScript = (AutomaticColliderBound)target;

            myScript.Rebound();
        }
    }
}