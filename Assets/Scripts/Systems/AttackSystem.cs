using Ecs;
using SampleProject;
using System.Diagnostics;
using UnityEditor.Tilemaps;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.Systems
{
    public sealed class AttackSystem : IUpdateSystem
    {
        private ComponentPool<AttackComponent> _attackPool;
        public void OnUpdate(int entity)
        {
            if (_attackPool == null)
            {
                Debug.LogError("Attack pool is not initialized.");
                return;
            }

            if (!_attackPool.HasComponent(entity))
            {
                return;
            }

            ref AttackComponent attackComponent =
                ref _attackPool.GetComponent(entity);

            if (!attackComponent.Attack)
            {
                return;
            }

            Debug.Log("Attack");
        }
    }
}
