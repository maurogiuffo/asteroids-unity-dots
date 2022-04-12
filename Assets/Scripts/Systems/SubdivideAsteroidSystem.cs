using Data;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Transforms;

namespace Systems
{
    [UpdateBefore(typeof(DestroyDeadSystem))]
    public class SubdivideAsteroidSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var prefabSource = GetEntityQuery(ComponentType.ReadOnly<PrefabEntityComponent>())
                .GetSingletonEntity();
            var prefab1 = GetComponentDataFromEntity<PrefabEntityComponent>(true)[prefabSource].Asteroid2;
            var prefab2 = GetComponentDataFromEntity<PrefabEntityComponent>(true)[prefabSource].Asteroid3;

            Entities
                .WithoutBurst()
                .WithStructuralChanges()
                .WithAll<AsteroidTag>()
                .ForEach((in HealthData healthData, in PhysicsVelocity velocity, in Translation translation, in AsteroidTypeData typeData) =>
                {
                    if (!healthData.isDead) return;
                    switch (typeData.type)
                    {
                        case 2:
                            return;
                        case 1:
                            Spawn(prefab2, translation, velocity, 1.5f);
                            Spawn(prefab2, translation, velocity, -1.5f);
                            break;
                        case 0:
                            Spawn(prefab1, translation, velocity, 1.5f);
                            Spawn(prefab1, translation, velocity, -1.5f);
                            break;                            
                    }
                }).Run();

            return default;
        }

        private void Spawn(Entity prefab, Translation translation, PhysicsVelocity velocity, float velocityModifier)
        {
            var spawned = EntityManager.Instantiate(prefab);
            EntityManager.SetComponentData(spawned, new Translation() {Value = translation.Value});
            EntityManager.SetComponentData(spawned,
                new PhysicsVelocity() {Linear = velocity.Linear * velocityModifier});
        }
    }
}