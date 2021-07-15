using UnityEngine;

namespace Bridge.Core.App.Content.Manager
{
    [CreateAssetMenu(fileName = "Scene Object", menuName = "3ridge/Content/Scene Object")]
    public class SceneObjectData : Content
    {
        #region Components

        [SerializeField]
        private readonly new ScreenType type;

        #endregion
    }
}
