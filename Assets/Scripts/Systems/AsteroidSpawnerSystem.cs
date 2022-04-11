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
        private Random random = new Random(1);
        
        protected override void OnUpdate()
        {
            spawnTimer -= Time.DeltaTime;
            if (spawnTimer > 0) return;
            spawnTimer = 10;
            
            Entities.ForEach((ref PrefabEntityComponent prefabEntityComponent) =>
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    var spawned = EntityManager.Instantiate(prefabEntityComponent.AsteroidPrefab);
                    var position = new float3(random.NextFloat(-10, 10), random.NextFloat(-10, 10), 0);
                    EntityManager.SetComponentData(spawned, new Translation {Value = position});
                    var velocity = new float3(random.NextFloat(-1f,1f), random.NextFloat(-1f,1f), 0);
                    EntityManager.SetComponentData(spawned, new PhysicsVelocity() {Linear = velocity});
                }
            });
        }
    }
}