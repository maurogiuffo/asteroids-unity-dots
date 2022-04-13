using Data;
using Unity.Entities;

namespace Systems
{
    public class SpaceshipSpawnerSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            EntityQuery query = GetEntityQuery(
                ComponentType.ReadOnly<SpaceshipTag>()
            );
            var count = query.CalculateEntityCount();
            if (count > 0) return;
            
            var prefabSource = GetEntityQuery(ComponentType.ReadOnly<PrefabEntityComponent>())
                .GetSingletonEntity();
            var prefab = GetComponentDataFromEntity<PrefabEntityComponent>(true)[prefabSource].Spaceship;

            Entities
                .ForEach((ref LivesData livesData) =>
                {
                    if (livesData.remaining == 0) return;
                    livesData.remaining--;
                    EntityManager.Instantiate(prefab);
                });
        }
    }
}