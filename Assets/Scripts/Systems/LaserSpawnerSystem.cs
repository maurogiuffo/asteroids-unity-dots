using Data;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

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

                        // todo: remove Quaternion, Mathf
                        Quaternion currentRotation = rotation.Value;
                        var angle = 360 - currentRotation.eulerAngles.z;
                        var rad = angle * Mathf.Deg2Rad;
                        var v2 = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
                        var direction = new float3(v2.y, v2.x, 0f);
                        var velocity = direction * 5;
                        EntityManager.SetComponentData(spawned, new PhysicsVelocity() {Linear = velocity});
                    }
                });
                
        }
    }
}