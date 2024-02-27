using System;

namespace Save
{
    [Serializable]
    public struct PositionProfile
    {
        public PrefabsVariants PrefabVariant;
        public float xPos;
        public float yPos;
        public float xAng;
        public float yAng;
        public float xVel;
        public float yVel;
        public float AngVel;
    }
}