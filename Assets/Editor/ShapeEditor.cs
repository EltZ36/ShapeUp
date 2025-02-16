using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Video Covering basics of custom editor
// https://www.youtube.com/watch?v=xFtFWmiW7IE&t=237s
[CustomEditor(typeof(Shape))]
public class ShapeEditor : Editor
{
    #region SerializedProperties
    SerializedProperty Tags;
    SerializedProperty uniqueID;
    SerializedProperty spriteName;

    SerializedProperty OnFixedUpdateEvent;
    SerializedProperty OnDragStartEvent;
    SerializedProperty OnDragEvent;
    SerializedProperty OnDragEndEvent;
    SerializedProperty OnPinchEvent;
    SerializedProperty OnSliceEvent;
    SerializedProperty OnTapEvent;
    SerializedProperty OnAccelerateEvent;
    SerializedProperty OnAttitudeChangeEvent;
    SerializedProperty OnCreateEvent;
    SerializedProperty OnDestroyEvent;
    #endregion

    #region HeaderBoolSerializedProperties
    SerializedProperty showFixedUpdate;
    SerializedProperty showDrag;
    SerializedProperty showPinch;
    SerializedProperty showSlice;
    SerializedProperty showTap;
    SerializedProperty showAccelerate;
    SerializedProperty showAttitude;
    SerializedProperty showCreate;
    SerializedProperty showDestroy;

    #endregion

    private ShapeTags checkTags;

    private void OnEnable()
    {
        Tags = serializedObject.FindProperty("Tags");
        uniqueID = serializedObject.FindProperty("uniqueID");
        spriteName = serializedObject.FindProperty("spriteName");

        OnFixedUpdateEvent = serializedObject.FindProperty("OnFixedUpdateEvent");
        OnDragStartEvent = serializedObject.FindProperty("OnDragStartEvent");
        OnDragEvent = serializedObject.FindProperty("OnDragEvent");
        OnDragEndEvent = serializedObject.FindProperty("OnDragEndEvent");
        OnPinchEvent = serializedObject.FindProperty("OnPinchEvent");
        OnSliceEvent = serializedObject.FindProperty("OnSliceEvent");
        OnTapEvent = serializedObject.FindProperty("OnTapEvent");
        OnAccelerateEvent = serializedObject.FindProperty("OnAccelerateEvent");
        OnAttitudeChangeEvent = serializedObject.FindProperty("OnAttitudeChangeEvent");
        OnCreateEvent = serializedObject.FindProperty("OnCreateEvent");
        OnDestroyEvent = serializedObject.FindProperty("OnDestroyEvent");

        showFixedUpdate = serializedObject.FindProperty("showFixedUpdate");
        showDrag = serializedObject.FindProperty("showDrag");
        showPinch = serializedObject.FindProperty("showPinch");
        showSlice = serializedObject.FindProperty("showSlice");
        showTap = serializedObject.FindProperty("showTap");
        showAccelerate = serializedObject.FindProperty("showAccelerate");
        showAttitude = serializedObject.FindProperty("showAttitude");
        showCreate = serializedObject.FindProperty("showCreate");
        showDestroy = serializedObject.FindProperty("showDestroy");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        Shape shape = (Shape)target;
        EditorGUILayout.BeginHorizontal();
        Collider2D c2D = shape.GetComponent<Collider2D>();
        if (c2D == null)
        {
            EditorGUILayout.HelpBox("Missing Collider2D", MessageType.Warning);
        }
        Rigidbody2D rb = shape.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            EditorGUILayout.HelpBox("Missing RigidBody2D", MessageType.Warning);
        }
        EditorGUILayout.EndHorizontal();
        SpriteRenderer sr = shape.GetComponent<SpriteRenderer>();
        if (sr == null || sr.sprite == null)
        {
            EditorGUILayout.HelpBox(
                "Please add Sprite Renderer and a Default Sprite",
                MessageType.Error
            );
            return;
        }

        // ChatGpt, EnumFlag dropdown menu
        // https://chatgpt.com/share/67af83c5-25a0-8010-806a-9da3bf897dca
        Tags.intValue = (int)
            (ShapeTags)EditorGUILayout.EnumFlagsField("Flags", (ShapeTags)Tags.intValue);
        checkTags = (ShapeTags)Tags.intValue;

        spriteName.stringValue = sr.sprite.name;
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.PropertyField(uniqueID);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(
            "Sprite Name:",
            new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.LowerCenter },
            GUILayout.MaxWidth(80)
        );
        EditorGUILayout.LabelField(
            shape.ShapeName,
            new GUIStyle(EditorStyles.helpBox)
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft,
                padding = new RectOffset(5, 5, 3, 3),
            }
        );
        EditorGUILayout.EndHorizontal();

        DrawFoldoutEvent(
            ShapeTags.OnFixedUpdate,
            showFixedUpdate,
            "Fixed Update Event",
            OnFixedUpdateEvent
        );
        DrawFoldoutEvent(
            ShapeTags.OnDrag,
            showDrag,
            "Drag Event",
            OnDragStartEvent,
            OnDragEvent,
            OnDragEndEvent
        );
        DrawFoldoutEvent(ShapeTags.OnPinch, showPinch, "Pinch Event", OnPinchEvent);
        DrawFoldoutEvent(ShapeTags.OnSlice, showSlice, "Slice Event", OnSliceEvent);
        DrawFoldoutEvent(ShapeTags.OnTap, showTap, "Tap Event", OnTapEvent);
        DrawFoldoutEvent(
            ShapeTags.OnAccelerate,
            showAccelerate,
            "Accelerate Event",
            OnAccelerateEvent
        );
        DrawFoldoutEvent(
            ShapeTags.OnAttitudeChange,
            showAttitude,
            "Attitude Change Event",
            OnAttitudeChangeEvent
        );
        DrawFoldoutEvent(ShapeTags.OnCreate, showCreate, "Create Event", OnCreateEvent);
        DrawFoldoutEvent(ShapeTags.OnDestroy, showDestroy, "Destroy Event", OnDestroyEvent);

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawFoldoutEvent(
        ShapeTags tag,
        SerializedProperty foldoutState,
        string title,
        params SerializedProperty[] properties
    )
    {
        if ((checkTags & tag) == tag)
        {
            foldoutState.boolValue = EditorGUILayout.BeginFoldoutHeaderGroup(
                foldoutState.boolValue,
                title
            );
            if (foldoutState.boolValue)
            {
                foreach (var property in properties)
                {
                    EditorGUILayout.PropertyField(property);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}
