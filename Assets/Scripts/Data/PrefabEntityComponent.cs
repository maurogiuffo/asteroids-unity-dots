using Unity.Entities;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct PrefabEntityComponent: IComponentData
    {
        public Entity Asteroid1;
        public Entity Asteroid2;
        public Entity Asteroid3;
        public Entity Laser;
        public Entity Spaceship;
    }
}