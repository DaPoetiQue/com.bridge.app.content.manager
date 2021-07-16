using UnityEngine;

namespace Bridge.Core.App.Content.Manager
{
    [CreateAssetMenu(fileName = "Scene UI", menuName = "3ridge/Content/Scene UI")]
    public class SceneUIData : Content
    {
        #region Components

        [SerializeField]
        public UIType uiType;

        [Space(5)]
        public ScreenType screenType;

        #endregion
    }
}
