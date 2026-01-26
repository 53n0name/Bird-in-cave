using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class RotateSpeedAuhoring : MonoBehaviour
{

    public float value;                                             // exact same name as a component in structure

    private class Baker : Baker<RotateSpeedAuhoring>
    {
        public override void Bake(RotateSpeedAuhoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RotateSpeed
            {
                value = authoring.value
            });
        }
    }

}

public struct RotateSpeed : IComponentData
{

    public float value;



}