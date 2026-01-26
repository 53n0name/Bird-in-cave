using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct RotatingMoveingCubeAspect : IAspect
{
    public readonly RefRO<RotatingCube> rotatingCube;
    public readonly RefRW<LocalTransform> localTransform;
    public readonly RefRO<RotateSpeed> rotateSpeed;
    public readonly RefRO<Movementt> movementt;

    public void MoveAndRotate(float deltaTime)
    {
        localTransform.ValueRW = localTransform.ValueRO.RotateZ(rotateSpeed.ValueRO.value * deltaTime);
        localTransform.ValueRW = localTransform.ValueRO.Translate(movementt.ValueRO.movementVector * deltaTime);

    }
}
