using Data;
using Unity.Entities;
using Unity.Jobs;

namespace Systems
{
    public class LaserLifetimeSystem : JobComponentSystem
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
            var deltatime = Time.DeltaTime;

            Entities.WithBurst().ForEach((Entity entity, ref LaserLifeTime laserLife) =>
            {
                laserLife.value -= deltatime;
                if (laserLife.value < 0) ecb.DestroyEntity(entity);
            }).Run();

            return default;
        }
    }
}