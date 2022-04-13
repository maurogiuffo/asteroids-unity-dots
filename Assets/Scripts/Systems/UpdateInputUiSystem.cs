using Data;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace Systems
{
    public class UpdateUiSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            RequireSingletonForUpdate<InputData>();
            var source = GetSingletonEntity<InputData>();
            var inputData = EntityManager.GetComponentData<InputData>(source);

            Entities.WithoutBurst()
                .ForEach((InputUiData inputUiData) =>
                {
                    Debug.Log(inputData.fireKey.ToString());
                }).Run();
            return default;
        }
    }
}