using Data;
using Unity.Entities;
using Unity.Jobs;

namespace Systems
{
    [UpdateBefore(typeof(DestroyDeadSystem))]
    public class ScoreSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            RequireSingletonForUpdate<ScoreData>();
            var source= GetSingletonEntity<ScoreData>();
            var scoreData = EntityManager.GetComponentData<ScoreData>(source);

            Entities
                .WithAll<AsteroidTag>()
                .ForEach((in HealthData healthData, in AsteroidTypeData typeData) =>
                {
                    if (!healthData.isDead) return;
                    switch (typeData.type)
                    {
                        case 0: scoreData.value += 10; break;
                        case 1: scoreData.value += 40; break;
                        case 2: scoreData.value += 100; break;
                    }
                }).Run();

            return default;
        }
    }
}