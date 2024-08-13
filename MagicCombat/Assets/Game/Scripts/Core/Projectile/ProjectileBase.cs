using Game.Scripts.Data;
using Game.Scripts.Enums;
using Game.Scripts.Units;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Core
{
    public class ProjectileBase : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        [Inject] private readonly IDamageProcessor _damageProcessor;
        
        private Pool _pool;
        private Vector2 _startPos;
        private Pool.Args _args;
        private float _maxDistance;
        
        private void OnCreated(Pool pool)
        {
            _pool = pool;
        }
        
        protected virtual void Reinitialize(Pool.Args args)
        {
            _args = args;
            
            _maxDistance = _args.TargetSpell.CastRange;
            transform.position = args.Position;
            _startPos = transform.position;
            
            gameObject.SetActive(true);

            Vector2 velocity = args.Direction * args.TargetSpell.Speed;

            _rigidbody2D.velocity = velocity;
        }

        private void FixedUpdate()
        {
            HandleMaxRangeReached();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            other.TryGetComponent(out UnitFacade targetUnit);
            
            if (targetUnit == null || targetUnit == _args.SourceCaster)
                return;
            
            DamageInput di = new DamageInput
            {
                Source = _args.SourceCaster,
                Target = targetUnit,
                IsDamageOverride = true,
                CalcSourceDamage = _args.TargetSpell.CalcType,
                SourceOverrideDamage = _args.TargetSpell.Damage
            };
            
            _damageProcessor.ProcessDamage(di);
            
            Finish();
        }

        private void HandleMaxRangeReached()
        {
            Vector2 currPos	= transform.position;
            float distance = (currPos - _startPos).magnitude;

            if (distance >= _maxDistance)
                Finish();
        }

        private void Finish()
        {
            _rigidbody2D.velocity = Vector2.zero;
            gameObject.SetActive(false);
            _pool.Despawn(this);
        }
        
        public class Pool : MonoMemoryPool<Pool.Args, ProjectileBase>
        {
            public struct Args
            {
                public Vector2 Position;
                public UnitFacade SourceCaster;
                public SpellConfig TargetSpell;
                public Vector2 Direction;
            }

            [Inject] public readonly ESpell ProjectileId;
            protected override void OnCreated(ProjectileBase projectile)
            {
                base.OnCreated(projectile);

                projectile.OnCreated(this);
            }


            protected override void Reinitialize(Args args, ProjectileBase projectile)
            {
                projectile.Reinitialize(args);
            }
        }
    }
}
