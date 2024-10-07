namespace Ecs
{
    public partial class ComponentPool<T> where T : struct
    {
        private struct Component
        {
            public bool exists;
            public T value;
        }
    }
}