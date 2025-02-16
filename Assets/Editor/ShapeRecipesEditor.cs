using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShapeRecipes))]
public class ShapeRecipesEditor : Editor
{
    SerializedProperty shapeDatabase;
    SerializedProperty shapeRecipesList;
    private bool showRecipes = true;

    private void OnEnable()
    {
        shapeDatabase = serializedObject.FindProperty("shapeDatabase");
        shapeRecipesList = serializedObject.FindProperty("shapeRecipesList");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        ShapeRecipes shapeRecipe = (ShapeRecipes)target;

        EditorGUILayout.PropertyField(shapeDatabase);
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
        if (
            shapeDatabase.objectReferenceValue == null
            || shapeRecipe.ShapeDatabase.ShapeNames.Count == 0
        )
        {
            shapeRecipesList.arraySize = 0;
            EditorGUILayout.HelpBox(
                "Shape Database is missing or empty. Recipes cleared",
                MessageType.Error
            );
            return;
        }

        int listSize = shapeRecipesList.arraySize;
        listSize = EditorGUILayout.IntField("Size", listSize);
        if (listSize != shapeRecipesList.arraySize)
        {
            shapeRecipesList.arraySize = listSize;
        }

        showRecipes = EditorGUILayout.Foldout(
            showRecipes,
            "Recipes",
            true,
            EditorStyles.foldoutHeader
        );
        if (showRecipes)
        {
            string[] shapes = shapeRecipe.ShapeDatabase.ShapeNames.ToArray();
            EditorGUI.indentLevel++;
            for (int i = 0; i < listSize; i++)
            {
                Rect recipeRect = EditorGUILayout.BeginVertical();
                EditorGUI.DrawRect(recipeRect, Color.black);

                SerializedProperty recipe = shapeRecipesList.GetArrayElementAtIndex(i);
                SerializedProperty shapeA = recipe.FindPropertyRelative("shapeA");
                SerializedProperty shapeB = recipe.FindPropertyRelative("shapeB");
                SerializedProperty shapeC = recipe.FindPropertyRelative("shapeC");
                SerializedProperty recipeType = recipe.FindPropertyRelative("recipeType");

                int shapeAIndex = Array.IndexOf(shapes, shapeA.stringValue);
                if (shapeAIndex == -1)
                    shapeAIndex = 0;
                shapeAIndex = EditorGUILayout.Popup("Shape A", shapeAIndex, shapes);
                shapeA.stringValue = shapes[shapeAIndex];

                int shapeBIndex = Array.IndexOf(shapes, shapeB.stringValue);
                if (shapeBIndex == -1)
                    shapeBIndex = 0;
                shapeBIndex = EditorGUILayout.Popup("Shape B", shapeBIndex, shapes);
                shapeB.stringValue = shapes[shapeBIndex];

                int shapeCIndex = Array.IndexOf(shapes, shapeC.stringValue);
                if (shapeCIndex == -1)
                {
                    shapeCIndex = 0;
                }
                shapeCIndex = EditorGUILayout.Popup("Shape Result", shapeCIndex, shapes);
                shapeC.stringValue = shapes[shapeCIndex];

                recipeType.intValue = EditorGUILayout.Popup(
                    "Recipe Type",
                    recipeType.intValue,
                    Enum.GetNames(typeof(RecipeType))
                );

                if (GUILayout.Button("DELETE"))
                {
                    shapeRecipesList.DeleteArrayElementAtIndex(i);
                    break;
                }
                EditorGUILayout.EndVertical();
            }

            if (GUILayout.Button("+ Add Recipe"))
            {
                shapeRecipesList.arraySize++;
            }

            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
