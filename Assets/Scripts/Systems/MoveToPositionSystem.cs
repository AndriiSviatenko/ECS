using Ecs;
using SampleProject;

namespace Systems
{
    public sealed class MoveToPositionSystem : IFixedUpdateSystem
    {
        private const float MIN_SQR_DISTANCE = 0.01f;

        private ComponentPool<MoveToPositionCommand> commandPool;
        private ComponentPool<MoveStateComponent> movePool;
        private ComponentPool<TransformComponent> transformPool;

        void IFixedUpdateSystem.OnFixedUpdate(int entity)
        {
            if (!commandPool.HasComponent(entity))
            {
                return;
            }

            ref MoveToPositionCommand command = ref commandPool.GetComponent(entity);
            ref MoveStateComponent moveComponent = ref movePool.GetComponent(entity);
            ref TransformComponent transformComponent = ref transformPool.GetComponent(entity);

            var endPosition = command.Destination;
            var myPosition = transformComponent.Value.position;

            var distanceVector = endPosition - myPosition;
            if (distanceVector.sqrMagnitude > MIN_SQR_DISTANCE)
            {
                moveComponent.MoveRequired = true;
                moveComponent.Direction = distanceVector.normalized;
            }
            else
            {
                commandPool.RemoveComponent(entity);
            }
        }
    }
}