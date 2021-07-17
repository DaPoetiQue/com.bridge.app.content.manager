using UnityEngine;

namespace Bridge.Core.App.Content.Manager
{
    [CreateAssetMenu(fileName = "Interaction Data", menuName = "3ridge/Interaction Data")]
    public class InteractionData : ScriptableObject
    {
        public Interactions interactions;
    }
}
