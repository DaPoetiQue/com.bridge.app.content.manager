using UnityEngine;

namespace Bridge.Core.App.Content.Manager
{
    [CreateAssetMenu(fileName = "Scene Object", menuName = "3ridge/Content/Scene Object")]
    public class SceneObjectData : Content
    {
        #region Components

        public bool interactable;

        [Space(5)]
        public InteractionData interactionData;

        [Space(5)]
        public ObjectData contentDescription;

        #endregion
    }
}
