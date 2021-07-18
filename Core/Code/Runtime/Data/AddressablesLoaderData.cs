using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Bridge.Core.App.Content.Manager
{
    [CreateAssetMenu(fileName = "Addressables Data", menuName = "3ridge/Content Load Data/Addressables")]
    public class AddressablesLoaderData : SceneContentLoadData
    {
        #region Components

        public IList<IResourceLocation> ResourceLocations = new List<IResourceLocation>();

        public string label;

        #endregion
    }
}
