using Data;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace Systems
{
    public class LaserSpawnerSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities
                .ForEach((ref PrefabEntityComponent prefabEntityComponent, ref FireData fireData,
                    ref Translation translation, ref Rotation rotation) =>
                {
                    if (fireData.fire)
                    {
                        var spawned = EntityManager.Instantiate(prefabEntityComponent.Prefab);
                        EntityManager.SetComponentData(spawned, new Translation {Value = translation.Value});
                        EntityManager.SetComponentData(spawned, new Rotation() {Value = rotation.Value});
                        var velocity = math.mul(rotation.Value, new float3(0, 1, 0)) * 5;
                        EntityManager.SetComponentData(spawned, new PhysicsVelocity() {Linear = velocity});
                        EntityManager.SetComponentData(spawned, new LaserLifeTime() {value = 2});
                    }
                });
        }
    }
}