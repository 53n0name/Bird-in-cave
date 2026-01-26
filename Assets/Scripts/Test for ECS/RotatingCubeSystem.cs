using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;

public partial struct RotatingCubeSystem : ISystem
{

    public void OnCreate(ref SystemState state) {
        state.RequireForUpdate<RotateSpeed>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state) {

        state.Enabled = false;
        return;

        //foreach ((RefRW<LocalTransform> localTransform, RefRO<RotateSpeed> rotateSpeed)
        //    in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>())
        //{

        //    float power = 1;
        //    for (int i = 0; i < 1000000; i++)
        //    {
        //        power *= 2f;
        //        power /= 2f;
        //    }

        //    localTransform.ValueRW = localTransform.ValueRO.RotateZ(rotateSpeed.ValueRO.value * SystemAPI.Time.DeltaTime * power);
        //}


        //RotatingCubeJob rotatingCubeJob = new RotatingCubeJob { deltaTime = SystemAPI.Time.DeltaTime };

        //state.Dependency = rotatingCubeJob.Schedule(state.Dependency);
        //rotatingCubeJob.ScheduleParallel();
    }

    [BurstCompile]
    //[WithAll(typeof(Player))]
    public partial struct RotatingCubeJob : IJobEntity
    {

        public float deltaTime;

        public void Execute(ref LocalTransform localTransform, in RotateSpeed rotateSpeed)
        {
            float power = 1;
            for (int i = 0; i < 1000000; i++)
            {
                power *= 2f;
                power /= 2f;
            }
            localTransform = localTransform.RotateZ(rotateSpeed.value * deltaTime * power);
        }
    }

}
