using Ecs;
using SampleProject;
using UnityEngine;

namespace Systems
{
    public sealed class MoveAnimationSystem : ILateUpdateSystem
    {
        private static readonly int State = Animator.StringToHash("State");

        private ComponentPool<AnimatorComponent> animatorPool;
        private ComponentPool<MoveStateComponent> movePool;
        private ComponentPool<AttackComponent> _attackPool;

        void ILateUpdateSystem.OnLateUpdate(int entity)
        {
            if (!animatorPool.HasComponent(entity) ||
                !movePool.HasComponent(entity) ||
                !_attackPool.HasComponent(entity))
            {
                return;
            }

            ref AnimatorComponent animatorComponent =
                ref animatorPool.GetComponent(entity);
            ref MoveStateComponent moveStateComponent =
                ref movePool.GetComponent(entity);
            ref AttackComponent attackComponent =
                ref _attackPool.GetComponent(entity);

            Debug.Log(moveStateComponent.MoveRequired);

            if (moveStateComponent.MoveRequired)
            {
                animatorComponent.Value.SetInteger(State, 1);
            }
            else if (attackComponent.Attack)
            {
                animatorComponent.Value.SetInteger(State, 2);
            }
            else
            {
                animatorComponent.Value.SetInteger(State, 0);
            }
        }
    }
}