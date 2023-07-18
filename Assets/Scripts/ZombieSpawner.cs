using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;

public class ZombieSpawner : MonoBehaviour
{
    Unity.Mathematics.Random random;
    [SerializeField] ZombieJobIA zombiePrefab;
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] int numberOfZombies = 100;
    List<ZombieJobIA> zombieList;
    bool onCooldown = false;
    float cooldownTime = 2f;

    void Start()
    {
        random = new Unity.Mathematics.Random((uint)UnityEngine.Random.Range(1, 100000));
        zombieList = new List<ZombieJobIA>();
        for (int i = 0; i < numberOfZombies; i++)
        {
            Vector2 point = new Vector2(UnityEngine.Random.Range(-20f, 20f), UnityEngine.Random.Range(-20f, 20f));
            if (!Physics2D.OverlapPoint(point, LayerMask.GetMask(StringsDefinitions.COLLISION_TERRAIN)))
            {
                ZombieJobIA zombieTransform = Instantiate(zombiePrefab, point, Quaternion.identity);
                zombieList.Add(zombieTransform);
            }
            else
            {
                i--;
            }
        }
    }

    void Update()
    {
        if (zombieList.Count <= 0) { return ; }
        if (!onCooldown)
        {
            onCooldown = true;
            for (int i = 0; i < zombieList.Count; i++)
            {
                if (zombieList[i] == null || zombieList[i].gameObject == null)
                {
                    zombieList.RemoveAt(i);
                    i--;
                }
                else
                    zombieList[i]?.SetMoveDirection((Vector2)random.NextFloat2Direction());
            }
            StartCoroutine(ChangeDirectionRoutine());
        }
        else
        {
            NativeArray<float2> positionArray = new NativeArray<float2>(zombieList.Count, Allocator.TempJob);
            NativeArray<float2> directionArray = new NativeArray<float2>(zombieList.Count, Allocator.TempJob);

            for (int i = 0; i < zombieList.Count; i++)
            {
                if (zombieList[i] == null || zombieList[i].gameObject == null)
                {
                    zombieList.RemoveAt(i);
                    i--;
                    continue;
                }
                positionArray[i] = (Vector2)zombieList[i].transform.position;
                directionArray[i] = zombieList[i].MoveDirection;
            }

            MoveBitchesJob moveBitches = new MoveBitchesJob()
            {
                deltaTime = Time.deltaTime,
                moveSpeed = this.moveSpeed,
                position = positionArray,
                direction = directionArray
            };

            JobHandle jobHandle = moveBitches.Schedule(zombieList.Count, zombieList.Count / 10);

            jobHandle.Complete();

            for (int i = 0; i < zombieList.Count; i++)
            {
                if (zombieList[i] == null || zombieList[i].gameObject == null)
                {
                    zombieList.RemoveAt(i);
                    i--;
                    continue ;
                }
                if (zombieList[i].CanMove())
                    zombieList[i]?.Move(positionArray[i]);
            }

            positionArray.Dispose();
            directionArray.Dispose();
        }
    }

    IEnumerator ChangeDirectionRoutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
    }
}
