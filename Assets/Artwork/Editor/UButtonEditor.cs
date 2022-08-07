using UnityEngine;
using UnityEditor;

namespace UFrame
{
    [CustomEditor(typeof(UButton))]
    public class UButtonEditor : Editor
    {
        UButton _target;
        SerializedProperty buttonType;
        SerializedProperty styleStructs;
        SerializedProperty onClick;

        private void OnEnable()
        {
            _target = target as UButton;

            buttonType = serializedObject.FindProperty("m_buttonType");
            styleStructs = serializedObject.FindProperty("m_styleStructs");
            onClick = serializedObject.FindProperty("OnClick");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(buttonType);
            if (buttonType.enumValueIndex == 1)
            {
                EditorGUILayout.PropertyField(styleStructs);
                if (styleStructs.isExpanded)
                {
                    styleStructs.arraySize = EditorGUILayout.IntField("Size", styleStructs.arraySize);
                    if (styleStructs.arraySize > 0)
                    {
                        for (int i = 0; i < styleStructs.arraySize; i++)
                        {
                            SerializedProperty property = styleStructs.GetArrayElementAtIndex(i);
                            EditorGUILayout.BeginVertical("box");
                            EditorGUILayout.PropertyField(property.FindPropertyRelative("targetGraphic"));
                            SerializedProperty transitionType = property.FindPropertyRelative("transitionType");
                            EditorGUILayout.PropertyField(transitionType);
                            switch (transitionType.enumValueIndex)
                            {
                                // Color tint
                                case 0: EditorGUILayout.PropertyField(property.FindPropertyRelative("stateColor")); break;
                                // Swap SVG
                                case 1: EditorGUILayout.PropertyField(property.FindPropertyRelative("stateSVG")); break;
                                // Swap Sprite
                                case 2: EditorGUILayout.PropertyField(property.FindPropertyRelative("stateSprite")); break;
                            }
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.Space();
                        }
                    }
                }
            }

            // Button OnClick Event Handler<Unity Event>
            EditorGUILayout.PropertyField(onClick,true);

            // Save Serialized Object Setting
            if (GUI.changed) EditorUtility.SetDirty(_target);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
