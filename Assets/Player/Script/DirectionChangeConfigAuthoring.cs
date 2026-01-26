using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class DirectionChangeConfigAuthoring : MonoBehaviour
{

    public class Baker : Baker<DirectionChangeConfigAuthoring>
    {

        public override void Bake(DirectionChangeConfigAuthoring authoring)
        {

            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new DirectionChangeSignal
            {
                ChangeCount = 0,
            });
        }
    }
}

public struct DirectionChangeSignal : IComponentData
{
    public float ChangeCount;
}