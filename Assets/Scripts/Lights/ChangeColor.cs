using Game;
using UnityEngine;

namespace Lights
{
    [RequireComponent(typeof(Light))]
    public class ChangeColor : MonoBehaviour
    {
        [SerializeField] private Color topReachedColor;
        [SerializeField] private Color bottomReachedColor;

        private Light _light;

        private void Awake()
        {
            _light = GetComponent<Light>();
        }

        private void OnEnable()
        {
            GameEvents.PlatformReachedTop += OnTopReached;
            GameEvents.PlatformReachedBottom += OnBottomReached;
        }

        private void OnDisable()
        {
            GameEvents.PlatformReachedTop -= OnTopReached;
            GameEvents.PlatformReachedBottom -= OnBottomReached;
        }

        private void OnTopReached()
        {
            _light.color = topReachedColor; 
        }

        private void OnBottomReached()
        {
            _light.color = bottomReachedColor; 
        }
    }
}
