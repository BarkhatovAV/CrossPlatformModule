using System;
using System.Collections.Generic;

namespace Save
{
    [Serializable]
    public class SceneConfig
    {
        public List<PositionProfile> profiles = new List<PositionProfile>();
    }
}