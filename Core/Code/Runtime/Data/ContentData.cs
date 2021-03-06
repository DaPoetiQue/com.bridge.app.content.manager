using System;
using System.Collections.Generic;
using UnityEngine;
using Bridge.Core.Debug;

namespace Bridge.Core.App.Content.Manager
{
    #region Options

    public enum ContentType
    {
        SceneProp = 0, SceneUI = 1, SceneAssetBundle = 2
    }

    public enum LoadType
    {
        Addressables = 1, Inspector = 2, Resources = 3, StreamingAssets = 4
    }

    public enum SelectableLoadType
    {
        Everything = 0, Addressables = 1, Inspector = 2, Resources = 3, StreamingAssets = 4
    }

    public enum Platform
    {
        Any, Android, iOS, LinuxStandalone, MacOSStandalone, WindowsStandalone, Editor
    }

    public enum UIType
    {
        UIButton = 0, UIScreen = 1, UIScrollView = 2
    }

    public enum ScreenType
    {
        Loading = 0, Menu = 1, Scene = 2
    }

    public enum SceneObjectSpace
    {
        Local, World
    }

    #endregion

    #region Scriptable Content Data

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

    public class SceneContentLoadData : ScriptableObject
    {
        public string nameTag;

        [Space(5)]
        public LoadType loadType;

        [Space(5)]
        public Platform platform;

        [Space(5)]
        public Description description;
    }

    #region Content Classes

    public class ObjectData : MonoDebug
    {
        [Space(15)]
        public string nameTag;

        [Space(15)]
        public GameObject asset;
    }

    #endregion

    #region Content Structs


    [Serializable]
    public struct SceneContentLoader
    {
        public string nameTag;

        [Space(5)]
        public SceneContentGroup sceneContentGroup;

        [Space(5)]
        public SceneContentLoadData sceneContentLoadData;

        [Space(5)]
        [TextArea]
        public string description;

        [HideInInspector]
        public bool contentLoaded;
    }

    #endregion

    #endregion

    #region Object

    #region Data

    [Serializable]
    public struct Description
    {
        [Space(5)]
        public List<Sprite> imageTag;

        [Space(5)]
        [TextArea]
        public string comment;
    }

    #endregion

    #region Scene

    public struct Pose
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
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
