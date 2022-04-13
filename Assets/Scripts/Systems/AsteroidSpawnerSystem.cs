using Data;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace Systems
{
    public class AsteroidSpawnerSystem : ComponentSystem
    {
        private float spawnCount = 5;
        private float spawnTimer = 0;
        private Random random;

        protected override void OnCreate()
        {
            random = new Random(1);
        }

        protected override void OnUpdate()
        {
            EntityQuery query = GetEntityQuery(
                ComponentType.ReadOnly<AsteroidTag>()
            );
            int count = query.CalculateEntityCount();
            
            if(count > 0) return;
            
            /*spawnTimer -= Time.DeltaTime;
            if (spawnTimer > 0) return;
            spawnTimer = 50;*/
            
            Entities
                .ForEach((ref PrefabEntityComponent prefabEntityComponent) =>
                {
                    for (int i = 0; i < spawnCount; i++)
                    {
                        var spawned = EntityManager.Instantiate(prefabEntityComponent.Asteroid1);
                        var position = new float3(random.NextInt(-10, 10), random.NextFloat(-10, 10), 0);
                        EntityManager.SetComponentData(spawned, new Translation {Value = position});
                        var lVelocity = new float3(random.NextFloat(-1f, 1f), random.NextFloat(-1f, 1f), 0);
                        var aVelocity = new float3(0, 0, random.NextFloat(-1f, 1f));
                        EntityManager.SetComponentData(spawned,
                            new PhysicsVelocity() {Linear = lVelocity, Angular = aVelocity});
                    }
                });
        }
    }
}