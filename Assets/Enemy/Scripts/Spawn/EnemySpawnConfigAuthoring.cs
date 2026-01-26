using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawnConfigAuthoring : MonoBehaviour
{

    public GameObject EnemyPrefabObject;
    public Transform FirstPosition;
    public Transform SecondPosition;
    public int CountOfEnemies;
    public int CountOfShips;

    public class Baker : Baker<EnemySpawnConfigAuthoring>
    {
        public override void Bake(EnemySpawnConfigAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            float3 pos1 = authoring.FirstPosition != null
            ? (float3)authoring.FirstPosition.position
            : float3.zero;

            float3 pos2 = authoring.SecondPosition != null
            ? (float3)authoring.SecondPosition.position
            : float3.zero;

            // if that object has tag it gets
            if (authoring.GetComponent<RightWallAuthoring>() != null)
            {
                AddComponent<RightWallTag>(entity);
            }
            else
            {
                AddComponent<LeftWallTag>(entity);
            }

            AddComponent(entity, new Enemy
            {
                enemyPrefabEntity = GetEntity(authoring.EnemyPrefabObject, TransformUsageFlags.Dynamic),
                firstPosition = pos1,
                secondPosition = pos2,
                countOfEnemies = authoring.CountOfEnemies,
                countOfShips = authoring.CountOfShips
            });
        }
    }
}

public struct Enemy : IComponentData
{
    public Entity enemyPrefabEntity;
    public float3 firstPosition;
    public float3 secondPosition;
    public int countOfShips;
    public int countOfEnemies;
}

public struct RightWallEnemyTag : IComponentData { }
public struct LeftWallEnemyTag : IComponentData { }

public struct LeftWallTag : IComponentData { }
public struct RightWallTag : IComponentData { }