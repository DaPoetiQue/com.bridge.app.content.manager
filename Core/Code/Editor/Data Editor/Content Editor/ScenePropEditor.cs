using UnityEngine;
using UnityEditor;
using Bridge.Core.App.Content.Manager;

namespace Bridge.Core.UnityEditor.Content.Manager
{
    public class ScenePropEditor : Editor
    {
        #region Main

        [MenuItem("3ridge/Create/Content/Scene Prop")]
        private static void CreateSceneProp()
        {
            GameObject sceneProp = new GameObject("Scene Prop");
            sceneProp.AddComponent<SceneProp>();

            if(Selection.gameObjects.Length > 0)
            {
                sceneProp.transform.SetParent(Selection.gameObjects[0].transform);
            }
        }

        #endregion
    }
}
