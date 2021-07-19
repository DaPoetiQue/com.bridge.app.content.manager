using UnityEngine;
using UnityEditor;
using Bridge.Core.App.Content.Manager;

namespace Bridge.Core.UnityEditor.Content.Manager
{
    [CustomEditor(typeof(AddressablesLoaderData))]
    public class AddressablesLoaderDataEditor : Editor
    {
        #region Main

        public override void OnInspectorGUI()
        {
            GUILayout.Label("Addressable Assets Data : Loads assets using the Unity's Addressables System.");
            GUILayout.Space(15);

            AddressablesLoaderData content = (AddressablesLoaderData)target;
            content.loadType = LoadType.Addressables;

            string name = (string.IsNullOrEmpty(content.nameTag)) ? content.name : content.nameTag;

            content.nameTag = EditorGUILayout.TextField("Loader Name", name);
            GUILayout.Space(10);

            content.label = EditorGUILayout.TextField("Addressables Label", content.label);
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
