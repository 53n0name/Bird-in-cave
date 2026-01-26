using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.UIElements;

public partial struct EnemySystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Enemy>();
    }

    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer enemyBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

        var directionOfPlayer = SystemAPI.GetSingleton<DirectionChangeSignal>();

        var em = state.EntityManager;
        Entity singletonEntity = em.CreateEntityQuery(typeof(EcsProxyConfig)).GetSingletonEntity();
        EcsProxyConfig config = em.GetComponentObject<EcsProxyConfig>(singletonEntity);
        GameObject proxyPrefab = config.ProxyPrefab;

        if (directionOfPlayer.ChangeCount < 1 || directionOfPlayer.ChangeCount > 2)
        {
            return;
        }

        Debug.Log(directionOfPlayer.ChangeCount);

        if (directionOfPlayer.ChangeCount == 2)
        {
            var query = SystemAPI.QueryBuilder()
                .WithAll<RightWallEnemyTag>()
                .Build();

            enemyBuffer.DestroyEntity(
                query,
                EntityQueryCaptureMode.AtPlayback
            );

            foreach (var e in config.RightWallCollider)
            {
                UnityEngine.Object.Destroy(e);
            }
            config.RightWallCollider.Clear();

            foreach (var enemySpawn in SystemAPI.Query<RefRO<Enemy>>().WithAll<RightWallTag>())
            {
                float3 step = (enemySpawn.ValueRO.secondPosition - enemySpawn.ValueRO.firstPosition) / (enemySpawn.ValueRO.countOfEnemies - 1);
                int EnemiesLeft = enemySpawn.ValueRO.countOfShips;

                for (int i = 0; i < enemySpawn.ValueRO.countOfEnemies; i++)
                {
                    float random = UnityEngine.Random.value;
                    // to get at least countOfShips-1 units of ships
                    if ((enemySpawn.ValueRO.countOfEnemies - i) <= EnemiesLeft - 2)
                    {
                        random = 0;
                    }

                    // to get chance of spawn 43% and check that there is something to spawn (EnemiesLeft)
                    if (random > 0.43f || EnemiesLeft <= 0)
                    {
                        continue;
                    }

                    EnemiesLeft--;

                    Entity spawnEnemyEntity = enemyBuffer.Instantiate(enemySpawn.ValueRO.enemyPrefabEntity);

                    float3 spawnPosition = enemySpawn.ValueRO.firstPosition + step * i;
                    spawnPosition.z = 1;
                    quaternion spawnRotation = quaternion.EulerXYZ(0, 0, math.radians(90));

                    // Object part
                    GameObject proxy = Object.Instantiate(proxyPrefab, spawnPosition, spawnRotation);
                    proxy.tag = "RightWallEnemy";
                    config.LeftWallCollider.Add(proxy);

                    //Entities part
                    enemyBuffer.SetComponent(spawnEnemyEntity, new LocalTransform
                    {
                        Position = spawnPosition,
                        Rotation = spawnRotation,
                        Scale = 1f
                    });

                    enemyBuffer.AddComponent<RightWallEnemyTag>(spawnEnemyEntity);
                }
            }
        }

        if (directionOfPlayer.ChangeCount == 1)
        {
            var query = SystemAPI.QueryBuilder()
                .WithAll<LeftWallEnemyTag>()
                .Build();

            enemyBuffer.DestroyEntity(
                query,
                EntityQueryCaptureMode.AtPlayback
            );

            foreach (var e in config.LeftWallCollider)
            {
                UnityEngine.Object.Destroy(e);
            }
            config.LeftWallCollider.Clear();

            foreach (var enemySpawn in SystemAPI.Query<RefRO<Enemy>>().WithAll<LeftWallTag>())
            {

                float3 step = (enemySpawn.ValueRO.secondPosition - enemySpawn.ValueRO.firstPosition) / (enemySpawn.ValueRO.countOfEnemies - 1);
                int EnemiesLeft = enemySpawn.ValueRO.countOfShips;

                for (int i = 0; i < enemySpawn.ValueRO.countOfEnemies; i++)
                {
                    float random = UnityEngine.Random.value;
                    // to get at least countOfShips-1 units of ships
                    if ((enemySpawn.ValueRO.countOfEnemies - i) <= EnemiesLeft - 2)
                    {
                        random = 0;
                    }

                    // to get chance of spawn 43% and check that there is something to spawn (EnemiesLeft)
                    if (random > 0.43f || EnemiesLeft <= 0)
                    {
                        continue;
                    }

                    EnemiesLeft--;

                    Entity spawnEnemyEntity = enemyBuffer.Instantiate(enemySpawn.ValueRO.enemyPrefabEntity);

                    float3 spawnPosition = enemySpawn.ValueRO.firstPosition + step * i;
                    spawnPosition.z = 1;
                    quaternion spawnRotation = quaternion.EulerXYZ(0, 0, math.radians(-90));

                    // Object part
                    GameObject proxy = Object.Instantiate(proxyPrefab, spawnPosition, spawnRotation);
                    proxy.tag = "LeftWallEnemy";
                    config.RightWallCollider.Add(proxy);

                    //Entity part
                    enemyBuffer.SetComponent(spawnEnemyEntity, new LocalTransform
                    {
                        Position = spawnPosition,
                        Rotation = spawnRotation,
                        Scale = 1f
                    });

                    enemyBuffer.AddComponent<LeftWallEnemyTag>(spawnEnemyEntity);
                }
            }
        }

        directionOfPlayer.ChangeCount -= 2;
        SystemAPI.SetSingleton(directionOfPlayer);

        enemyBuffer.Playback(state.EntityManager);
        enemyBuffer.Dispose();
    }
}
