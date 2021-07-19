using UnityEngine;

namespace Bridge.Core.App.Content.Manager
{
    [CreateAssetMenu(fileName = "Resources Data", menuName = "3ridge/Content Load Data/Resources")]
    public class ResourcesLoaderData : SceneContentLoadData
    {
        #region Components

        [Space(5)]
        public string contentDirectory;

        #endregion
    }
}
