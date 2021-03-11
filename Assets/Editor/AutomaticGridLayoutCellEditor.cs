using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AutomaticGridLayoutCell))]
class AutomaticGridLayoutCellEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        if(GUILayout.Button("Resize")) {
            AutomaticGridLayoutCell myScript = (AutomaticGridLayoutCell)target;

            myScript.Resize();
        }
    }
}