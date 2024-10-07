namespace Ecs
{
    public interface ILateUpdateSystem : ISystem
    {
        void OnLateUpdate(int entity);
    }
}