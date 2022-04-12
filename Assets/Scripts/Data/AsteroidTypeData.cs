using Unity.Entities;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct AsteroidTypeData: IComponentData
    {
        public int type;
    }
}