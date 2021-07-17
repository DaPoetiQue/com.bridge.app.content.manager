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
            SceneObjectData content = (SceneObjectData)target;

            string name = (string.IsNullOrEmpty(content.nameTag))? content.name : content.nameTag;

            content.nameTag = EditorGUILayout.TextField("Content Name", name);
            GUILayout.Space(10);

            content.prefab = EditorGUILayout.ObjectField("Object Prefab", content.prefab, typeof(GameObject), false) as GameObject;
            GUILayout.Space(5);

            content.enableOnLoad = EditorGUILayout.Toggle("Enable Content On Load", content.enableOnLoad);
            content.contentType = ContentType.SceneObject;
            GUILayout.Space(10);

            if (GUILayout.Button("Open Content Editor", GUILayout.Height(25)))
            {
                ContentCreatorEditorWindow.CreateContentLoadManagerWindow(content);
            }
        }
    }
}
