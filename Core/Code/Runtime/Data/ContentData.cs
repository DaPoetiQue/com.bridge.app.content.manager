using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bridge.Core.App.Content.Manager
{
    #region Options

    public enum ContentType
    {
        SceneObject, SceneUI
    }

    public enum LoadType
    {
        Addressables, Inspector, Resources, StreamingAssets
    }
    public enum UIType
    {
        UIButton, UIScreen, UIScrollView
    }

    public enum ScreenType
    {
        Loading, Menu, Scene
    }


    #endregion

    #region Content Data

    public class Content : ScriptableObject
    {
        public string nameTag;

        [Space(5)]
        public GameObject prefab;

        [Space(5)]
        public ContentType contentType;

        [Space(5)]
        public bool enableOnLoad;
    }

    #endregion

    #region Content Loader Data

    [Serializable]
    public class ContentLoader
    {
        [Space(5)]
        public LoadType loadType;

        [Space(5)]
        public List<AddressablesLoader> addressablesLoader;

        [Space(5)]
        public List<InspectorLoader> inspectorLoader;

        [Space(5)]
        public List<ResourcesLoader> resourcesLoader;

        [Space(5)]
        public List<StreamingAssetsLoader> streamingAssetsLoader;
    }

    [Serializable]
    public class AddressablesLoader
    {
        public string nameTag;

        [Space(5)]
        public string label;

        [HideInInspector]
        public List<Content> content;

        [Space(5)]
        public Transform contentContainer;

        [HideInInspector]
        public int contentCount;
    }

    [Serializable]
    public class InspectorLoader
    {
        public string nameTag;

        [Space(5)]
        public List<Content> content;

        [Space(5)]
        public Transform contentContainer;

        [HideInInspector]
        public int contentCount;
    }

    [Serializable]
    public class ResourcesLoader
    {
        public string nameTag;

        [Space(5)]
        public string path;

        [HideInInspector]
        public List<Content> content;

        [Space(5)]
        public Transform contentContainer;

        [HideInInspector]
        public int contentCount;
    }

    [Serializable]
    public class StreamingAssetsLoader
    {
        public string nameTag;

        [Space(5)]
        public string path;

        [HideInInspector]
        public List<Content> content;

        [Space(5)]
        public Transform contentContainer;

        [HideInInspector]
        public int contentCount;
    }

    #endregion
}
