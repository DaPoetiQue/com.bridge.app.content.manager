using UnityEngine;
using UnityEditor;
using Bridge.Core.App.Content.Manager;

namespace Bridge.Core.UnityEditor.Content.Manager
{
    public class SceneContentGroupEditor : Editor
    {
        #region Main

        [MenuItem("3ridge/Create/Content/Scene Content Group")]
        private static void CreateSceneContent()
        {
            GameObject sceneContent = new GameObject("Scene Content Group");
            sceneContent.AddComponent<SceneContentGroup>();

            if(Selection.gameObjects.Length > 0)
            {
                sceneContent.transform.SetParent(Selection.gameObjects[0].transform);
            }
        }

        #endregion
    }
}
