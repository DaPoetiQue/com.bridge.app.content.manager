using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bridge.Core.App.Content.Manager
{
    #region Options

    public enum ContentType
    {
        SceneObject = 0, SceneUI = 1
    }

    public enum LoadType
    {
        Addressables = 0, Inspector = 1, Resources = 2, StreamingAssets = 3
    }
    public enum UIType
    {
        UIButton = 0, UIScreen = 1, UIScrollView = 2
    }

    public enum ScreenType
    {
        Loading = 0, Menu = 1, Scene = 2
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

    #region Object

    #region Data

    [Serializable]
    public struct ObjectData
    {
        public Sprite[] thumbnail;

        [Space(5)]
        [TextArea]
        public string description;
    }

    #endregion

    #region Scene

    public struct Pose
    {
        public Transform position;
        public Transform rotation;
    }

    #endregion

    #region Interactions

    [Serializable]
    public struct Interactions
    {
        public bool all, rotate, scale, translate;
    }

    #endregion

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
