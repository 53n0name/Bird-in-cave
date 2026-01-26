using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class PlayerShootingSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<Player>();
    }

    protected override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Entity playerEntity = SystemAPI.GetSingletonEntity<Player>();
            EntityManager.SetComponentEnabled<Stunned>(playerEntity, true);

        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Entity playerEntity = SystemAPI.GetSingletonEntity<Player>();
            EntityManager.SetComponentEnabled<Stunned>(playerEntity, false);

        }


        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }

        //SpawnCubesConfig spawnCubesConfig = SystemAPI.GetSingleton<SpawnCubesConfig>();

        //EntityCommandBuffer EntityCommandBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

        //foreach (RefRO<LocalTransform> localTransform in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<Player>().WithDisabled<Stunned>())
        //{
        //    Entity spawnEntity = EntityCommandBuffer.Instantiate(spawnCubesConfig.cubePrefabEntity);
        //    EntityCommandBuffer.SetComponent(spawnEntity, new LocalTransform
        //    {
        //        Position = localTransform.ValueRO.Position,
        //        Rotation = quaternion.identity,
        //        Scale = 1f
        //    });

        //}

        //EntityCommandBuffer.Playback(EntityManager);

    }




}
