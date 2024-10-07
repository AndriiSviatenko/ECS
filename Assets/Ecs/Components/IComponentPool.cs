namespace Ecs
{
    public interface IComponentPool
    {
        void AllocateComponent();
        void RemoveComponent(int entity);
    }
}