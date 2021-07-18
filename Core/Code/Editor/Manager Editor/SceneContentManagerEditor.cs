using UnityEditor;
using Bridge.Core.App.Content.Manager;

namespace Bridge.Core.UnityEditor.Content.Manager
{
    public class SceneContentManagerEditor : Editor
    {
        [MenuItem("3ridge/Create/Content/Scene Content Manager")]
        private static void CreateContentLoadManager()
        {
            var contentLoadManager = new UnityEngine.GameObject("_3ridge Scene Content Manager");
            contentLoadManager.AddComponent<SceneContentManager>();

            if(Selection.activeGameObject != null) contentLoadManager.transform.SetParent(Selection.activeGameObject.transform);

            UnityEngine.Debug.Log("<color=white>-->></color> <color=green> Success </color>:<color=white> A content load manager has been created successfully.</color>");
        }

        [MenuItem("3ridge/Create/Content/Scene Content Manager", true)]
        private static bool CanCreateContentLoadManager()
        {
            return FindObjectOfType<SceneContentManager>() == null;
        }
    }
}
