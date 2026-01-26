using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class LeftWallAuthoring : MonoBehaviour
{
    public class Baker : Baker<LeftWallAuthoring>
    {
        public override void Bake(LeftWallAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new LeftWallTag {} );
        }
    }

    public struct LeftWallTag : IComponentData { }
}