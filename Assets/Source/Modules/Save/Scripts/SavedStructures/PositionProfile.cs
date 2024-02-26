using System;
using UnityEngine;

namespace Save
{
    [Serializable]
    public struct PositionProfile
    {
        public PrefabsVariants PrefabVariant;
        public Vector2 CurrentPosition;
        public Vector2 CurrentAngular;
        public Vector2 CurrentVelocity;
        public float CurrentAngularVelocity;
    }
}