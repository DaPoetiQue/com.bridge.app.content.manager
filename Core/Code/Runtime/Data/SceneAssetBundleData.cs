using UnityEngine;

namespace Bridge.Core.App.Content.Manager
{
    public class SceneAssetBundleData : ScriptableObject
    {
        [HideInInspector]
        public string nameTag;

        [Space(5)]
        public Content contentBundle;

        [Space(5)]
        public Platform buildPlatform;
    }
}
