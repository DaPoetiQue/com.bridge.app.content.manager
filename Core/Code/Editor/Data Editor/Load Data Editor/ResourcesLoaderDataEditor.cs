using UnityEngine;
using UnityEditor;
using Bridge.Core.App.Content.Manager;

namespace Bridge.Core.UnityEditor.Content.Manager
{
    [CustomEditor(typeof(ResourcesLoaderData))]
    public class ResourcesLoaderDataEditor : Editor
    {
        #region Main

        public override void OnInspectorGUI()
        {
            GUILayout.Label("Resources Content Data : Loads all content inside the app's Resources folder.");
            GUILayout.Space(10);

            ResourcesLoaderData content = (ResourcesLoaderData)target;
            content.loadType = LoadType.Resources;

            string name = (string.IsNullOrEmpty(content.nameTag)) ? content.name : content.nameTag;

            content.nameTag = EditorGUILayout.TextField("Loader Name", name);
            GUILayout.Space(10);

            content.contentDirectory = EditorGUILayout.TextField("Resource Path", content.contentDirectory);
            GUILayout.Space(10);

            content.platform = (App.Content.Manager.RuntimePlatform)EditorGUILayout.EnumPopup("Runtime Platform", content.platform);
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
