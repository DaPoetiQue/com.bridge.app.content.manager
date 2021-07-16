using UnityEditor;
using Bridge.Core.App.Content.Manager;

namespace Bridge.Core.UnityEditor.Content.Manager
{
    public class ContentLoadManagerEditor : Editor
    {
        [MenuItem("3ridge/Create/Content Load Manager")]
        private static void CreateContentLoadManager()
        {
            var contentLoadManager = new UnityEngine.GameObject("_3ridge Content Load Manager");
            contentLoadManager.AddComponent<ContentLoadManager>();

            if(Selection.activeGameObject != null) contentLoadManager.transform.SetParent(Selection.activeGameObject.transform);

            UnityEngine.Debug.Log("<color=white>-->></color> <color=green> Success </color>:<color=white> A content load manager has been created successfully.</color>");
        }

        [MenuItem("3ridge/Create/Content Load Manager", true)]
        private static bool CanCreateContentLoadManager()
        {
            return FindObjectOfType<ContentLoadManager>() == null;
        }
    }
}
