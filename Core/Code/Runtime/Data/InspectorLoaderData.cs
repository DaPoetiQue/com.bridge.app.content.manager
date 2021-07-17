using System.Collections.Generic;
using UnityEngine;

namespace Bridge.Core.App.Content.Manager
{
    [CreateAssetMenu(fileName = "Inspector Data", menuName = "3ridge/Content Load Data/Inspector")]
    public class InspectorLoaderData : SceneContentLoader
    {
        #region Components

        [Space(5)]
        public List<Content> ContentToLoad;

        #endregion
    }
}
