using UnityEngine;
using UnityEditor;
using Bridge.Core.App.Content.Manager;


namespace Bridge.Core.UnityEditor.Content.Manager
{
    [CustomEditor(typeof(SceneUIData))]
    public class SceneUIDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.Label("Scene UI");
            GUILayout.Space(5);

            SceneUIData content = (SceneUIData)target;

            string name = (string.IsNullOrEmpty(content.nameTag)) ? content.name : content.nameTag;
            content.nameTag = EditorGUILayout.TextField("Content Name", name);
            GUILayout.Space(5);

            content.prefab = EditorGUILayout.ObjectField("Content Prefab", content.prefab, typeof(GameObject), false) as GameObject;
            GUILayout.Space(5);

            content.uiType = (UIType)EditorGUILayout.EnumPopup("UI Type", UIType.UIButton);
            GUILayout.Space(5);

            content.screenType = (ScreenType)EditorGUILayout.EnumPopup("UI Screen", ScreenType.Menu);
            GUILayout.Space(5);

            content.enableOnLoad = EditorGUILayout.Toggle("Enable Content On Load", content.enableOnLoad);
            content.contentType = ContentType.SceneUI;
            GUILayout.Space(10);

            if (GUILayout.Button("Open Content Editor", GUILayout.Height(25)))
            {
                SceneContentCreatorEditorWindow.OpenContentCreatorWindow(content);
            }
        }
    }
}
