using System;
using Game;
using UnityEngine;

namespace Dancer
{
    [RequireComponent(typeof(Animator))]
    public class DancerController : MonoBehaviour
    {
        private static readonly int DanceIndex = Animator.StringToHash("DanceIndex");
        private static readonly int IsWaving = Animator.StringToHash("IsWaving");

        private enum DanceType
        {
            Flair = 0,
            House = 1,
            HipHop = 2,
            Snake = 3
        }

        [SerializeField] private DanceType type;
        [SerializeField] private float rotationSpeed = 5.0f;

        private Animator _animator;
        private Quaternion _originalRotation;
        private Transform _playerTransform;
        private bool _isWavingActive;

        private void Awake()
        {
            _animator = GetComponent<Animator>(); 
            _animator.SetInteger(DanceIndex, (int)type);
            
            _originalRotation = transform.rotation;
        }

        private void Update()
        {
            if (_isWavingActive && _playerTransform)
            {
                 var direction = _playerTransform.position - transform.position;
                 direction.y = 0.0f;

                 if (direction == Vector3.zero)
                 {
                     return;
                 }
                 
                 var targetRotation = Quaternion.LookRotation(direction);
                 transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, _originalRotation, rotationSpeed * Time.deltaTime);
            }
        }

        private void OnEnable()
        {
            GameEvents.WaveRangePlayerEnter += OnWaveRangePlayerEnter;    
            GameEvents.WaveRangePlayerExit += OnWaveRangePlayerExit;
        }

        private void OnDisable()
        {
            GameEvents.WaveRangePlayerEnter -= OnWaveRangePlayerEnter;
            GameEvents.WaveRangePlayerExit -= OnWaveRangePlayerExit;
        }

        public void OnWaveRangePlayerEnter(Transform player)
        {
            _playerTransform = player;
            _isWavingActive = true;
            _animator.SetBool(IsWaving, true); 
        }

        public void OnWaveRangePlayerExit(Transform player)
        {
            _playerTransform = null;
            _isWavingActive = false;
            _animator.SetBool(IsWaving, false); 
        }
    }
}
