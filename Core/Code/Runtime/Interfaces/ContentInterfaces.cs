using System.Collections.Generic;
using UnityEngine;

namespace Bridge.Core.App.Content.Manager
{
    public interface ISceneObject
    {

    }

    public interface ISceneUI
    {

    }

    public interface ISceneContentGroup
    {
        string GetGroupName();
        List<Sprite> GetImageTags();
        ObjectData GetContentAtIndex(int contentIndex);
        List<ObjectData> GetAllContent();
        int GetLoadedContentCount();
        int GetIndex();
    }
}
