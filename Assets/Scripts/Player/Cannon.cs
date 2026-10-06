using Game;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Cannon : MonoBehaviour
    {
        [Header("Projectile Parameters")]
        [SerializeField] private Bullet.Bullet bulletPrefab;

        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private float projectileVelocity = 1500f;
        [SerializeField] private float projectileCooldown = 0.1f;

        [SerializeField] private int maxBullets = 10;

        [Header("Object Pool Parameters")] [SerializeField]
        private bool collectionCheck = true;

        [SerializeField] private int defaultCapacity = 20;
        [SerializeField] private int maxSize = 100;

        public int CurrentBullets { get; private set; }

        private InputActions _actions;

        private IObjectPool<Bullet.Bullet> _objectPool;

        private float _nextTimeToShoot;
        
        private void Awake()
        {
            _actions = new InputActions();

            _objectPool = new ObjectPool<Bullet.Bullet>(
                CreateProjectile,
                OnGetFromPool,
                OnReleaseToPool,
                OnDestroyPooledObject,
                collectionCheck,
                defaultCapacity,
                maxSize
            );
            
            CurrentBullets = maxBullets;
        }

        private void OnEnable()
        {
            _actions.Spaceship.Enable();

            _actions.Spaceship.Fire.performed += OnFire;

            GameEvents.ProjectileCollision += OnProjectileCollision;
            GameEvents.ProjectileDespawn += OnProjectileDespawn;
        }

        private void OnDisable()
        {
            GameEvents.ProjectileCollision -= OnProjectileCollision;
            GameEvents.ProjectileDespawn -= OnProjectileDespawn;
            
            _actions.Spaceship.Fire.performed -= OnFire;
            
            _actions.Spaceship.Disable();
        }

        private Bullet.Bullet CreateProjectile()
        {
            var projectile = Instantiate(bulletPrefab);
            projectile.ObjectPool = _objectPool;
            return projectile;
        }

        private void OnGetFromPool(Bullet.Bullet bullet)
        {
            bullet.gameObject.SetActive(true);
        }

        private void OnReleaseToPool(Bullet.Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        private void OnDestroyPooledObject(Bullet.Bullet bullet)
        {
            Destroy(bullet.gameObject);
        }
        
        private void OnFire(InputAction.CallbackContext context)
        {
            if (_objectPool == null)
            {
                return;
            }

            if (Time.time <= _nextTimeToShoot)
            {
                return;
            }

            if (CurrentBullets < 2)
            {
                return;
            }

            foreach (var spawnPoint in spawnPoints)
            {
                Shoot(spawnPoint);

                CurrentBullets--;
                
                GameEvents.BulletRemoved.Invoke();
            }
        }

        private void Shoot(Transform spawnPoint)
        {
            var projectile = _objectPool.Get();
            
            if (projectile == null)
            {
                return;
            }

            projectile.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);

            projectile
                .GetComponent<Rigidbody>()
                .AddForce(projectile.transform.forward * projectileVelocity, ForceMode.Acceleration);

            projectile.Deactivate();

            _nextTimeToShoot = Time.time + projectileCooldown;
        }

        private void OnProjectileCollision(Collision collision)
        {
            if (CurrentBullets >= maxBullets)
            {
                return;
            }
            
            CurrentBullets++;
            
            GameEvents.BulletAdded.Invoke();
        }

        private void OnProjectileDespawn()
        {
            if (CurrentBullets >= maxBullets)
            {
                return;
            }
            
            CurrentBullets++;
            
            GameEvents.BulletAdded.Invoke();
        }
    }
}