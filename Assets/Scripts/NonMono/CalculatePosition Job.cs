using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;

[BurstCompile]
public struct MoveBitchesJob : IJobParallelFor
{
    public NativeArray<float2> position;
    public NativeArray<float2> direction;
    public float moveSpeed;
    public float deltaTime;
    public void Execute(int index)
    {
        position[index] += direction[index] * (moveSpeed * deltaTime);
    }
}
