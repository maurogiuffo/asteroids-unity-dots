﻿using Unity.Entities;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct DamageData: IComponentData
    {
        public bool damageApplied;
    }
}