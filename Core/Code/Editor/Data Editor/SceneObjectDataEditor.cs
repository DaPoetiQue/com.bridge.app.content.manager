using UnityEngine;
using UnityEditor;
using Bridge.Core.App.Content.Manager;

namespace Bridge.Core.UnityEditor.Content.Manager
{
    [CustomEditor(typeof(SceneObjectData))]
    public class SceneObjectDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.Label("Scene Object");
            GUILayout.Space(5);

            SceneObjectData content = (SceneObjectData)target;

            string name = (string.IsNullOrEmpty(content.nameTag))? content.name : content.nameTag;

            content.nameTag = EditorGUILayout.TextField("Content Name", name);
            GUILayout.Space(10);

            content.prefab = EditorGUILayout.ObjectField("Content Prefab", content.prefab, typeof(GameObject), false) as GameObject;
            GUILayout.Space(5);

            SerializedObject serializedObjectInfo = new SerializedObject(content);
            SerializedProperty serializedPropertyInfo = serializedObjectInfo.FindProperty("contentDescription");
            EditorGUILayout.PropertyField(serializedPropertyInfo, true);
            serializedObjectInfo.ApplyModifiedProperties();
            GUILayout.Space(5);

            content.interactable = EditorGUILayout.Toggle("Interactable", content.interactable);
            GUILayout.Space(5);

            if(content.interactable)
            {
                SerializedObject serializedObjectInteractions = new SerializedObject(content);
                SerializedProperty serializedPropertyInteractions = serializedObjectInteractions.FindProperty("interactionData");
                EditorGUILayout.PropertyField(serializedPropertyInteractions, true);
                serializedObjectInteractions.ApplyModifiedProperties();
                GUILayout.Space(5);
            }

            content.enableOnLoad = EditorGUILayout.Toggle("Enable On Load", content.enableOnLoad);
            content.contentType = ContentType.SceneObject;
            GUILayout.Space(10);

            if (GUILayout.Button("Open Content Editor", GUILayout.Height(25)))
            {
                ContentCreatorEditorWindow.CreateContentLoadManagerWindow(content);
            }
        }
    }
}
