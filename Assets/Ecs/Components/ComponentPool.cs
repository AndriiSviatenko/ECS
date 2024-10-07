using System;

namespace Ecs
{
    public partial class ComponentPool<T> : IComponentPool where T : struct
    {
        private Component[] components = new Component[256];
        private int size;

        void IComponentPool.AllocateComponent()
        {
            if (size + 1 >= components.Length)
            {
                Array.Resize(ref components, components.Length * 2);
            }

            components[size] = new Component
            {
                exists = false,
                value = default
            };
            
            size++;
        }

        public ref T GetComponent(int entity)
        {
            ref var component = ref components[entity];
            return ref component.value;
        }

        public void SetComponent(int entity, ref T data)
        {
            ref var component = ref components[entity];
            component.exists = true;
            component.value = data;
        }

        public bool HasComponent(int entity)
        {
            return components[entity].exists;
        }

        public void RemoveComponent(int entity)
        {
            ref var component = ref components[entity];
            component.exists = false;
        }
    }
}