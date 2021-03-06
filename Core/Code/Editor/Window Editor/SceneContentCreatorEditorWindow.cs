using UnityEngine;
using UnityEditor;
using Bridge.Core.App.Content.Manager;
using System;

namespace Bridge.Core.UnityEditor.Content.Manager
{
    public class SceneContentCreatorEditorWindow : EditorWindow
    {
        #region Window Initialization

        [MenuItem("Window/3ridge/Scene Content/Create Content Editor")]
        private static void OpenContentCreatorWindow()
        {
            SceneContentCreatorEditorWindow window = GetWindow<SceneContentCreatorEditorWindow>("3ridge Content Creator");
            window.minSize = new Vector2(400.0f, 450.0f);
        }

        public static void OpenContentCreatorWindow(ScenePropData sceneObjectData)
        {
            SceneContentCreatorEditorWindow window = GetWindow<SceneContentCreatorEditorWindow>("3ridge Content Creator");
            window.minSize = new Vector2(400.0f, 450.0f);

            UpdateWindowToSelectedContent(sceneObjectData);
        }

        public static void OpenContentCreatorWindow(SceneUIData sceneUIData)
        {
            SceneContentCreatorEditorWindow window = GetWindow<SceneContentCreatorEditorWindow>("3ridge Content Creator");
            window.minSize = new Vector2(400.0f, 450.0f);

            UpdateWindowToSelectedContent(sceneUIData);
        }

        #endregion

        #region Window Layouts

        #region Textures

        private Texture2D iconTexture;
        private Texture2D headerSectionTexture;
        private Texture2D settingsSectionTexture;
        private Texture2D settingsSectionContentTexture;

        #endregion

        #region Colors

        private Color headerSectionColor = new Color(35.0f / 255.0f, 35.0f / 255.0f, 35.0f / 255.0f, 1.0f);
        private Color settingsSectionColor = new Color(25.0f / 255.0f, 25.0f / 255.0f, 25.0f / 255.0f, 1.0f);
        private Color settingsSectionContentColor = new Color(25.0f / 255.0f, 25.0f / 255.0f, 25.0f / 255.0f, 1.0f);

        #endregion

        #region Rects

        private Rect iconRect;
        private Rect headerSectionRect;
        private Rect settingsSectionRect;
        private Rect settingsSectionContentRect;

        #endregion

        #region Window Styles

        private GUIStyle settingsHeaderStyle = new GUIStyle();
        private GUIStyle settingContentStyle = new GUIStyle();

        #endregion

        #endregion

        #region Window Content

        private GUIContent settingsHeaderContent = new GUIContent();

        #endregion

        #region Settings

        private static ContentType contentType;
        private static App.Content.Manager.Platform buildPlatform;

        #region Scene Data

        private static string contentName;

        private static ScenePropData sceneObjectData;
        private static SceneUIData sceneUIData;

        private static SceneAssetBundleData sceneAssetBundleData;

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

            settingsSectionContentTexture = new Texture2D(1, 1);
            settingsSectionContentTexture.SetPixel(0, 0, settingsSectionContentColor);
            settingsSectionContentTexture.Apply();

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
            sceneObjectData = CreateInstance<ScenePropData>();
            sceneUIData = CreateInstance<SceneUIData>();

            sceneAssetBundleData = CreateInstance<SceneAssetBundleData>();
        }

        #endregion

        #region Main

        private void OnWindowUpdates()
        {
            DrawLayouts();
            DrawSettingsLayout();
        }

        private void DrawLayouts()
        {
            #region Header Section

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

            #region Settings Section

            settingsSectionRect.x = 0;
            settingsSectionRect.y = headerSectionRect.height;
            settingsSectionRect.width = Screen.width;
            settingsSectionRect.height = Screen.height - headerSectionRect.height;

            GUI.DrawTexture(settingsSectionRect, settingsSectionTexture);

            float settingsSectionContentX = Screen.width - (Screen.width / 4);

            settingsSectionContentRect.x = 15;
            settingsSectionContentRect.y = settingsSectionRect.y;
            settingsSectionContentRect.width = settingsSectionContentX;
            settingsSectionContentRect.height = settingsSectionRect.height;

            GUI.DrawTexture(settingsSectionContentRect, settingsSectionContentTexture);

            #endregion
        }

