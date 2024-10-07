using UnityEngine;

namespace Ecs
{
    public class Entity : MonoBehaviour
    {
        private int id;
        public int Id
        {
            get { return id; }
        }


        private EcsWorld ecsWorld;

        public void Init(EcsWorld world)
        {
            id = world.CreateEntity();
            ecsWorld = world;
            OnInit();
        }

        protected virtual void OnInit()
        {
        }

        public void Dispose()
        {
            ecsWorld.DestroyEntity(id);
            ecsWorld = null;
            id = -1;
        }

        public void SetData<T>(T component) where T : struct
        {
            ecsWorld.SetComponent(id, ref component);
        }

        public ref T GetData<T>() where T : struct
        {
            return ref ecsWorld.GetComponent<T>(id);
        } 
    }
}