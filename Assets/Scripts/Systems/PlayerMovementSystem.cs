using Data;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using UnityEngine;

namespace Systems
{
    public class PlayerMovementSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;
            Entities.ForEach((ref Translation position, ref PhysicsVelocity velocity, in Rotation rotation,
                in MoveData moveData) =>
            {
               // var foward = math.forward(rotation.Value);

               // todo: remove Quaternion, Mathf
                Quaternion currentRotation = rotation.Value;
                var angle = 360 - currentRotation.eulerAngles.z;
                var rad = angle * Mathf.Deg2Rad;
                var v2 = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
                var direction = new float3(v2.y, v2.x, 0f);

                velocity.Linear += direction * deltaTime * moveData.speed * moveData.direction.y;
                velocity.Angular = new float3(0, 0, -moveData.direction.x * moveData.turnSpeed);
            }).Run();
        }
    }
}