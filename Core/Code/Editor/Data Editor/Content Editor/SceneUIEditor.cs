using UnityEngine;
using UnityEditor;
using Bridge.Core.App.Content.Manager;

namespace Bridge.Core.UnityEditor.Content.Manager
{
    public class SceneUIEditor : Editor
    {
        #region Main

        [MenuItem("3ridge/Create/Content/Scene UI")]
        private static void CreateSceneUI()
        {
            GameObject sceneProp = new GameObject("Scene UI");
            sceneProp.AddComponent<SceneUI>();

            if (Selection.gameObjects.Length > 0)
            {
                sceneProp.transform.SetParent(Selection.gameObjects[0].transform);
            }
        }

        #endregion
    }
}
