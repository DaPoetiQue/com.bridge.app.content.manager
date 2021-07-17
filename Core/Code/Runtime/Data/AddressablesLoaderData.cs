using UnityEngine;

namespace Bridge.Core.App.Content.Manager
{
    [CreateAssetMenu(fileName = "Addressables Data", menuName = "3ridge/Content Load Data/Addressables")]
    public class AddressablesLoaderData : SceneContentLoader
    {
        #region Components

        public string label;

        #endregion
    }
}
