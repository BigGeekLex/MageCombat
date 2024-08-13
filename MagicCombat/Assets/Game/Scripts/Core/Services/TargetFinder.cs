using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Enums;
using Game.Scripts.Units;
using UnityEngine;

namespace Game.Scripts.Core
{
    public interface ITargetFinder
    {
        UnitFacade GetNearestTarget(Vector2 position, float range, EUnitCategory targetUnitCategory, UnitFacade except);
    }
    
    public class TargetFinder : ITargetFinder
    {
        Collider2D[] _result = new Collider2D[100];
        
        public UnitFacade GetNearestTarget(Vector2 position, float range, EUnitCategory targetUnitCategory, UnitFacade except)
        {
            var count = Physics2D.OverlapCircleNonAlloc(position, range, _result);
            var units = GetUnitsInHits(position, count, except);
            return GetAllTarget(units, position, targetUnitCategory).FirstOrDefault();
        }
        
        IEnumerable<UnitFacade> GetUnitsInHits(Vector2 pos, int count, UnitFacade except)
        => 
            _result
                .Take(count)
                .Select(h => h.GetComponentInParent<UnitFacade>())
                .Where(u => u != null && u != except)
                .OrderBy(u => Vector2.Distance(pos, u.transform.position));
        
        
        IEnumerable<UnitFacade> GetAllTarget(IEnumerable<UnitFacade> units, Vector2 pos, EUnitCategory targetUnitCategory) 
        => 
            units
                .Where(u => u.UnitCategory == targetUnitCategory)
                .OrderBy(u => Vector2.Distance(pos, u.transform.position));
    }
}
