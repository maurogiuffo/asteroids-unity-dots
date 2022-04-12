using Unity.Entities;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct FireData: IComponentData
    {
        public bool fire;
        public float time;
    }
}