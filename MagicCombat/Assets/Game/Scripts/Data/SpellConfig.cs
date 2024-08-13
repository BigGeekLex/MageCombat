using Game.Scripts.Enums;
using UnityEngine;

namespace Game.Scripts.Data
{
    [CreateAssetMenu(menuName = "Configs/Spell", fileName = "Spell", order = 0)]
    public class SpellConfig : AutoIdConfig
    {
        public ESpell Spell;
        public float Damage;
        public ECalcType CalcType;
        public float CastRange;
        public float Speed;
    }
}
