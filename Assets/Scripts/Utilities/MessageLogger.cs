using Game;
using UnityEngine;

namespace Utilities
{
    public class MessageLogger : MonoBehaviour
    {
        private void OnEnable()
        {
            GameEvents.ProjectileCollision += LogProjectileCollision;
        }

        private void OnDisable()
        {
            GameEvents.ProjectileCollision -= LogProjectileCollision;
        }

        private void LogProjectileCollision(Collision collision)
        {
            Debug.Log($"Projectile {collision.thisGameObject.name} hit {collision.gameObject.name}");
        }
    }
}