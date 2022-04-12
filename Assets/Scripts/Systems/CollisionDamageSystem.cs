using Data;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;

namespace Systems
{
    [UpdateBefore(typeof(DestroyDeadSystem))]
    public class CollisionDamageSystem : JobComponentSystem
    {
        private BuildPhysicsWorld buildPhysicsWorld;
        private StepPhysicsWorld stepPhysicsWorld;

        protected override void OnCreate()
        {
            buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
            stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
            base.OnCreate();
        }

        private struct ApplierJob : ITriggerEventsJob
        {
            public ComponentDataFromEntity<DamageData> damageGruop;
            public ComponentDataFromEntity<HealthData> healthGruop;

            public void Execute(TriggerEvent triggerEvent)
            {
                Exec( triggerEvent.EntityA, triggerEvent.EntityB);
                Exec(triggerEvent.EntityB, triggerEvent.EntityA);
            }

            private void Exec(Entity entityA, Entity entityB)
            {
                if (!damageGruop.HasComponent(entityA)) return;
                if (!healthGruop.HasComponent(entityB)) return;
                var healthData = healthGruop[entityB];
                healthData.isDead = true;
                healthGruop[entityB] = healthData;
            }
        }
        
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var applicationJob = new ApplierJob
            {
                damageGruop = GetComponentDataFromEntity<DamageData>(),
                healthGruop = GetComponentDataFromEntity<HealthData>(),
            };
            
            applicationJob.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorld.PhysicsWorld, inputDeps)
                .Complete();
            return default;
        }
    }
}