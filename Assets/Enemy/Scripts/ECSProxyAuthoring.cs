using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ECSProxyAuthoring : MonoBehaviour
{
    public GameObject proxyObject;

    public class Baker : Baker<ECSProxyAuthoring>
    {
        public override void Bake(ECSProxyAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponentObject(entity, new EcsProxyConfig
            {
                ProxyPrefab = authoring.proxyObject,
                RightWallCollider = new List<GameObject>(),
                LeftWallCollider = new List<GameObject>()
            });
        }
    }
}

public class EcsProxyConfig : IComponentData
{
    public GameObject ProxyPrefab;
    public List<GameObject> RightWallCollider;
    public List<GameObject> LeftWallCollider;
}