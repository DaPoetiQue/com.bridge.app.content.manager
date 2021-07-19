using UnityEngine;
using UnityEditor;
using Bridge.Core.App.Content.Manager;

namespace Bridge.Core.UnityEditor.Content.Manager
{
    [CustomEditor(typeof(StreamingAssetsLoaderData))]
    public class StreamingLoaderDataEditor : Editor
    {
        #region Main

        public override void OnInspectorGUI()
        {
            GUILayout.Label("Streaming Assets Data : Loads all assets inside the Streaming Assets folder.)");
            GUILayout.Space(15);

            StreamingAssetsLoaderData content = (StreamingAssetsLoaderData)target;
            content.loadType = LoadType.StreamingAssets;

            string name = (string.IsNullOrEmpty(content.nameTag)) ? content.name : content.nameTag;

            content.nameTag = EditorGUILayout.TextField("Loader Name", name);
            GUILayout.Space(10);

            content.contentDirectory = EditorGUILayout.TextField("Streaming Path", content.contentDirectory);
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
