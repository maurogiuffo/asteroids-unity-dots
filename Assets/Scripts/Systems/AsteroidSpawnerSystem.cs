using Data;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Systems
{
    public class AsteroidSpawnerSystem : ComponentSystem
    {
        private float spawnCount = 5;
        private float spawnTimer = 2;
        private Random random;

        protected override void OnCreate()
        {
            random = new Random(56);
        }

        protected override void OnUpdate()
        {
            EntityQuery query = GetEntityQuery(
                ComponentType.ReadOnly<AsteroidTag>()
            );
            var count = query.CalculateEntityCount();
            if (count > 0) return;
           
            //spawnTimer -= Time.DeltaTime;
            //if (spawnTimer > 0) return;
           // spawnTimer = 2;

            var camera = Camera.main;
            var areaHeight = camera.orthographicSize * 2f;
            var areaWidth = areaHeight * camera.aspect;
            
            var angularVelocity = new float3(0, 0, 2);

            Entities
                .ForEach((ref PrefabEntityComponent prefabEntityComponent) =>
                {
                    for (int i = 0; i < spawnCount; i++)
                    {
                        var spawned = EntityManager.Instantiate(prefabEntityComponent.Asteroid1);
                        var position = new float3(areaWidth * 0.5f + random.NextFloat(-2, 2),
                            areaHeight * 0.5f + random.NextFloat(-2, 2), 0);
                        EntityManager.SetComponentData(spawned, new Translation {Value = position});
                        var linearVelocity = new float3(random.NextFloat(-1f, 1f), random.NextFloat(-1f, 1f), 0);
                        EntityManager.SetComponentData(spawned,
                            new PhysicsVelocity()
                                {Linear = linearVelocity, Angular = angularVelocity * random.NextInt(-1, 1)});
                    }
                });
        }
    }
}