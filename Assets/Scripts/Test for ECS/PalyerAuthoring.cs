using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PalyerAuthoring : MonoBehaviour
{
    public class Baker : Baker<PalyerAuthoring>
    {
        public override void Bake(PalyerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new Player());
        }
    }
}

public struct Player : IComponentData
{

}