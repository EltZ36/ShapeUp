using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShapeDatabase))]
public class ShapeDatabaseEditor : Editor
{
    SerializedProperty shapeNames;
    SerializedProperty prefabs;

    private bool showShapes = true;

    private void OnEnable()
    {
        shapeNames = serializedObject.FindProperty("shapeNames");
        prefabs = serializedObject.FindProperty("prefabs");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        bool duplicates = false;
        HashSet<string> keys = new HashSet<string>();

        int listSize = prefabs.arraySize;
        listSize = EditorGUILayout.IntField("Size", listSize);
        if (listSize != prefabs.arraySize)
        {
            prefabs.arraySize = listSize;
        }

        showShapes = EditorGUILayout.Foldout(
            showShapes,
            "Shapes",
            true,
            EditorStyles.foldoutHeader
        );
        if (showShapes)
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < listSize; i++)
            {
                EditorGUILayout.BeginHorizontal();

                SerializedProperty prefabProp = prefabs.GetArrayElementAtIndex(i);

                GameObject prefab = (GameObject)prefabProp.objectReferenceValue;
                string name = prefab != null ? prefab.name : null;

                GUILayout.Label("Name");
                GUILayout.Label(name != null ? name : "null");
                GUILayout.Label("Prefab");
                EditorGUILayout.PropertyField(prefabProp, GUIContent.none);

                if (name != null)
                {
                    if (keys.Contains(name))
                    {
                        duplicates = true;
                    }
                    else
                    {
                        keys.Add(name);
                    }
                }

                if (GUILayout.Button("x"))
                {
                    prefabs.DeleteArrayElementAtIndex(i);
                    break;
                }

                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("+ Add Shape"))
            {
                prefabs.arraySize++;
            }
            EditorGUI.indentLevel--;
        }

        if (keys.Count > 0)
        {
            shapeNames.ClearArray();
            shapeNames.arraySize = keys.Count;
            List<string> shapeNameSet = keys.ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                shapeNames.GetArrayElementAtIndex(i).stringValue = shapeNameSet[i];
            }
        }
        else
        {
            shapeNames.arraySize = 0;
        }

        if (duplicates)
        {
            EditorGUILayout.HelpBox("Duplicate shape name detected", MessageType.Error);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
