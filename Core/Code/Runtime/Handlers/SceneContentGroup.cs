using System.Collections.Generic;
using Bridge.Core.Debug;

namespace Bridge.Core.App.Content.Manager
{
    public class SceneContentGroup : MonoDebug, ISceneContent
    {
        public List<ObjectData> loadedContent;

        public int contentCount;
    }
}
