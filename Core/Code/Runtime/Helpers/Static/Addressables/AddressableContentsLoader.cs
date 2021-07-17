using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.AddressableAssets;
using UnityEngine;

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

        public static async Task LoadAssetsFromLocations<T>(IList<IResourceLocation> resourceLocation, List<T> content) where T : Object
        {
            foreach (var _Location in resourceLocation)
            {
<<<<<<< HEAD:Core/Code/Runtime/Helpers/Static/Addressables/AddressableContentsLoader.cs
                content.Add(await Addressables.LoadAssetAsync<T>(_Location).Task as T);
=======
<<<<<<< HEAD:Core/Code/Runtime/Helpers/Static/Addressables/AddressableContentsLoader.cs
                content.Add(await Addressables.LoadAssetAsync<T>(_Location).Task as T);
=======
                T content = await Addressables.LoadAssetAsync<T>(_Location).Task as T;
                prefabs.Add(content);
>>>>>>> 36f5806b4be4cd51f14387c281d02ad4a9f150fe:Core/Code/Runtime/Helpers/AddressableContentsLoader.cs
>>>>>>> 107b66fe270f0597aba1222dac05d5b1ef666344:Core/Code/Runtime/Helpers/AddressableContentsLoader.cs
            }
        } 

        #endregion
    }
}
