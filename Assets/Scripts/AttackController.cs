using Ecs;
using SampleProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class AttackController : MonoBehaviour
    {
        [SerializeField] private Entity entity;
        private void Start()
        {
            entity = FindObjectOfType<Entity>();
        }
        private void Update()
        {
            ref AttackComponent attackComponent = 
                ref entity.GetData<AttackComponent>();

            if (Input.GetMouseButtonDown(0))
            {
                attackComponent.Attack = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                attackComponent.Attack = false;
            }
        }
    }
}
