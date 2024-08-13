using UnityEngine;

namespace Game.Scripts.Data
{
    [CreateAssetMenu(menuName = "Configs/Unit", fileName = "Unit", order = 0)]
    public class UnitConfig : AutoIdConfig
    {
        public StatsModel Stats;
    }
}
