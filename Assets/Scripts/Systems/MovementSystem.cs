using Ecs;
using SampleProject;
using UnityEngine;

namespace Systems
{
    public sealed class MovementSystem : IFixedUpdateSystem
    {
        private ComponentPool<MoveStateComponent> statePool;
        private ComponentPool<MoveSpeedComponent> speedPool;
        private ComponentPool<TransformComponent> transformPool;

        void IFixedUpdateSystem.OnFixedUpdate(int entity) //Index на мою сущность!
        {
            if (!statePool.HasComponent(entity))
            {
                return;
            }
            
            ref MoveStateComponent stateComponent = ref statePool.GetComponent(entity);
            if (!stateComponent.MoveRequired)
            {
                return;
            }
            
            //Логика перемещения:
            ref TransformComponent transformComponent = 
                ref transformPool.GetComponent(entity);
            ref MoveSpeedComponent moveSpeedComponent = 
                ref speedPool.GetComponent(entity);

            var direction = stateComponent.Direction;
            var offset = direction * moveSpeedComponent.Value * Time.fixedDeltaTime;
            transformComponent.Value.position += offset;
            transformComponent.Value.rotation = Quaternion.LookRotation(direction, Vector3.up);
            stateComponent.MoveRequired = false;
        }
    }
}