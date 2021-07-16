using UnityEngine;
using UnityEditor;
using Bridge.Core.App.Content.Manager;

namespace Bridge.Core.App.Content.Editor
{
    public class ContentLoadManagerWindow : EditorWindow
    {
        #region Window Initialization

        [MenuItem("3ridge/Create/Content")]
        private static void CreateContentLoadManagerWindow()
        {
            ContentLoadManagerWindow window = GetWindow<ContentLoadManagerWindow>("3ridge Content Creator");
            window.minSize = new Vector2(400.0f, 300.0f);
        }

        #endregion

        #region Window Layouts

        #region Textures

        private Texture2D iconTexture;
        private Texture2D headerSectionTexture;
        private Texture2D settingsSectionTexture;

        #endregion

        #region Colors

        private Color headerSectionColor = new Color(25.0f / 255.0f, 25.0f / 255.0f, 25.0f / 255.0f, 1.0f);
        private Color settingsSectionColor = new Color(35.0f / 255.0f, 35.0f / 255.0f, 35.0f / 255.0f, 1.0f);

        #endregion

        #region Rects

        private Rect iconRect;
        private Rect headerSectionRect;
        private Rect settingsSectionRect;

        #endregion

        #endregion

        #region Window Content

        private GUIContent settingsHeaderContent = new GUIContent();



        #endregion

        #region Window Styles

        private GUIStyle settingsHeaderStyle = new GUIStyle();
        private GUIStyle settingContentStyle = new GUIStyle();
        private GUIStyle settingsButtonStype = new GUIStyle();

        #endregion

        #region Settings

        private ContentType contentType;

        #region Scene Data

        private string contentName;

        private static SceneObjectData sceneObjectData;
        private static SceneUIData sceneUIData;

        #endregion

        #endregion

        #region Unity

        private void OnEnable() => Init();
        private void OnGUI() => OnWindowUpdates();

        #endregion

        #region Initializations

        private void Init()
        {
            InitializeTextures();
            InitializeLayoutStyles();
            InitializeContentData();
        }

        private void InitializeTextures()
        {
            #region Header

            headerSectionTexture = new Texture2D(1, 1);
            headerSectionTexture.SetPixel(0, 0, headerSectionColor);
            headerSectionTexture.Apply();

            #endregion

            #region Icon

            iconTexture = Resources.Load<Texture2D>("Editor/Windows");

            #endregion

            #region Settings

            settingsSectionTexture = new Texture2D(1, 1);
            settingsSectionTexture.SetPixel(0, 0, settingsSectionColor);
            settingsSectionTexture.Apply();

            #endregion
        }

        private void InitializeLayoutStyles()
        {
            #region Settings Header

            settingsHeaderStyle.normal.textColor = Color.white;
            settingsHeaderStyle.fontSize = 15;
            settingsHeaderStyle.fontStyle = FontStyle.Bold;
            settingsHeaderStyle.padding.top = 40;
            settingsHeaderStyle.padding.left = 50;
            settingsHeaderStyle.alignment = TextAnchor.LowerCenter;
            settingsHeaderContent.text = "Scene Content Creator";

            #endregion

            #region Settings Content

            settingContentStyle.normal.textColor = Color.white;
            settingContentStyle.fontSize = 12;
            settingContentStyle.alignment = TextAnchor.LowerLeft;
            settingContentStyle.padding.left = 25;

            #endregion
        }

        private void InitializeContentData()
        {
            sceneObjectData = CreateInstance<SceneObjectData>();
            sceneUIData = CreateInstance<SceneUIData>();
        }

        #endregion

        #region Main

        /// <summary>
        /// Draws window layouts.
        /// </summary>
        private void OnWindowUpdates()
        {
            DrawLayouts();
            DrawSettingsLayout();
        }

        private void DrawLayouts()
        {
            #region Header

            headerSectionRect.x = 0;
            headerSectionRect.y = 0;
            headerSectionRect.width = Screen.width;
            headerSectionRect.height = 100;

            GUI.DrawTexture(headerSectionRect, headerSectionTexture);

            #endregion

            #region Icon

            iconRect.width = 100;
            iconRect.height = 100;
            iconRect.x = 10;
            iconRect.y = 0;

            GUI.DrawTexture(iconRect, iconTexture);

            GUILayout.Label(settingsHeaderContent, settingsHeaderStyle);

            #endregion

            #region Settings

            settingsSectionRect.x = 0;
            settingsSectionRect.y = headerSectionRect.height;
            settingsSectionRect.width = Screen.width;
            settingsSectionRect.height = Screen.height - headerSectionRect.height;

            GUI.DrawTexture(settingsSectionRect, settingsSectionTexture);

            #endregion
        }

        private void DrawSettingsLayout()
        {
            GUILayout.BeginArea(settingsSectionRect);

            GUILayout.Space(25);

            EditorGUILayout.BeginHorizontal();

            contentType = (ContentType)EditorGUILayout.EnumPopup("Content Type", contentType);

            EditorGUILayout.EndHorizontal();

            GUILayout.Space(20);

            contentName = EditorGUILayout.TextField("Content Name", contentName);

            switch (contentType)
            {
                case ContentType.SceneObject:

                    sceneObjectData.nameTag = contentName;
                    GUILayout.Space(10);
                    sceneObjectData.prefab = EditorGUILayout.ObjectField("Object Prefab", sceneObjectData.prefab, typeof(GameObject), false) as GameObject;
                    GUILayout.Space(10);
                    sceneObjectData.enableOnLoad = EditorGUILayout.Toggle("Enable Content On Load", sceneObjectData.enableOnLoad);
                    sceneObjectData.contentType = contentType;

                    break;

                case ContentType.SceneUI:

                    sceneUIData.nameTag = contentName;
                    GUILayout.Space(10);
                    sceneUIData.prefab = EditorGUILayout.ObjectField("UI Prefab", sceneUIData.prefab, typeof(GameObject), false) as GameObject;
                    GUILayout.Space(10);
                    sceneUIData.screenType = (ScreenType)EditorGUILayout.EnumPopup("Screen Type", sceneUIData.screenType);
                    GUILayout.Space(10);
                    sceneUIData.uiType = (UIType)EditorGUILayout.EnumPopup("UI Type", sceneUIData.uiType);
                    GUILayout.Space(10);
                    sceneUIData.enableOnLoad = EditorGUILayout.Toggle("Enable Content On Load", sceneUIData.enableOnLoad);
                    sceneUIData.contentType = contentType;

                    break;
            }

            GUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Create", GUILayout.Height(35)))
            {
                CreateContent(contentType);
            }

            EditorGUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        private void CreateContent(ContentType contentType)
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Scene Content", contentName, "asset", "Save Created Scene Content");

            switch (contentType)
            {
                case ContentType.SceneObject:

                    AssetDatabase.CreateAsset(sceneObjectData, path);
                    AssetDatabase.Refresh();

                    UnityEngine.Debug.Log($"-->> <color=white>A scene [<color=grey>Object</color>]  content named  :</color> <color=cyan>[ {contentName} ]</color><color=white>.has been created successfully at directory :</color> <color=orange>{path}</color><color=white>.</color>");

                    break;

                case ContentType.SceneUI:

                    AssetDatabase.CreateAsset(sceneUIData, path);
                    AssetDatabase.Refresh();

                    UnityEngine.Debug.Log($"-->> <color=white>A scene [<color=grey>UI</color>] content named  :</color> <color=cyan>[ {contentName} ]</color><color=white>.has been created successfully at directory :</color> <color=orange>{path}</color><color=white>.</color>");

                    break;
            }
        }

        #endregion
    }
}
