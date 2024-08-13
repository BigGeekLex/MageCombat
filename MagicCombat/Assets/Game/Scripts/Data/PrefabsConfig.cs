using System;
using Game.Scripts.Core;
using Game.Scripts.Enums;
using Game.Scripts.Units;
using UnityEngine;

namespace Game.Scripts.Data
{
    [CreateAssetMenu(menuName = "Configs/Prefabs", fileName = "Prefabs", order = 0)]
    public class PrefabsConfig : ScriptableObject
    {
        public UnitData[] Enemies;
        public UnitData[] Heroes;
        public ProjectileData[] Projectiles;
    }

    [Serializable]
    public class ProjectileData
    {
        public ESpell Spell;
        public ProjectileBase Projectile;
    }
    
    [Serializable]
    public class UnitData
    {
        public UnitConfig UnitConfig;
        public UnitFacade UnitPrefab;
    }
}
