using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Bridge.Core.App.Content.Manager;

namespace Bridge.Core.UnityEditor.Content.Manager
{
    public class ContentLoaderEditorWindow : EditorWindow
    {
        #region Window Initialization

        [MenuItem("3ridge/Editor/Content Loader")]
        private static void OpenContentLoaderWindow()
        {
            ContentLoaderEditorWindow window = GetWindow<ContentLoaderEditorWindow>("3ridge Content Loader");
            window.minSize = new Vector2(400.0f, 450.0f);
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

        private Color headerSectionColor = new Color(239.0f / 255.0f, 124.0f / 255.0f, 24.0f / 255.0f, 1.0f);
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

        private static LoadType loadType;
        private static App.Content.Manager.RuntimePlatform platform;

        #endregion

        #region Load Data

        private static AddressablesLoaderData addressablesLoaderData;
        private static InspectorLoaderData inspectorLoaderData;
        private static ResourcesLoaderData resourcesLoaderData;
        private static StreamingAssetsLoaderData streamingAssetsLoaderData;

        #endregion

        #region Loader Data

        private static string loaderName;

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
            settingsHeaderContent.text = "Scene Content Loader";

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
            addressablesLoaderData = CreateInstance<AddressablesLoaderData>();
            inspectorLoaderData = CreateInstance<InspectorLoaderData>();
            resourcesLoaderData = CreateInstance<ResourcesLoaderData>();
            streamingAssetsLoaderData = CreateInstance<StreamingAssetsLoaderData>();
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

            GUILayout.Label("Creates a scene content loader for loading app runtime content.");
            GUILayout.Space(15);


            loaderName = EditorGUILayout.TextField("Loader Name", loaderName);
            GUILayout.Space(15);

            loadType = (LoadType)EditorGUILayout.EnumPopup("Loader Type", loadType);
            GUILayout.Space(10);

            switch(loadType)
            {
                case LoadType.Addressables:

                    addressablesLoaderData.nameTag = loaderName;
                    addressablesLoaderData.loadType = loadType;
                    addressablesLoaderData.platform = platform;

                    addressablesLoaderData.label = EditorGUILayout.TextField("Addressables Label", addressablesLoaderData.label);
                    GUILayout.Space(10);

                    break;

                case LoadType.Inspector:

                    inspectorLoaderData.nameTag = loaderName;
                    inspectorLoaderData.loadType = loadType;
                    inspectorLoaderData.platform = platform;

                    SerializedObject serializedObjectInspector = new SerializedObject(inspectorLoaderData);
                    SerializedProperty serializedPropertyInspector = serializedObjectInspector.FindProperty("ContentToLoad");
                    EditorGUILayout.PropertyField(serializedPropertyInspector, true);
                    serializedObjectInspector.ApplyModifiedProperties();
                    GUILayout.Space(10);

                    break;

                case LoadType.Resources:

                    resourcesLoaderData.nameTag = loaderName;
                    resourcesLoaderData.loadType = loadType;
                    resourcesLoaderData.platform = platform;

                    resourcesLoaderData.path = EditorGUILayout.TextField("Resources Path", resourcesLoaderData.path);
                    GUILayout.Space(10);

                    break;

                case LoadType.StreamingAssets:

                    streamingAssetsLoaderData.nameTag = loaderName;
                    streamingAssetsLoaderData.loadType = loadType;
                    streamingAssetsLoaderData.platform = platform;

                    streamingAssetsLoaderData.path = EditorGUILayout.TextField("Streaming Path", streamingAssetsLoaderData.path);
                    GUILayout.Space(10);

                    break;
            }

            platform = (App.Content.Manager.RuntimePlatform)EditorGUILayout.EnumPopup("Runtime Platform", platform);
            GUILayout.Space(20);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Create", GUILayout.Height(25)))
            {
                CreateContentLoader(loadType);
            }

            if (GUILayout.Button("Clear", GUILayout.Height(25)))
            {
                ClearContentLoader(loadType, false);
            }

            if (GUILayout.Button("Reset", GUILayout.Height(25)))
            {
                ClearContentLoader(loadType, true);
            }

            EditorGUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        private void CreateContentLoader(LoadType loaderType)
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Content Load Data", loaderName, "asset", "Save Created Scene Content Load Data");

            switch (loaderType)
            {
                case LoadType.Addressables:

                    if (!string.IsNullOrEmpty(path))
                    {
                        AssetDatabase.CreateAsset(addressablesLoaderData, path);
                        UnityEngine.Debug.Log($"-->> <color=white>A scene [<color=grey>Object</color>]  load data named  :</color> <color=cyan>[ {loaderName} ]</color><color=white>.has been created successfully at directory :</color> <color=orange>{path}</color><color=white>.</color>");
                        AssetDatabase.Refresh();

                        EditorUtility.FocusProjectWindow();
                    }

                    break;

                case LoadType.Inspector:

                    if (!string.IsNullOrEmpty(path))
                    {
                        AssetDatabase.CreateAsset(inspectorLoaderData, path);
                        UnityEngine.Debug.Log($"-->> <color=white>A scene [<color=grey>Object</color>]  load data named  :</color> <color=cyan>[ {loaderName} ]</color><color=white>.has been created successfully at directory :</color> <color=orange>{path}</color><color=white>.</color>");
                        AssetDatabase.Refresh();

                        EditorUtility.FocusProjectWindow();
                    }

                    break;

                case LoadType.Resources:

                    if (!string.IsNullOrEmpty(path))
                    {
                        AssetDatabase.CreateAsset(resourcesLoaderData, path);
                        UnityEngine.Debug.Log($"-->> <color=white>A scene [<color=grey>Object</color>]  load data named  :</color> <color=cyan>[ {loaderName} ]</color><color=white>.has been created successfully at directory :</color> <color=orange>{path}</color><color=white>.</color>");
                        AssetDatabase.Refresh();

                        EditorUtility.FocusProjectWindow();
                    }

                    break;

                case LoadType.StreamingAssets:

                    if (!string.IsNullOrEmpty(path))
                    {
                        AssetDatabase.CreateAsset(streamingAssetsLoaderData, path);
                        UnityEngine.Debug.Log($"-->> <color=white>A scene [<color=grey>Object</color>]  load data named  :</color> <color=cyan>[ {loaderName} ]</color><color=white>.has been created successfully at directory :</color> <color=orange>{path}</color><color=white>.</color>");
                        AssetDatabase.Refresh();

                        EditorUtility.FocusProjectWindow();
                    }

                    break;
            }
        }

        private void ClearContentLoader(LoadType loaderType, bool reset)
        {
            loaderName = EditorGUILayout.TextField("Loader Name", string.Empty);

            if(reset) loadType = (LoadType)EditorGUILayout.EnumPopup("Loader Type", LoadType.Addressables);

            switch (loaderType)
            {
                case LoadType.Addressables:

                    addressablesLoaderData.label = EditorGUILayout.TextField("Addressables Label", string.Empty);

                    break;

                case LoadType.Inspector:

                    inspectorLoaderData.ContentToLoad = new List<App.Content.Manager.Content>();

                    break;

                case LoadType.Resources:

                    resourcesLoaderData.path = EditorGUILayout.TextField("Resources Path", string.Empty);

                    break;

                case LoadType.StreamingAssets:

                    streamingAssetsLoaderData.path = EditorGUILayout.TextField("Streaming Path", string.Empty);

                    break;
            }
        }

        #endregion
    }
}
