using UnityEngine;

namespace Bridge.Core.App.Content.Manager
{
    [CreateAssetMenu(fileName = "Scene Prop", menuName = "3ridge/Content/Scene Prop")]
    public class ScenePropData : Content
    {
        #region Components

        public bool interactable;

        [Space(5)]
        public InteractionData interactionData;

        [Space(5)]
        public Description contentDescription;

        #endregion
    }
}
