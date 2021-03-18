using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ColliderEvent))]
[CanEditMultipleObjects]
public class ColliderEventEditor : Editor
{
    SerializedProperty useTagFilter;
    SerializedProperty useLayerFilter;
    SerializedProperty useComponentFilter;
    SerializedProperty useParentComponentFilter;
    SerializedProperty useChildComponentFilter;

    SerializedProperty tags;
    SerializedProperty layerMask;
    SerializedProperty componentType;
    SerializedProperty parentComponentType;
    SerializedProperty childComponentType;

    SerializedProperty onCollide;
    SerializedProperty onStay;
    SerializedProperty onExit;
    
    void OnEnable()
    {
        useTagFilter = serializedObject.FindProperty("useTagFilter");
        useLayerFilter = serializedObject.FindProperty("useLayerFilter");
        useComponentFilter = serializedObject.FindProperty("useComponentFilter");
        useParentComponentFilter = serializedObject.FindProperty("useParentComponentFilter");
        useChildComponentFilter = serializedObject.FindProperty("useChildComponentFilter");

        tags = serializedObject.FindProperty("tags");
        layerMask = serializedObject.FindProperty("layerMask");
        componentType = serializedObject.FindProperty("componentType");
        parentComponentType = serializedObject.FindProperty("parentComponentType");
        childComponentType = serializedObject.FindProperty("childComponentType");

        onCollide = serializedObject.FindProperty("onCollide");
        onStay = serializedObject.FindProperty("onStay");
        onExit = serializedObject.FindProperty("onExit");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        // DrawDefaultInspector();

        ColliderEvent script = (ColliderEvent)target;

        EditorGUILayout.PropertyField(useTagFilter);
        if (script.UseTagFilter) {
            EditorGUILayout.PropertyField(tags);
        }

        EditorGUILayout.PropertyField(useLayerFilter);
        if (script.UseLayerFilter) {
            EditorGUILayout.PropertyField(layerMask);
        }

        EditorGUILayout.PropertyField(useComponentFilter);
        if (script.UseComponentFilter) {
            EditorGUILayout.PropertyField(componentType);
        }

        EditorGUILayout.PropertyField(useParentComponentFilter);
        if (script.UseParentComponentFilter) {
            EditorGUILayout.PropertyField(parentComponentType);
        }
        
        EditorGUILayout.PropertyField(useChildComponentFilter);
        if (script.UseChildComponentFilter) {
            EditorGUILayout.PropertyField(childComponentType);
        }

        EditorGUILayout.PropertyField(onCollide);
        EditorGUILayout.PropertyField(onStay);
        EditorGUILayout.PropertyField(onExit);


        serializedObject.ApplyModifiedProperties();
    }
}