using Unity.Entities;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct HealthData : IComponentData
    {
        public bool isDead;
    }
}