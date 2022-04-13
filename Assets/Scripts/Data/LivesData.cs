using Unity.Entities;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct LivesData: IComponentData
    {
        public int remaining;
    }
}