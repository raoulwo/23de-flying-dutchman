using System;
using System.Collections;
using Game;
using UnityEngine;
using UnityEngine.Pool;

namespace Bullet
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float timeoutDelay = 5f;

        [Header("Debug")] 
        [SerializeField] private float timeUntilTimeout;

        public IObjectPool<Bullet> ObjectPool
        {
            set => _objectPool = value;
        }

        private IObjectPool<Bullet> _objectPool;

        private Rigidbody _rb;

        private Coroutine _coroutine;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            GameEvents.ProjectileCollision.Invoke(collision);
            
            ReturnToPool();
        }

        public void Deactivate()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(DeactivateRoutine(timeoutDelay));
        }

        private IEnumerator DeactivateRoutine(float delay)
        {
            timeUntilTimeout = delay;

            while (timeUntilTimeout > 0.0f)
            {
                timeUntilTimeout -= Time.deltaTime;

                yield return null;
            }

            ReturnToPool();
        }

        private void ReturnToPool()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;

            if (_objectPool != null)
            {
                _objectPool.Release(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}