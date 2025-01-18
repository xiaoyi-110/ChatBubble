using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public List<BulletData> bullets;
}

[Serializable]
public class BulletData : IComparable<BulletData>
{
    public int Type;
    public int SpawnIndex;
    public string Text;
    public float Speed=10f;
    public float SpawnTime=0f;

    public int CompareTo(BulletData other)
    {
        return SpawnTime.CompareTo(other.SpawnTime);
    }
}