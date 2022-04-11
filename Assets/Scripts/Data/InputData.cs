using Unity.Entities;
using UnityEngine;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct InputData : IComponentData
    {
        public KeyCode upKey;
        public KeyCode downKey;
        public KeyCode rightKey;
        public KeyCode leftKey;
        public KeyCode fireKey;
    }
}
