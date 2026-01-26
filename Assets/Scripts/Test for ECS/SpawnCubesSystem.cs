using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.VisualScripting;

public partial class SpawnCubesSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<SpawnCubesConfig>();
    }

    protected override void OnUpdate()
    {
        this.Enabled = false;

        SpawnCubesConfig spawnCubesConfig = SystemAPI.GetSingleton<SpawnCubesConfig>();

        for (int i = 0; i < spawnCubesConfig.amountToSpawn; i++)
        {
            Entity spawnEntity = EntityManager.Instantiate(spawnCubesConfig.cubePrefabEntity);
            EntityManager.SetComponentData(spawnEntity, new LocalTransform
            {
                Position = new float3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-3f, 3f), 0),
                Rotation = quaternion.identity,
                Scale = 1f
            });
            
        }
    }



}
