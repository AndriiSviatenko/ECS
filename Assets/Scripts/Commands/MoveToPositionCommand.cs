using System;
using UnityEngine;

namespace SampleProject
{
    [Serializable]
    public struct MoveToPositionCommand
    {
        public Vector3 Destination;
    }
}