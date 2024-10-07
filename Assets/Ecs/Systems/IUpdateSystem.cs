namespace Ecs
{
    public interface IUpdateSystem : ISystem
    {
        void OnUpdate(int entity);
    }
}