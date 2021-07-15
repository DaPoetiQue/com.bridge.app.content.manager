using System.Collections;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine;
using Bridge.Core.Debug;
using Bridge.Core.App.Events;

namespace Bridge.Core.App.Content.Manager
{
    public class ContentLoadManager : MonoDebug
    {
        #region  Components

        [SerializeField]
        private ContentLoader contentLoader = new ContentLoader();

         [Space(3)]
        [SerializeField]
        private float minLoadTimeOut = 2.0f, maxLoadTimeOut = 4.0f;

        private float timeOut = 0.0f;

        [Space(3)]
        [SerializeField]
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
            LoadAppContent(initialAppView);
        }

        private void LoadAppContent(AppEventsData.AppViewState appView)
        {
            timeOut = GetLoadTimeOut();

            if(!isAppContentLoaded)
            {
                switch(contentLoader.loadType)
                {
                    case LoadType.Addressables:

                    LoadAddressablesContent();

                    break;

                    case LoadType.Inspector:

                    StartCoroutine(OnLoadInspectorContent(appView));

                    break;

                    case LoadType.Resources:

                    StartCoroutine(OnLoadResourcesContent(appView));

                    break;

                    case LoadType.StreamingAssets:

                    StartCoroutine(OnLoadStreamingAssetsContent(appView));

                    break;
                }

                Log(LogData.LogLevel.Debug, this, "App Initialized.");
            }

            if(isAppContentLoaded)
            {
                StartCoroutine(OnLoad(appView));
            }
        }

        private async void LoadAddressablesContent()
        {
            if (isAppContentLoaded) return;

            if (contentLoader.addressablesLoader.Count <= 0)
            {
                Log(LogData.LogLevel.Error, this, $"Content loader for [ Addressables Loader ] not created/assigned in the inspector. Create and assign a loader.");
                return;
            }

            foreach(AddressablesLoader loader in contentLoader.addressablesLoader)
            {
                if(string.IsNullOrEmpty(loader.label))
                {
                    Log(LogData.LogLevel.Error, this, $"[ Addressables Loader ] Content load label not assigned for loader at index : {contentLoader.addressablesLoader.IndexOf(loader)}.");
                    return;
                }

                string loaderName = (string.IsNullOrEmpty(loader.nameTag))? loader.nameTag : $"Addressables Loader_00{contentLoader.addressablesLoader.IndexOf(loader) + 1}";

                if(!loader.contentContainer) 
                {
                    GameObject contentContainer = new GameObject($"[ {loaderName} ] Content Container");
                    loader.contentContainer = contentContainer.transform;
                    
                    Log(LogData.LogLevel.Debug, this, $"A content container has been created successfully for : [ {loaderName} ].");
                }

                List<Content> loadedContent = new List<Content>();

                await AddressableContentsLoader.GellAllAssetsLocations(loader.label, ResourceLocations);
                await AddressableContentsLoader.LoadAssetsFromLocations(ResourceLocations, loadedContent);

                if(loadedContent.Count <= 0)
                {
                    Log(LogData.LogLevel.Error, this, $"[ Addressables Loader ] Content not found using label {loader.label}. Failed to load content at index : {contentLoader.addressablesLoader.IndexOf(loader)}.");
                    return;
                }

                loader.contentCount = loadedContent.Count;

                foreach(Content content in loadedContent)
                {

                    if(content?.prefab)
                    {
                        GameObject createdContent = Instantiate(content?.prefab, loader.contentContainer);
                        createdContent.name = content.name;

                        createdContent.SetActive(content.enableOnLoad);
                    }
                }

                if(loader.contentContainer.childCount != loader.contentCount)
                {       
                    Log(LogData.LogLevel.Warning, this, $"[ {loader.nameTag} ] content not fully loaded.");
                    Invoke("LoadInspectorContent", 1.0f);
                }

                if(loader.contentContainer.childCount == loader.contentCount)
                {
                    isAppContentLoaded = true;

                    Log(LogData.LogLevel.Success, this, $"[ {loader.nameTag} ] content loaded successfully.");
                }
            }
        }

