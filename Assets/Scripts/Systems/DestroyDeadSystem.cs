using Data;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace Systems
{
    public class DestroyDeadSystem : JobComponentSystem
    {
        private EndSimulationEntityCommandBufferSystem endSimulationEntityCommandBufferSystem;

        protected override void OnCreate()
        {
            endSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            base.OnCreate();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var ecb = endSimulationEntityCommandBufferSystem.CreateCommandBuffer();

            Entities.WithBurst().ForEach((Entity entity, in HealthData healthData) =>
            {
                if (healthData.isDead)
                {
                    ecb.DestroyEntity(entity);
                }
            }).Run();

            return default;
        }
    }
}