        private void DrawSettingsLayout()
        {
            GUILayout.BeginArea(settingsSectionContentRect);
            GUILayout.Space(25);

            GUILayout.Label("This tool creates app scene content to be loadede at runtime.");
            GUILayout.Space(15);

            contentName = EditorGUILayout.TextField("Content Name", contentName);
            GUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();
            
            contentType = (ContentType)EditorGUILayout.EnumPopup("Content Type", contentType);

            EditorGUILayout.EndHorizontal();

            GUILayout.Space(15);

            switch (contentType)
            {
                case ContentType.SceneProp:

                    sceneObjectData.nameTag = contentName;
                    GUILayout.Space(10);

                    sceneObjectData.prefab = EditorGUILayout.ObjectField("Object Prefab", sceneObjectData.prefab, typeof(GameObject), false) as GameObject;
                    GUILayout.Space(10);

                    SerializedObject serializedObject = new SerializedObject(sceneObjectData);
                    SerializedProperty serializedProperty = serializedObject.FindProperty("contentDescription");
                    EditorGUILayout.PropertyField(serializedProperty, true);
                    serializedObject.ApplyModifiedProperties();
                    GUILayout.Space(5);

                    sceneObjectData.interactable = EditorGUILayout.Toggle("Interactable", sceneObjectData.interactable);
                    GUILayout.Space(5);

                    if (sceneObjectData.interactable)
                    {
                        SerializedObject serializedObjectInteractions = new SerializedObject(sceneObjectData);
                        SerializedProperty serializedPropertyInteractions = serializedObjectInteractions.FindProperty("interactionData");
                        EditorGUILayout.PropertyField(serializedPropertyInteractions, true);
                        serializedObjectInteractions.ApplyModifiedProperties();
                        GUILayout.Space(5);
                    }

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

                case ContentType.SceneAssetBundle:

                    SerializedObject serializedSceneAssetBundle = new SerializedObject(sceneAssetBundleData); ;
                    SerializedProperty serializedSceneAssetBundleProperty = serializedSceneAssetBundle.FindProperty("contentBundle");
                    EditorGUILayout.PropertyField(serializedSceneAssetBundleProperty, true);
                    serializedSceneAssetBundle.ApplyModifiedProperties();
                    GUILayout.Space(10);

                    sceneAssetBundleData.buildPlatform = (Platform)EditorGUILayout.EnumPopup("Build Platform", sceneAssetBundleData.buildPlatform);

                    break;
            }

            GUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Create", GUILayout.Height(25)))
            {
                CreateContent(contentType);
            }

            if (GUILayout.Button("Clear", GUILayout.Height(25)))
            {
                ClearContent(contentType);
            }

            EditorGUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        private void CreateContent(ContentType contentType)
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Scene Content", contentName, "asset", "Save Created Scene Content");

            switch (contentType)
            {
                case ContentType.SceneProp:

                    if(!string.IsNullOrEmpty(path))
                    {
                        AssetDatabase.CreateAsset(sceneObjectData, path);
                        UnityEngine.Debug.Log($"-->> <color=white>A scene [<color=grey>Object</color>]  content named  :</color> <color=cyan>[ {contentName} ]</color><color=white>.has been created successfully at directory :</color> <color=orange>{path}</color><color=white>.</color>");
                        AssetDatabase.Refresh();

                        EditorUtility.FocusProjectWindow();
                    }

                    break;

                case ContentType.SceneUI:

                    if (!string.IsNullOrEmpty(path))
                    {

                        AssetDatabase.CreateAsset(sceneUIData, path);
                        UnityEngine.Debug.Log($"-->> <color=white>A scene [<color=grey>UI</color>] content named  :</color> <color=cyan>[ {contentName} ]</color><color=white>.has been created successfully at directory :</color> <color=orange>{path}</color><color=white>.</color>");
                        AssetDatabase.Refresh();

                        EditorUtility.FocusProjectWindow();
                    }

                    break;

                case ContentType.SceneAssetBundle:

                    if (!string.IsNullOrEmpty(path))
                    {
                        UnityEngine.Object[] list = new UnityEngine.Object[5];

                        if(sceneAssetBundleData.buildPlatform == Platform.Any)
                        {
                            BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
                        }
                        else
                        {
                            switch (sceneAssetBundleData.buildPlatform)
                            {
                                case Platform.Android:

                                    BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.Android);

                                    break;

                                case Platform.iOS:

                                    BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.iOS);

                                    break;

                                case Platform.LinuxStandalone:

                                    BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneLinux64);

                                    break;

                                case Platform.MacOSStandalone:

                                    BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

                                    break;

                                case Platform.WindowsStandalone:

                                    BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneOSX);

                                    break;
                            }
                        }

                        EditorUtility.FocusProjectWindow();
                    }

                    break;
            }
        }

        private static void UpdateWindowToSelectedContent(ScenePropData objectData)
        {
            contentType = (ContentType)EditorGUILayout.EnumPopup("Content Type", objectData.contentType);
            contentName = EditorGUILayout.TextField("Content Name", objectData.nameTag);

            sceneObjectData.prefab = EditorGUILayout.ObjectField("Object Prefab", objectData.prefab, typeof(GameObject), false) as GameObject;
            GUILayout.Space(5);

            sceneObjectData.contentDescription = objectData.contentDescription;
            GUILayout.Space(5);

            sceneObjectData.interactable = objectData.interactable;
            sceneObjectData.interactionData = objectData.interactionData;
            GUILayout.Space(5);

            sceneObjectData.interactable = EditorGUILayout.Toggle("Interactable", sceneObjectData.interactable);
            GUILayout.Space(5);

            sceneObjectData.enableOnLoad = EditorGUILayout.Toggle("Enable Content On Load", objectData.enableOnLoad);
            sceneObjectData.contentType = contentType;
        }

        private static void UpdateWindowToSelectedContent(SceneUIData uiData)
        {
            contentType = (ContentType)EditorGUILayout.EnumPopup("Content Type", uiData.contentType);
            contentName = EditorGUILayout.TextField("Content Name", uiData.nameTag);

            sceneUIData.prefab = EditorGUILayout.ObjectField("UI Prefab", uiData.prefab, typeof(GameObject), false) as GameObject;
            sceneUIData.enableOnLoad = EditorGUILayout.Toggle("Enable Content On Load", uiData.enableOnLoad);
            sceneUIData.screenType = (ScreenType)EditorGUILayout.EnumPopup("Screen Type", uiData.screenType);
            sceneUIData.uiType = (UIType)EditorGUILayout.EnumPopup("UI Type", uiData.uiType);
            sceneUIData.contentType = contentType;
        }

        private void ClearContent(ContentType contentType)
        {

            contentName = EditorGUILayout.TextField("Content Name", string.Empty);

            switch (contentType)
            {
                case ContentType.SceneProp:

                    sceneObjectData.nameTag = string.Empty;
                    GUILayout.Space(10);
                    sceneObjectData.prefab = EditorGUILayout.ObjectField("Object Prefab", null, typeof(GameObject), false) as GameObject;
                    GUILayout.Space(10);
                    sceneObjectData.enableOnLoad = EditorGUILayout.Toggle("Enable Content On Load", false);
                    sceneObjectData.contentType = contentType;

                    break;

                case ContentType.SceneUI:

                    sceneUIData.nameTag = string.Empty;
                    GUILayout.Space(10);
                    sceneUIData.prefab = EditorGUILayout.ObjectField("UI Prefab", null, typeof(GameObject), false) as GameObject;
                    GUILayout.Space(10);
                    sceneUIData.screenType = (ScreenType)EditorGUILayout.EnumPopup("Screen Type", ScreenType.Loading);
                    GUILayout.Space(10);
                    sceneUIData.uiType = (UIType)EditorGUILayout.EnumPopup("UI Type", UIType.UIButton);
                    GUILayout.Space(10);
                    sceneUIData.enableOnLoad = EditorGUILayout.Toggle("Enable Content On Load", false);
                    sceneUIData.contentType = contentType;

                    break;
            }
        }

        #endregion
    }
}
