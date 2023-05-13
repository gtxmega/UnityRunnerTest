using UnityEngine;

namespace Utility
{
    public class WorldCanvasSetCamera : MonoBehaviour
    {
        [SerializeField] private Canvas _targetCanvas;

        private void Start()
        {
            _targetCanvas.worldCamera = Camera.main;
        }
    }
}