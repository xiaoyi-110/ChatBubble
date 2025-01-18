using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletPrefabData", menuName = "BulletPrefabData", order = 1)]
public class BulletPrefabData : ScriptableObject
{
    public List<GameObject> bulletPrefabs;
}