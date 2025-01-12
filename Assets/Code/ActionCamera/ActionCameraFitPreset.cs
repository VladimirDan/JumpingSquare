using UnityEngine;

namespace Game.Scripts
{
    [RequireComponent(typeof(ActionCamera))]
    public class ActionCameraFitPreset : MonoBehaviour
    {
        [SerializeField] private ActionCameraSimplePreset minPreset;

        private void Awake() => MoveByPreset();
        
        [ContextMenu(nameof(MoveByPreset))]
        private void MoveByPreset() => GetComponent<ActionCamera>().MoveByPreset(minPreset);
    }
}