using Data;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;

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
                var direction = math.mul(rotation.Value, new float3(0, 1, 0));
                velocity.Linear += direction * deltaTime * moveData.speed * moveData.direction.y;
                velocity.Angular = new float3(0, 0, -moveData.direction.x * moveData.turnSpeed);
            }).Run();
        }
    }
}