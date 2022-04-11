using Unity.Entities;
using Unity.Mathematics;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct MoveData: IComponentData
    {
        public float3 direction;
        public float speed;
        public float turnSpeed;
    }
}