using System.Collections.Generic;
using UnityEngine;

namespace Bridge.Core.App.Content.Manager
{
    public interface ISceneObject
    {
        string GetName();
        List<Sprite> GetImageTag();
        GameObject GetObject();
        Pose GetPropPose();
        int GetSceneGroupIndex();
        int GetInstanceID();

        void ShowAsset(bool show);
        void SetPose(Pose pose, SceneObjectSpace space);
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
