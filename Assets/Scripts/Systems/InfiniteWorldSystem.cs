using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public class InfiniteWorldSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var camera = Camera.main;
            var areaHeight = camera.orthographicSize * 2f;
            var areaWidth = areaHeight * camera.aspect;

            Entities.ForEach((ref Translation position) =>
            {
                if (position.Value.x > areaWidth * 0.5f)
                    position.Value.x += -areaWidth;
                if (position.Value.x < -areaWidth * 0.5f)
                    position.Value.x += areaWidth;

                if (position.Value.y > areaHeight * 0.5f)
                    position.Value.y += -areaHeight;
                if (position.Value.y < -areaHeight * 0.5f)
                    position.Value.y += areaHeight;
            }).Schedule();
        }
    }
}