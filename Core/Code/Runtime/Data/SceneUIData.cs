using UnityEngine;

namespace Bridge.Core.App.Content.Manager
{
    [CreateAssetMenu(fileName = "Scene UI", menuName = "3ridge/Content/Scene UI")]
    public class SceneUIData : Content
    {
        #region Components

        [SerializeField]
<<<<<<< HEAD
        public UIType uiType;

        [Space(5)]
        public ScreenType screenType;
=======
        private readonly new UIType type;
>>>>>>> 36f5806b4be4cd51f14387c281d02ad4a9f150fe

        #endregion
    }
}
