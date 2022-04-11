using Unity.Entities;
using UnityEngine;
using System;
using Data;

namespace Systems
{
    public class PlayerInputSystem: SystemBase
    {
        protected override void OnUpdate()
        {
            
            Entities.ForEach((ref MoveData moveData, in InputData inputData) =>
            {
                var isRightKeyPressed= Input.GetKey(inputData.rightKey);
                var isLeftKeyPressed= Input.GetKey(inputData.leftKey);
                var isUpKeyPressed= Input.GetKey(inputData.upKey);
                var isDownKeyPressed= Input.GetKey(inputData.downKey);

                moveData.direction.x = isRightKeyPressed ? 1 : isLeftKeyPressed ? -1 : 0;
                moveData.direction.y = isUpKeyPressed ? 1 : isDownKeyPressed ? -1 : 0;
            }).Run();
         
        }
    }
}