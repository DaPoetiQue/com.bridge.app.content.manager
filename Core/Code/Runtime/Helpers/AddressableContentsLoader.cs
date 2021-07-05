using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.AddressableAssets;

namespace Bridge.Core.App.Content.Manager
{
    public static class AddressableContentsLoader
    {
        #region Main
        public static async Task GellAllAssetsLocations(string label, IList<IResourceLocation> resourceLocations)
        {
            var loadedLocations = await Addressables.LoadResourceLocationsAsync(label).Task;

            foreach (var resourceLocation in loadedLocations)
            {
                resourceLocations.Add(resourceLocation);
            }
        }

        public static async Task LoadAssetsFromLocations<T>(IList<IResourceLocation> resourceLocation, List<T> prefabs) where T : Object
        {
            foreach (var _Location in resourceLocation)
            {
                prefabs.Add(await Addressables.InstantiateAsync(_Location).Task as T);
            }
        }

        #endregion
    }
}
