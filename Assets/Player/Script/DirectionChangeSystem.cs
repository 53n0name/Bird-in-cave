using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public partial struct DirectionChangeSystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<DirectionChangeSignal>();
    }

}