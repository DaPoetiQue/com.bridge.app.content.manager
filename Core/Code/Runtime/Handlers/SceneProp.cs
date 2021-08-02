using Bridge.Core.Debug;
using System.Collections.Generic;
using UnityEngine;

namespace Bridge.Core.App.Content.Manager
{
    public class SceneProp : ObjectData, ISceneObject
    {
        #region Components

        [Space(5)]
        public List<Sprite> imageTag;

        public Pose propPose = new Pose();

        [Space(5)]
        public int groupIndex;

        #endregion

        #region Main

        public string GetName()
        {
            if(string.IsNullOrEmpty(nameTag))
            {
                Log(LogLevel.Warning, this, $"Scene Prop name missing/not assigned. Returning default name : {name}");
                return name;
            }

            return nameTag;
        }

        public List<Sprite> GetImageTag()
        {
            if(imageTag.Count <= 0)
            {
                Log(LogLevel.Warning, this, $"There are no Scene Prop image tag(s) assigned for : {nameTag}. Returning null");
                return null;
            }

            return imageTag;
        }

        public GameObject GetObject()
        {
            return this.gameObject;
        }

        public Pose GetPropPose()
        {
            return propPose;
        }

        public int GetSceneGroupIndex()
        {
            return groupIndex;
        }

        public new int GetInstanceID()
        {
            return this.gameObject.GetInstanceID();
        }

        public void ShowAsset(bool show)
        {
            asset.SetActive(show);
        }

        public void SetPose(Pose pose, SceneObjectSpace space)
        {
            propPose = pose;

            switch(space)
            {
                case SceneObjectSpace.Local:

                    this.transform.localPosition = pose.position;
                    this.transform.localRotation = pose.rotation;
                    this.transform.localScale = pose.scale;

                    break;

                case SceneObjectSpace.World:

                    this.transform.position = pose.position;
                    this.transform.rotation = pose.rotation;
                    this.transform.localScale = pose.scale;

                    break;
            }
        }

        #endregion
    }
}
