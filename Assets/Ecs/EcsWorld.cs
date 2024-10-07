using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Ecs
{
    public sealed class EcsWorld
    {
        private readonly List<ISystem> systems = new();
        private readonly List<IUpdateSystem> updateSystems = new();
        private readonly List<IFixedUpdateSystem> fixedUpdateSystems = new();
        private readonly List<ILateUpdateSystem> lateUpdateSystems = new();

        private readonly Dictionary<Type, IComponentPool> componentPools = new();
        private readonly List<bool> entities = new();

        public int CreateEntity()
        {
            var id = 0;
            var count = entities.Count;

            for (; id < count; id++)
            {
                if (!entities[id])
                {
                    entities[id] = true;
                    return id;
                }
            }

            id = count;
            entities.Add(true);

            foreach (var pool in componentPools.Values)
            {
                pool.AllocateComponent();
            }

            return id;
        }

        public void DestroyEntity(int entity)
        {
            entities[entity] = false;
            foreach (var pool in componentPools.Values)
            {
                pool.RemoveComponent(entity);
            }
        }

        public ref T GetComponent<T>(int entity) where T : struct
        {
            var pool = (ComponentPool<T>)componentPools[typeof(T)];
            return ref pool.GetComponent(entity);
        }

        public void SetComponent<T>(int entity, ref T component) where T : struct
        {
            var pool = (ComponentPool<T>) componentPools[typeof(T)];
            pool.SetComponent(entity, ref component);
        }

        public void Update()
        {
            for (int i = 0, count = updateSystems.Count; i < count; i++)
            {
                var system = updateSystems[i];
                for (var entity = 0; entity < entities.Count; entity++)
                {
                    if (entities[entity])
                    {
                        system.OnUpdate(entity);
                    }
                }
            }
        }

        public void FixedUpdate()
        {
            for (int i = 0, count = fixedUpdateSystems.Count; i < count; i++)
            {   
                var system = fixedUpdateSystems[i];
                for (var entity = 0; entity < entities.Count; entity++)
                {
                    if (entities[entity])
                    {
                        system.OnFixedUpdate(entity);
                    }
                }
            }
        }

        public void LateUpdate()
        {
            for (int i = 0, count = lateUpdateSystems.Count; i < count; i++)
            {
                var system = lateUpdateSystems[i];
                for (var entity = 0; entity < entities.Count; entity++)
                {
                    if (entities[entity])
                    {
                        system.OnLateUpdate(entity);
                    }
                }
            }
        }

        public void BindComponent<T>() where T : struct
        {
            componentPools[typeof(T)] = new ComponentPool<T>();
        }

        public void BindSystem<T>() where T : ISystem, new()
        {
            var system = new T();
            systems.Add(system);

            if (system is IUpdateSystem updateSystem)
            {
                updateSystems.Add(updateSystem);
            }

            if (system is IFixedUpdateSystem fixedUpdateSystem)
            {
                fixedUpdateSystems.Add(fixedUpdateSystem);
            }

            if (system is ILateUpdateSystem lateUpdateSystem)
            {
                lateUpdateSystems.Add(lateUpdateSystem);
            }
        }

        public void Install()
        {
            foreach (var system in systems)
            {
                Type systemType = system.GetType();
                var fields = systemType.GetFields(
                    BindingFlags.Instance |
                    BindingFlags.DeclaredOnly |
                    BindingFlags.NonPublic
                );
                foreach (var field in fields)
                {
                    var componentType = field.FieldType.GenericTypeArguments[0];
                    if (componentPools.TryGetValue(componentType, out var componentPool))
                    {
                        field.SetValue(system, componentPool);
                    }
                    else
                    {
                        Debug.LogError($"Component pool for type {componentType} not found.");
                    }
                }
            }
        }
    }
}