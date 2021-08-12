using UnityEngine;
using UnityEditor;

namespace Bridge.Core.UnityEditor.Content.Manager
{
    public class ContentLoadManagerWindow : EditorWindow
    {
        #region Window Initialization

        [MenuItem("3ridge/Create/Content")]
        private static void CreateContentLoadManagerWindow()
        {
            ContentLoadManagerWindow window = GetWindow<ContentLoadManagerWindow>();
            window.name = "3ridge App Content";
            window.minSize = new Vector2(300.0f, 400.0f);
            window.Show();
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

        #region Unity

        private void OnEnable() => Init();
        private void OnGUI() => OnWindowUpdates();

        #endregion

        #region Initializations

        private void Init()
        {
            InitializeTextures();
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

        private void InitializeContentData()
        {

        }

        #endregion

        #region Main

        /// <summary>
        /// Draws window layouts.
        /// </summary>
        private void OnWindowUpdates()
        {
            DrawLayouts();
            DrawHeaderLayout();
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
            iconRect.x = (Screen.width / 2) - iconRect.width;
            iconRect.y = 0;
           

            #endregion

            #region Settings

            settingsSectionRect.x = 0;
            settingsSectionRect.y = headerSectionRect.height;
            settingsSectionRect.width = Screen.width;
            settingsSectionRect.height = Screen.height - headerSectionRect.height;

            GUI.DrawTexture(settingsSectionRect, settingsSectionTexture);

            #endregion
        }

        private void DrawHeaderLayout()
        {
            GUILayout.BeginArea(headerSectionRect);

            GUI.DrawTexture(iconRect, iconTexture);

            GUILayout.EndArea();
        }

        private void DrawSettingsLayout()
        {
            GUILayout.BeginScrollView(settingsSectionRect.position);

            GUILayout.EndScrollView();
        }

        #endregion
    }
}
