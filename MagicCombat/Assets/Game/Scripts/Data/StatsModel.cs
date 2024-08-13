using System;
using Game.Scripts.Enums;

namespace Game.Scripts.Data
{
    [Serializable]
    public class StatsModel
    {
        public StatData[] Stats;
    }
    
    
    [Serializable]
    public class StatData
    {
        public EStat Stat;
        public float Value;
    }
}
