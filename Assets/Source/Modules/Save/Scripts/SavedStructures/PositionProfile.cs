using System;

namespace Save
{
    [Serializable]
    public struct PositionProfile
    {
        public PrefabsVariants PrefabVariant;
        public float CurrentXPosition;
        public float CurrentYPosition;
        public float CurrentXAngular;
        public float CurrentYAngular;
        public float CurrentXVelocity;
        public float CurrentYVelocity;
        public float CurrentAngularVelocity;
    }
}