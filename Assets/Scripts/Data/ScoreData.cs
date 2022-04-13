using Unity.Entities;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct ScoreData: IComponentData
    {
        public int value;
    }
}