using Game.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Units
{
    public class BotDamageLogic : MonoBehaviour
    {
        [Inject] private readonly UnitFacade _unitFacade;
        [Inject] private readonly IDamageProcessor _damageProcessor;
        
        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            other.gameObject.TryGetComponent(out UnitFacade unit);
            
            if (unit == null)
                return;
            
            ProcessDamage(unit);
        }

        protected virtual void ProcessDamage(UnitFacade target)
        {
            DamageInput di = new DamageInput
            {
                Source = _unitFacade,
                Target = target
            };
            
            _damageProcessor.ProcessDamage(di);
        }
    }
}
