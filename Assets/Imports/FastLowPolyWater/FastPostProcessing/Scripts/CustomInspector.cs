using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

    [CustomEditor(typeof(HighSpeedPostProcessing))]
    public class CustomInspector : Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        HighSpeedPostProcessing ed = (HighSpeedPostProcessing)target;
        if (EditorWindow.HasOpenInstances<SceneView>())
        {
            ed.SceneCamera = ed.GetCamera();
        
        ed.UpdateComponents();
        }
    }

    }
#endif
