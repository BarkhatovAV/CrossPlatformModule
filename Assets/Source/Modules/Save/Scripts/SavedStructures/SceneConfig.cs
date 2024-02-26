using System;
using System.Collections.Generic;
using UnityEngine;

namespace Save
{
    [Serializable]
    public class SceneConfig
    {
        //public List<PrefabsVariants> prefabVariants;
        public List<int> prefabVariants;
        public List<Vector2> positions;
        public List<Vector2> angulars;
        public List<Vector2> velocities;
        public List<float> angularVelocities;

        public void Add(PositionProfile positionProfile)
        {
            if(positions == null)
            {
                //prefabVariants = new List<PrefabsVariants>();
                prefabVariants = new List<int>();
                positions = new List<Vector2>();
                angulars = new List<Vector2>();
                velocities = new List<Vector2>();
                angularVelocities = new List<float>();

                Debug.Log("Создаю по-новой");
            }

            prefabVariants.Add(positionProfile.PrefabVariant);
            positions.Add(positionProfile.CurrentPosition);
            angulars.Add(positionProfile.CurrentAngular);
            velocities.Add(positionProfile.CurrentVelocity);
            angularVelocities.Add(positionProfile.CurrentAngularVelocity);
        }
    }
}