using System.Collections.Generic;
using Bridge.Core.Debug;
using UnityEngine;

namespace Bridge.Core.App.Content.Manager
{
    public class SceneContentGroup : MonoDebug, ISceneContentGroup
    {
        #region Components

        [Space(5)]
        public string groupName;

        [Space(5)]
        public List<Sprite> imageTag;

        [Space(5)]
        public List<ObjectData> loadedContent;

        [Space(5)]
        public int loadedContentCount;

        [HideInInspector]
        public int loadedGroupIndex;

        #endregion

        #region Main

        #region Getting Group Data

        public string GetGroupName()
        {
            if(string.IsNullOrEmpty(groupName))
            {
                Log(LogData.LogLevel.Debug, this, $"Group Name Missing/Not Assigned. Returning Default Name : {name}");
                return name;
            }

            return groupName;
        }

        public List<Sprite> GetImageTags()
        {
            return imageTag;
        }

        public ObjectData GetContentAtIndex(int objectIndex)
        {
            if(loadedContent.Count <= 0)
            {
                Log(LogData.LogLevel.Error, this, $"There is no Group Content found.");
                return null;
            }

            if(objectIndex > loadedContent.Count)
            {
                Log(LogData.LogLevel.Error, this, $"Content index : {objectIndex} for getting Group Content is out of range.");
                return null;
            }

            return loadedContent[objectIndex];
        }

        public List<ObjectData> GetAllContent()
        {
            if (loadedContent.Count <= 0)
            {
                Log(LogData.LogLevel.Error, this, $"There is no Group Content found. Returning null.");
                return null;
            }

            return loadedContent;
        }

        public int GetLoadedContentCount()
        {
            if (loadedContent.Count <= 0)
            {
                Log(LogData.LogLevel.Warning, this, $"There is no Group Content found. Returning 0.");
                return 0;
            }

            return loadedContentCount;
        }

        public int GetIndex()
        {
            return loadedGroupIndex;
        }

        #endregion

        #endregion
    }
}
