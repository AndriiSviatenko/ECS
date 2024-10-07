namespace Ecs
{
    public interface IFixedUpdateSystem : ISystem
    {
        void OnFixedUpdate(int entity);
    }
}