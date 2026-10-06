using System;
using UnityEngine;

namespace Game
{
    public static class GameEvents
    {
        public static Action<Collision> ProjectileCollision;
        public static Action ProjectileDespawn;

        public static Action BulletRemoved;
        public static Action BulletAdded;

        public static Action<Transform> WaveRangePlayerEnter;
        public static Action<Transform> WaveRangePlayerExit;

        public static Action PlatformReachedTop;
        public static Action PlatformReachedBottom;
    }
}
