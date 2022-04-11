using Unity.Entities;
using Unity.Mathematics;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct FireData: IComponentData
    {
        public bool fire;
    }
}