using Unity.Entities;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct PrefabEntityComponent: IComponentData
    {
        public Entity AsteroidPrefab;
    }
}