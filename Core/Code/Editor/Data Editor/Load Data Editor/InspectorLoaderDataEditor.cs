using UnityEngine;
using UnityEditor;
using Bridge.Core.App.Content.Manager;

namespace Bridge.Core.UnityEditor.Content.Manager
{
    [CustomEditor(typeof(InspectorLoaderData))]
    public class InspectorLoaderDataEditor : Editor
    {
        #region Main

        public override void OnInspectorGUI()
        {
            GUILayout.Label("Inspector Content Data : Loads all the content assigned through the inspector panel.");
            GUILayout.Space(15);

            InspectorLoaderData content = (InspectorLoaderData)target;
            content.loadType = LoadType.Inspector;

            string name = (string.IsNullOrEmpty(content.nameTag)) ? content.name : content.nameTag;

            content.nameTag = EditorGUILayout.TextField("Loader Name", name);
            GUILayout.Space(10);

            SerializedObject serializedObjectInspector = new SerializedObject(content);
            SerializedProperty serializedPropertyInspector = serializedObjectInspector.FindProperty("ContentToLoad");
            EditorGUILayout.PropertyField(serializedPropertyInspector, true);
            serializedObjectInspector.ApplyModifiedProperties();
            GUILayout.Space(10);

            content.platform = (App.Content.Manager.Platform)EditorGUILayout.EnumPopup("Runtime Platform", content.platform);
            GUILayout.Space(10);

            SerializedObject serializedObjectInfo = new SerializedObject(content);
            SerializedProperty serializedPropertyInfo = serializedObjectInfo.FindProperty("description");
            EditorGUILayout.PropertyField(serializedPropertyInfo, true);
            serializedObjectInfo.ApplyModifiedProperties();
            GUILayout.Space(15);

            if (GUILayout.Button("Open Content Loader Editor", GUILayout.Height(35)))
            {
                ContentLoaderEditorWindow.OpenContentLoaderWindow(content);
            }
        }

        #endregion
    }
}
