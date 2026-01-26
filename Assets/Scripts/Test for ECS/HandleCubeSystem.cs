using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public partial struct HandleCubeSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach (RotatingMoveingCubeAspect rotatingMoveingCubeAspect in
            SystemAPI.Query<RotatingMoveingCubeAspect>().WithAll<RotatingCube>()) 
        {
            rotatingMoveingCubeAspect.MoveAndRotate(SystemAPI.Time.DeltaTime);
        }
    }
}
