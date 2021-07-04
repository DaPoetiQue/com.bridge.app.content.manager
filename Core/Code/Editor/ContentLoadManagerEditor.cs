using UnityEditor;

namespace Bridge.Core.Contents
{
    public class ContentLoadManagerEditor : Editor
    {
        [MenuItem("Bridge/Create/Content Load Manager")]
        private static void CreateContentLoadManager()
        {
            var contentLoadManager = new UnityEngine.GameObject("_Content Load Manager");
            contentLoadManager.AddComponent<ContentLoadManager>();

            if(Selection.activeGameObject != null) contentLoadManager.transform.SetParent(Selection.activeGameObject.transform);

            UnityEngine.Debug.Log("<color=white>-->></color> <color=green> Success </color>:<color=white> A content load manager has been created successfully.</color>");
        }

        [MenuItem("Bridge/Create/Content Load Manager", true)]
        private static bool CanCreateContentLoadManager()
        {
            return FindObjectOfType<ContentLoadManager>() == null;
        }
    }
}
