using Game;
using UnityEngine;

namespace Platform
{
    [RequireComponent(typeof(MeshRenderer))]
    public class PlatformController : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float waveDistance = 20.0f;
        [SerializeField] private Material topPositionMaterial;
        [SerializeField] private Material bottomPositionMaterial;
        
        private MeshRenderer _meshRenderer;

        public Vector3 PlayerDistance => playerTransform.position - transform.position;

        private bool _playerWasInRange;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            var playerDistance = PlayerDistance.magnitude;

            if (!_playerWasInRange && playerDistance <= waveDistance)
            {
                GameEvents.WaveRangePlayerEnter.Invoke(playerTransform);
                
                _playerWasInRange = true;
            }

            if (_playerWasInRange && playerDistance > waveDistance)
            {
                GameEvents.WaveRangePlayerExit.Invoke(playerTransform);
                
                _playerWasInRange = false;
            }
        }

        public void OnReachedTop()
        {
            GameEvents.PlatformReachedTop.Invoke();
            
            _meshRenderer.material = topPositionMaterial;
        }

        public void OnReachedBottom()
        {
            GameEvents.PlatformReachedBottom.Invoke();
            
            _meshRenderer.material = bottomPositionMaterial;
        }
    }
}
