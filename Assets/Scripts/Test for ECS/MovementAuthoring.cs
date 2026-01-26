using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class MovementAuthoring : MonoBehaviour
{
    public class Baker : Baker<MovementAuthoring> {
        public override void Bake(MovementAuthoring authoring)
        {
            Entity Entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(Entity, new Movementt
            {
                movementVector = new float3(UnityEngine.Random.Range(-1f,1f),UnityEngine.Random.Range(-1f, 1f) ,0)
            });
        }
    }
}

public  struct Movementt : IComponentData
{
    public float3 movementVector;
}