using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine;
using Bridge.Core.Debug;
using Bridge.Core.App.Events;
using UnityEngine.Networking;

namespace Bridge.Core.App.Content.Manager
{
    public class SceneContentManager : MonoDebug
    {
        #region  Components

        [Space(15)]
        [SerializeField]
        private SelectableLoadType selectLoadType;

        [Space(5)]
        [SerializeField]
        private List<SceneContentLoader> sceneContentLoader = new List<SceneContentLoader>();
        private int sceneContentCount = 0;
        private List<SceneContentGroup> loadedSceneContentGroups = new List<SceneContentGroup>();

        private bool isLoadingContentData;

        private ContentLoader contentLoader = new ContentLoader();

        private float minLoadTimeOut = 2.0f, maxLoadTimeOut = 4.0f;

        private float timeOut = 0.0f;

        private AppEventsData.AppViewState initialAppView;

        public IList<IResourceLocation> ResourceLocations = new List<IResourceLocation>();

        private bool isAppContentLoaded = false;

        #endregion

        #region Unity Defaults

        private void Awake()
        {
            SubscribeToAppEvents();
        }

        private void Start()
        {
            Init();
        }

        #endregion

        #region Main

        private void SubscribeToAppEvents()
        {
            EventsManager.Instance.OnAppInitializedEvent.AddListener(Init);
            EventsManager.Instance.OnSelectableUIEvent.AddListener(OnSelectableUIEvent);
        }

        private void Init()
        {
            if (sceneContentLoader.Count <= 0) return;

            LoadContentData(initialAppView, success => 
            {
                Log(LogData.LogLevel.Success, this, $"Scene content load completed : {sceneContentCount} content(s) found.");
            });
        }

        private void LoadContentData(AppEventsData.AppViewState appView, Action<bool> contentLoaded)
        {
            foreach (SceneContentLoader loader in sceneContentLoader)
            {
                if(loader.sceneContentGroup == null)
                {
                    Log(LogData.LogLevel.Error, this, $"There is no Scene Content object assigned for content loader {loader.nameTag}. At index {sceneContentLoader.IndexOf(loader)}");
                    return;
                }

                if(selectLoadType == SelectableLoadType.Everything)
                {
                    switch (loader.sceneContentLoadData.loadType)
                    {
                        case LoadType.Addressables:

                            if(!loader.contentLoaded)
                            {
                                LoadAddressablesData(loader.sceneContentGroup, (AddressablesLoaderData)loader.sceneContentLoadData, sceneContentLoader.IndexOf(loader), success =>
                                {
                                    contentLoaded.Invoke(success);
                                });
                            }

                            break;

                        case LoadType.Inspector:

                            if (!loader.contentLoaded)
                            {
                                LoadInspectorData(loader.sceneContentGroup, (InspectorLoaderData)loader.sceneContentLoadData, sceneContentLoader.IndexOf(loader), success =>
                                {
                                    contentLoaded.Invoke(success);
                                });
                            }

                            break;

                        case LoadType.Resources:

                            if (!loader.contentLoaded)
                            {
                                LoadResourcesData(loader.sceneContentGroup, (ResourcesLoaderData)loader.sceneContentLoadData, sceneContentLoader.IndexOf(loader), success =>
                                {
                                    contentLoaded.Invoke(success);
                                });
                            }

                            break;

                        case LoadType.StreamingAssets:

                            if (!loader.contentLoaded)
                            {
                                LoadStreamingData(loader.sceneContentGroup, (StreamingAssetsLoaderData)loader.sceneContentLoadData, sceneContentLoader.IndexOf(loader), success =>
                                {
                                    contentLoaded.Invoke(success);
                                });
                            }

                            break;
                    }
                }
                else
                {

                }
            }
        }

        #region Load Data

        private async void LoadAddressablesData(SceneContentGroup sceneContent, AddressablesLoaderData loaderData, int loaderQueueID, Action<bool> callBack)
        {
            if(string.IsNullOrEmpty(loaderData.label))
            {
                Log(LogData.LogLevel.Error, this, $"[ Addressables Load Data ] label not assigned for loader at index : {loaderQueueID}.");
                return;
            }

            sceneContent.loadedContent = new List<ObjectData>();
            List<Content> loadedContent = new List<Content>();

            await AddressableContentsLoader.GellAllAssetsLocations(loaderData.label, loaderData.ResourceLocations);
            await AddressableContentsLoader.LoadAssetsFromLocations(loaderData.ResourceLocations, loadedContent);

            if (loadedContent.Count <= 0)
            {
                Log(LogData.LogLevel.Error, this, $"[ Addressables Load Data ] Content not found using label {loaderData.label}. Failed to load content at index : {loaderQueueID}.");
                return;
            }

            CreateSceneContent(loaderData.nameTag, sceneContent, loadedContent, created =>
            {
                callBack.Invoke(created);
            });
        }

        private void LoadInspectorData(SceneContentGroup sceneContent, InspectorLoaderData loaderData, int loaderQueueID, Action<bool> callBack)
        {
            sceneContent.loadedContent = new List<ObjectData>();

            if(loaderData.ContentToLoad.Count <= 0)
            {
                Log(LogData.LogLevel.Error, this, $"[ Inspector Load Data ] There is no Content To Load data assigned in the inspector at index : {loaderQueueID}.");
                return;
            }

            CreateSceneContent(loaderData.nameTag, sceneContent, loaderData.ContentToLoad, created =>
            {
                callBack.Invoke(created);
            });
        }

