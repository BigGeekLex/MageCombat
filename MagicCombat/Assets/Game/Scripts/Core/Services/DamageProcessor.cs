using Game.Scripts.Enums;
using UnityEngine;

namespace Game.Scripts.Core
{

    public interface IDamageProcessor
    {
        void ProcessDamage(DamageInput di);
    }
    
    public class DamageProcessor : IDamageProcessor
    {
        public void ProcessDamage(DamageInput di)
        {
            var currentTargetStats = di.Target.UnitModel.CurrentStats;
            var maxTargetStats = di.Target.UnitModel.MaxStats;
            
            float currentHealth = currentTargetStats[EStat.Health];
            float maxHealth = maxTargetStats[EStat.Health];
            float damage = -CalculateDamage(di);
            
            currentTargetStats[EStat.Health] = Mathf.Clamp(currentHealth + damage, 0, maxHealth);
        }
        
        //Main damage formula
        private float CalculateDamage(DamageInput di)
        {
            float damageSource = di.IsDamageOverride ? GetOverrideDamage(di) : di.Source.UnitModel.MaxStats[EStat.Damage];
            float targetArmorRatio = di.Target.UnitModel.MaxStats[EStat.ArmorRatio];
            
            return damageSource * (1 - targetArmorRatio); //1 - full block damage
        }

        private float GetOverrideDamage(DamageInput di)
        {
            ECalcType calc = di.CalcSourceDamage;

            switch (calc)
            {
                case ECalcType.Override:
                {
                    return di.SourceOverrideDamage;
                }
                case ECalcType.PercentFromMax:
                {
                    var sourceStat = di.Source.UnitModel.MaxStats[EStat.Damage];
                    return di.SourceOverrideDamage * sourceStat + sourceStat;
                }
            }
            return 0;
        }
    }
}
