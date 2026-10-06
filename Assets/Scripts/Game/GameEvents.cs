using System;
using UnityEngine;

namespace Game
{
    public static class GameEvents
    {
        public static Action<Collision> ProjectileCollision;

        public static Action<Transform> WaveRangePlayerEnter;
        public static Action<Transform> WaveRangePlayerExit;
    }
}
