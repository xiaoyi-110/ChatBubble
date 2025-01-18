using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class LevelData_SO : ScriptableObject
{
	public List<BulletData> Sheet1;
}

[Serializable]
public class BulletData : IComparable<BulletData>
{
	public int ID;
	public string PrefabName;
	public int IsAttackable;
	public int SpawnIndex;
	public string Text;
	public float Speed;
	public float Time;

    public int CompareTo(BulletData other)
    {
        return Time.CompareTo(other.Time);
    }
}
