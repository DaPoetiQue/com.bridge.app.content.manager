using UnityEngine;

namespace Bridge.Core.App.Content.Manager
{
    [CreateAssetMenu(fileName = "Streaming Data", menuName = "3ridge/Content Load Data/Streaming")]
    public class StreamingAssetsLoaderData : SceneContentLoader
    {
        #region Components

        [Space(5)]
        public string path;

        #endregion
    }
}
