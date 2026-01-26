using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class RightWallAuthoring : MonoBehaviour
{
    public class Baker : Baker<RightWallAuthoring>
    {
        public override void Bake(RightWallAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new RightWallTag { });
        }
    }

    public struct RightWallTag : IComponentData { }
}