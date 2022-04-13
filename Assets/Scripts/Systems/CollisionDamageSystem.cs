using Data;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;

namespace Systems
{
    [UpdateBefore(typeof(SubdivideAsteroidSystem))]
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

        private struct ApplyDamageJob : ITriggerEventsJob
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
                var damageData = damageGruop[entityA];
                var healthData = healthGruop[entityB];
                if(healthData.isDead) return;
                if (damageData.damageApplied) return;
                
                healthData.isDead = true;
                healthGruop[entityB] = healthData;

                damageData.damageApplied = true;
                damageGruop[entityA] = damageData;
            }
        }
        
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var applicationJob = new ApplyDamageJob
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