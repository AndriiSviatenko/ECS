using System;
using UnityEngine;

namespace SampleProject
{
    [Serializable]
    public struct MoveStateComponent
    {
        public bool MoveRequired;
        public Vector3 Direction;
    }
}