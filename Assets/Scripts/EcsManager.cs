using Assets.Scripts.Systems;
using Ecs;
using Systems;
using UnityEngine;

namespace SampleProject
{
    public sealed class EcsManager : MonoBehaviour
    {
        private readonly EcsWorld ecsWorld = new();


        private void Awake()
        {
            ecsWorld.BindComponent<MoveStateComponent>();
            ecsWorld.BindComponent<MoveSpeedComponent>();
            ecsWorld.BindComponent<TransformComponent>();
            ecsWorld.BindComponent<AnimatorComponent>();
            ecsWorld.BindComponent<MoveToPositionCommand>();
            ecsWorld.BindComponent<AttackComponent>();

            ecsWorld.BindSystem<MovementSystem>();
            ecsWorld.BindSystem<MoveAnimationSystem>();
            ecsWorld.BindSystem<MoveToPositionSystem>();
            ecsWorld.BindSystem<AttackSystem>();

            ecsWorld.Install();
            FindObjectOfType<Entity>().Init(ecsWorld);
        }

        private void Update()
        {
            ecsWorld.Update();
        }

        private void FixedUpdate()
        {
            ecsWorld.FixedUpdate();
        }

        private void LateUpdate()
        {
            ecsWorld.LateUpdate();
        }
    }
}