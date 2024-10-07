using System;
using Ecs;
using UnityEngine;

namespace SampleProject
{
    public sealed class MoveController : MonoBehaviour
    {
        [SerializeField] private Entity entity;
        private void Start()
        {
            entity = FindObjectOfType<Entity>();
        }
        private void Update()
        {
            ref MoveStateComponent moveStateComponent =
                ref entity.GetData<MoveStateComponent>();

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveStateComponent.MoveRequired = true;
                moveStateComponent.Direction = Vector3.left;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                moveStateComponent.MoveRequired = true;
                moveStateComponent.Direction = Vector3.right;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                moveStateComponent.MoveRequired = true;
                moveStateComponent.Direction = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                moveStateComponent.MoveRequired = true;
                moveStateComponent.Direction = Vector3.back;
            }
        }
    }
}