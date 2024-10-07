using System;
using Ecs;
using SampleProject;
using UnityEngine;

namespace Entities
{
    public sealed class CharacterEntity : Entity
    {
        [SerializeField] private float speed = 5.0f;
        [SerializeField] private int attackForce = 5;
        
        protected override void OnInit()
        {
            SetData(new MoveSpeedComponent
            {
                Value = speed
            });

            SetData(new TransformComponent
            {
                Value = transform
            });

            SetData(new MoveStateComponent());

            SetData(new AnimatorComponent
            {
                Value = GetComponent<Animator>()
            });

            SetData(new AttackComponent()
            {
                Value = attackForce
            });
        }
    }
}