        private void LoadResourcesData(SceneContentGroup sceneContent, ResourcesLoaderData loaderData, int loaderQueueID, Action<bool> callBack)
        {
            Log(LogData.LogLevel.Debug, this, $"Loading [ Resources Load Data ] at index : {loaderQueueID}.");

            if (string.IsNullOrEmpty(loaderData.contentDirectory))
            {
                Log(LogData.LogLevel.Error, this, $"Content directory missing/not assigned for [ Resources Load Data ] at index : {loaderQueueID}.");
                return;
            }

            sceneContent.loadedContent = new List<ObjectData>();
            List<Content> loadedContent = new List<Content>();

            loadedContent = Resources.LoadAll<Content>(loaderData.contentDirectory).ToList();

            if (loadedContent.Count <= 0)
            {
                Log(LogData.LogLevel.Error, this, $"[ Streaming Assets Load Data ] Content not found at directory : {loaderData.contentDirectory}. Failed to load content at index : {loaderQueueID}.");
                return;
            }

            CreateSceneContent(loaderData.nameTag, sceneContent, loadedContent, created => 
            {
                callBack.Invoke(created);
            });
        }

        private void LoadStreamingData(SceneContentGroup sceneContent, StreamingAssetsLoaderData loaderData, int loaderQueueID, Action<bool> callBack)
        {

            if (string.IsNullOrEmpty(loaderData.contentDirectory))
            {
                Log(LogData.LogLevel.Error, this, $"Content directory missing/not assigned for [ Streaming Assets Load Data ] at index : {loaderQueueID}.");
                return;
            }

            sceneContent.loadedContent = new List<ObjectData>();
            List<Content> loadedContent = new List<Content>();

            StartCoroutine(GetStreamedAsset(GetAssetsDirectory(loaderData.contentDirectory)));
        }

        private IEnumerator GetStreamedAsset(string directory)
        {
            var content = AssetBundle.LoadFromFileAsync(directory);

            yield return content;

            var loadedContent = content.assetBundle;

            if(loadedContent == null)
            {
                Log(LogData.LogLevel.Error, this, $"Downloading [ Streaming Assets Data ]. From directory : {directory} Failed.");
            }

            yield return null;

            StopCoroutine("GetStreamedAsset");
        }

        private string GetAssetsDirectory(string relativeDirectory)
        {
            return "file://" + Path.Combine(Application.streamingAssetsPath, relativeDirectory);
        }

        #endregion

        #region Create Scene Content

        private async void CreateSceneContent(string nameTag, SceneContentGroup sceneContent, List<Content> loadedContent, Action<bool> callBack)
        {
            sceneContent.contentCount = loadedContent.Count;

            foreach (Content content in loadedContent)
            {
                if (content?.prefab)
                {
                    await CreateSceneContentTask(content.name, content.contentType, sceneContent, content?.prefab, content.enableOnLoad);
                }
            }

            if (sceneContent.loadedContent.Count != sceneContent.contentCount)
            {
                Log(LogData.LogLevel.Warning, this, $"[ {nameTag} ] content not fully loaded.");
                return;
            }

            if (!loadedSceneContentGroups.Contains(sceneContent))
            {
                loadedSceneContentGroups.Add(sceneContent);
                callBack.Invoke(true);
            }
        }

        private async Task CreateSceneContentTask(string contentName, ContentType contentType, SceneContentGroup sceneContent, GameObject prefab, bool enabled)
        {
            GameObject createdContent = Instantiate(prefab, sceneContent.transform);
            createdContent.name = contentName;

            if (contentType == ContentType.SceneProp) createdContent.AddComponent<SceneProp>();
            if (contentType == ContentType.SceneUI) createdContent.AddComponent<SceneUI>();

            if (!sceneContent.loadedContent.Contains(createdContent.GetComponent<ObjectData>())) sceneContent.loadedContent.Add(createdContent.GetComponent<ObjectData>());

            createdContent.SetActive(enabled);

            sceneContentCount++;

            await Task.Yield();
        }

        #endregion

        #endregion

        #region Courotines

        private IEnumerator OnLoad(AppEventsData.AppViewState appView)
        {
            Log(LogData.LogLevel.Debug, this, "Content started loading.");

            while(timeOut > 0.0f)
            {
                timeOut -= 1.0f * Time.deltaTime;

                yield return null;
            }

            Log(LogData.LogLevel.Success, this, "Content loading has completed.");

            EventsManager.Instance.OnAppViewChangedEvent.Invoke(appView);

            yield return null;

            StopCoroutine("OnLoad");
        }

        #endregion

        #region App Events

        private void OnSelectableUIEvent(AppEventsData.SelectionType selection, AppEventsData.AppViewState appView)
        {
            if(appView == AppEventsData.AppViewState.None) return;

            EventsManager.Instance.OnAppViewChangedEvent.Invoke(AppEventsData.AppViewState.LoadingView);

            LoadContentData(appView, loaded => 
            {
                Log(LogData.LogLevel.Success, this, "Content load completed with no errors.");
            });
        }

        #endregion
    }
}
