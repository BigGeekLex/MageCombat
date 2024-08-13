using Game.Scripts.Data;
using Game.Scripts.Enums;
using UniRx;
using Zenject;

namespace Game.Scripts.Units
{
    public class UnitModel : IInitializable
    {
        [Inject] private readonly UnitConfig _unitConfig;
        
        private ReactiveDictionary<EStat, float> _currentStats = new();
        private ReactiveDictionary<EStat, float> _maxStats = new();

        public ReadOnlyReactiveProperty<bool> IsDead;
        public ReactiveDictionary<EStat, float> CurrentStats => _currentStats;
        public IReadOnlyReactiveDictionary<EStat, float> MaxStats => _maxStats;
        
        public void Initialize()
        {
            IsDead = Observable.Merge(
                    CurrentStats.ObserveAdd().Where(x => x.Key == EStat.Health).Select(x => x.Value),
                    CurrentStats.ObserveReplace().Where(x => x.Key == EStat.Health).Select(x => x.NewValue)
                )
                .Select(h => h <= 0)
                .ToReadOnlyReactiveProperty(false);
            
            var stats = _unitConfig.Stats.Stats;

            foreach (var statData in stats)
            {
                EStat stat = statData.Stat;
                float value = statData.Value;
                
                _maxStats.Add(stat, value);
                
                if (stat.IsHasCurrent())
                    _currentStats.Add(stat, value);
            }
        }
    }

    public static class StatExtension
    {
        public static bool IsHasCurrent(this EStat stat)
        {
            switch (stat)
            {
                case EStat.Health:
                    return true;
            }
            return false;
        }
    }
}