        private void LoadInspectorContent()
        {
                if (isAppContentLoaded) return;

                if(contentLoader.inspectorLoader.Count <= 0)
                {
                    Log(LogData.LogLevel.Error, this, $"Content loader for [ Inspector Loader ] not created/assigned in the inspector. Create and assign a loader.");
                    return;
                }

                foreach(InspectorLoader loader in contentLoader.inspectorLoader)
                {
                    if(loader.content.Count <= 0)
                    {
                        Log(LogData.LogLevel.Error, this, $"[ Inspector Loader ] Content loader not created/assigned for loader at index : {contentLoader.inspectorLoader.IndexOf(loader)}.");
                        return;
                    }

                    string loaderName = (string.IsNullOrEmpty(loader.nameTag))? loader.nameTag : $"Inspector Loader_00{contentLoader.inspectorLoader.IndexOf(loader) + 1}";

                    if(!loader.contentContainer) 
                    {
                        GameObject contentContainer = new GameObject($"[ {loaderName} ] Content Container");
                        loader.contentContainer = contentContainer.transform;
                        Log(LogData.LogLevel.Debug, this, $"A content container has been created successfully for : [ {loaderName} ].");
                    }

                    if(loader.contentContainer)
                    {
                        if(loader.content.Count <= 0)
                        {
                            Log(LogData.LogLevel.Error, this, $"[ {loaderName} ] content not created/assigned for loader at index : {contentLoader.inspectorLoader.IndexOf(loader)}.");
                            return;
                        }

                        loader.contentCount = loader.content.Count;

                        foreach(Content content in loader.content)
                        {
                            if(!content.prefab)
                            {
                                Log(LogData.LogLevel.Error, this, $"[ {loaderName} ] content prefab not assigned for loader at index : {contentLoader.inspectorLoader.IndexOf(loader)}.");
                                return;
                            }

                            GameObject createdContent = Instantiate(content.prefab, loader.contentContainer);

                            string contentName = (string.IsNullOrEmpty(content.nameTag))? content.nameTag : content.prefab.name;
                            createdContent.name = contentName;

                            createdContent.SetActive(content.enableOnLoad);
                        }

                        if(loader.contentContainer.childCount != loader.contentCount)
                        {
                            Log(LogData.LogLevel.Warning, this, $"[ {loader.nameTag} ] content not fully loaded.");
                            Invoke("LoadInspectorContent", 1.0f);
                        }

                        if(loader.contentContainer.childCount == loader.contentCount)
                        {
                            isAppContentLoaded = true;

                            Log(LogData.LogLevel.Success, this, $"[ {loader.nameTag} ] content loaded successfully.");
                        }
                    }
                }
        }

        private void LoadResourcesContent()
        {
            if (isAppContentLoaded) return;

            if (contentLoader.resourcesLoader.Count <= 0)
            {
                Log(LogData.LogLevel.Error, this, $"Content loader for [ Resources Loader ] not created/assigned in the inspector. Create and assign a loader.");
                return;
            }

            foreach(ResourcesLoader loader in contentLoader.resourcesLoader)
            {
                if(string.IsNullOrEmpty(loader.path))
                {
                    Log(LogData.LogLevel.Error, this, $"[ Resources Loader ] Content load path not assigned for loader at index : {contentLoader.resourcesLoader.IndexOf(loader)}.");
                    return;
                }   

                Content[] loadedContent = Resources.LoadAll<Content>(loader.path);

                if(loadedContent.Length <= 0)
                {
                    Log(LogData.LogLevel.Error, this, $"[ Resources Loader ] Content not found at path {loader.path}. Path not found/invalid at index : {contentLoader.resourcesLoader.IndexOf(loader)}.");
                    return;
                } 

                loader.contentCount = loadedContent.Length;   

                string loaderName = (string.IsNullOrEmpty(loader.nameTag))? loader.nameTag : $"Resources Loader_00{contentLoader.resourcesLoader.IndexOf(loader) + 1}";

                if(!loader.contentContainer) 
                {
                    GameObject contentContainer = new GameObject($"[ {loaderName} ] Content Container");
                    loader.contentContainer = contentContainer.transform;
                    
                    Log(LogData.LogLevel.Debug, this, $"A content container has been created successfully for : [ {loaderName} ].");
                }    

                foreach(Content content in loadedContent)
                {
                    GameObject createdContent = Instantiate<GameObject>(content.prefab, loader.contentContainer);
                    createdContent.name = content.name;

                    createdContent.SetActive(content.enableOnLoad);
                }    

                if(loader.contentContainer.childCount != loader.contentCount)
                {       
                    Log(LogData.LogLevel.Warning, this, $"[ {loader.nameTag} ] content not fully loaded.");
                    Invoke("LoadResourcesContent", 1.0f);
                }

                if(loader.contentContainer.childCount == loader.contentCount)
                {
                    isAppContentLoaded = true;

                    Log(LogData.LogLevel.Success, this, $"[ {loader.nameTag} ] content loaded successfully.");
                }        
            }
        }

