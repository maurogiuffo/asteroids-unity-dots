using Unity.Entities;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct LaserLifeTime: IComponentData
    {
        public float value;
    }
}