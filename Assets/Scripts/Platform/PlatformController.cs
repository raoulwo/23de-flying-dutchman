using System;
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

        private float _playerDistance;
        private bool _playerWasInRange;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            _playerDistance = Vector3.Magnitude(playerTransform.position - transform.position);

            if (!_playerWasInRange && _playerDistance <= waveDistance)
            {
                GameEvents.WaveRangePlayerEnter.Invoke(playerTransform);
                
                _playerWasInRange = true;
            }

            if (_playerWasInRange && _playerDistance > waveDistance)
            {
                GameEvents.WaveRangePlayerExit.Invoke(playerTransform);
                
                _playerWasInRange = false;
            }
        }

        public void OnReachedTop()
        {
            _meshRenderer.material = topPositionMaterial;
        }

        public void OnReachedBottom()
        {
            _meshRenderer.material = bottomPositionMaterial;
        }
    }
}
