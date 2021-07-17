using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bridge.Core.App.Content.Manager
{
    #region Options

    public enum ContentType
    {
<<<<<<< HEAD
        SceneObject = 0, SceneUI = 1
=======
        SceneObject, SceneUI
>>>>>>> 107b66fe270f0597aba1222dac05d5b1ef666344
    }

    public enum LoadType
    {
<<<<<<< HEAD
        Addressables = 0, Inspector = 1, Resources = 2, StreamingAssets = 3
    }
    public enum UIType
    {
        UIButton = 0, UIScreen = 1, UIScrollView = 2
=======
        Addressables, Inspector, Resources, StreamingAssets
    }
    public enum UIType
    {
        UIButton, UIScreen, UIScrollView
>>>>>>> 107b66fe270f0597aba1222dac05d5b1ef666344
    }

    public enum ScreenType
    {
<<<<<<< HEAD
        Loading = 0, Menu = 1, Scene = 2
=======
        Loading, Menu, Scene
>>>>>>> 107b66fe270f0597aba1222dac05d5b1ef666344
    }


    #endregion

    #region Content Data

    public class Content : ScriptableObject
    {
        public string nameTag;

        [Space(5)]
        public GameObject prefab;

        [Space(5)]
<<<<<<< HEAD
        public ContentType contentType;
=======
<<<<<<< HEAD
        public ContentType contentType;
=======
        public ContentType type;
>>>>>>> 36f5806b4be4cd51f14387c281d02ad4a9f150fe
>>>>>>> 107b66fe270f0597aba1222dac05d5b1ef666344

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
