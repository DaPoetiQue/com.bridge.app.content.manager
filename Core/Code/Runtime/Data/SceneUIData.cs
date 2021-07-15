using UnityEngine;

namespace Bridge.Core.App.Content.Manager
{
    [CreateAssetMenu(fileName = "Scene UI", menuName = "3ridge/Content/Scene UI")]
    public class SceneUIData : Content
    {
        #region Components

        [SerializeField]
        private readonly new UIType type;

        #endregion
    }
}