        private float GetLoadTimeOut()
        {
            return Random.Range(minLoadTimeOut, maxLoadTimeOut);
        }

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

        private IEnumerator OnLoadAddressableContent(AppEventsData.AppViewState appView)
        {
            Log(LogData.LogLevel.Debug, this, "Content started loading.");

            LoadAddressablesContent();

            while(timeOut > 0.0f)
            {
                if(timeOut <= 0.1f && !isAppContentLoaded)
                {
                    timeOut = GetLoadTimeOut();
                }

                timeOut -= 1.0f * Time.deltaTime;

                yield return null;
            }

            Log(LogData.LogLevel.Success, this, "Content loading has completed.");

            EventsManager.Instance.OnAppViewChangedEvent.Invoke(appView);

            yield return null;

            StopCoroutine("OnLoadAddressableContent");
        }

        private IEnumerator OnLoadInspectorContent(AppEventsData.AppViewState appView)
        {
            Log(LogData.LogLevel.Debug, this, "Content started loading.");

            LoadInspectorContent();

            while(timeOut > 0.0f)
            {
                if(timeOut <= 0.1f && !isAppContentLoaded)
                {
                    timeOut = GetLoadTimeOut();
                }

                timeOut -= 1.0f * Time.deltaTime;

                yield return null;
            }

            Log(LogData.LogLevel.Success, this, "Content loading has completed.");

            EventsManager.Instance.OnAppViewChangedEvent.Invoke(appView);

            yield return null;

            StopCoroutine("OnLoadInspectorContent");
        }

        private IEnumerator OnLoadResourcesContent(AppEventsData.AppViewState appView)
        {
            Log(LogData.LogLevel.Debug, this, "Content started loading.");

            LoadResourcesContent();

            while(timeOut > 0.0f)
            {
                if(timeOut <= 0.1f && !isAppContentLoaded)
                {
                    timeOut = GetLoadTimeOut();
                }

                timeOut -= 1.0f * Time.deltaTime;

                yield return null;
            }

            Log(LogData.LogLevel.Success, this, "Content loading has completed.");

            EventsManager.Instance.OnAppViewChangedEvent.Invoke(appView);

            yield return null;

            StopCoroutine("OnLoadResourcesContent");
        }

        private IEnumerator OnLoadStreamingAssetsContent(AppEventsData.AppViewState appView)
        {

            yield return null;

            StopCoroutine("Load");
        }

        #endregion

        #region App Events

        private void OnSelectableUIEvent(AppEventsData.SelectionType selection, AppEventsData.AppViewState appView)
        {
            if(appView == AppEventsData.AppViewState.None) return;

            EventsManager.Instance.OnAppViewChangedEvent.Invoke(AppEventsData.AppViewState.LoadingView);

            LoadAppContent(appView);
        }

        #endregion
    }
}
