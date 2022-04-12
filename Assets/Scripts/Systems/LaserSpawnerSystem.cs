using Data;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace Systems
{
    public class LaserSpawnerSystem : ComponentSystem
    {
        private float timer;

        protected override void OnUpdate()
        {
            var prefabSource = GetEntityQuery(ComponentType.ReadOnly<PrefabEntityComponent>())
                .GetSingletonEntity();
            var prefab = GetComponentDataFromEntity<PrefabEntityComponent>(true)[prefabSource].Laser;

            timer -= Time.DeltaTime;
            if (timer < 0) timer = 0;
            
            Entities.ForEach((ref FireData fireData, ref Translation translation, ref Rotation rotation) =>
            {
                if (!fireData.fire || !timer.Equals(0)) return;
                timer = fireData.time;
                var spawned = EntityManager.Instantiate(prefab);
                EntityManager.SetComponentData(spawned, new Translation {Value = translation.Value});
                EntityManager.SetComponentData(spawned, new Rotation() {Value = rotation.Value});
                var velocity = math.mul(rotation.Value, new float3(0, 1, 0)) * 5;
                EntityManager.SetComponentData(spawned, new PhysicsVelocity() {Linear = velocity});
                EntityManager.SetComponentData(spawned, new LaserLifeTime() {value = 2});
            });
        }
    }
}