using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using Player;
using UnityEngine;

namespace UI
{
    public class UIBullets : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Cannon cannon;

        private float _spawnX = 0.0f;
        private float _spawnY = 0.0f;
        private float _iconOffsetX = 40.0f;

        private List<GameObject> _bullets = new();

        private void Start()
        {
            for (var i = 0; i < cannon.CurrentBullets; i++)
            {
                SpawnBulletIcon();
            }
        }

        private void OnEnable()
        {
            GameEvents.BulletAdded += OnBulletAdded;
            GameEvents.BulletRemoved += OnBulletRemoved;
        }

        private void OnDisable()
        {
            GameEvents.BulletAdded -= OnBulletAdded;
            GameEvents.BulletRemoved -= OnBulletRemoved;
        }

        private void SpawnBulletIcon()
        {
            var bullet = Instantiate(bulletPrefab, transform);

            if (_bullets.Count == 0)
            {
                bullet.transform.localPosition = new Vector3(_spawnX, _spawnY, 0);
            }
            else
            {
                bullet.transform.localPosition = new Vector3(_spawnX + _iconOffsetX, _spawnY, 0);

                _spawnX += _iconOffsetX;
            }

            _bullets.Add(bullet);
        }

        private void OnBulletAdded()
        {
            SpawnBulletIcon();
        }

        private void OnBulletRemoved()
        {
            if (_bullets.Count <= 0)
            {
                return;
            }

            var last = _bullets.Last();
            _bullets.Remove(last);

            Destroy(last.gameObject);

            if (_bullets.Count > 0)
            {
                _spawnX -= _iconOffsetX;
            }
        }
    }
}