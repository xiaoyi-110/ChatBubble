
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    /// <summary>
    /// 对象池
    /// </summary>
    public Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();

    /// <summary>
    /// 预设体
    /// </summary>
    public Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();


    public GameObject GetObject(string objName, Vector3 position=default(Vector3), Quaternion rotation=default(Quaternion))
    {
        GameObject result = null;

        if(pool.ContainsKey(objName) && pool[objName].Count > 0)
        {   
            result = pool[objName].Dequeue();
        }
        else
        {
            //如果对象池中没有，则从预设体中实例化
            GameObject prefab = null;
            if(prefabs.ContainsKey(objName))
            {
                prefab = prefabs[objName];
            }
            else
            {
                prefab = Resources.Load<GameObject>(ResoucesUtility.GetPrefabAsset(objName));
                prefabs.Add(objName, prefab);
            }

            result = GameObject.Instantiate(prefab);
        }

        result.transform.position = position;
        result.transform.rotation = rotation;
        result.SetActive(true);

        result.name = objName;
        return result;
    }

    public void RecycleObject(GameObject obj)
    {
        obj.SetActive(false);
        if(pool.ContainsKey(obj.name))
        {
            pool[obj.name].Enqueue(obj);
        }
        else
        {
            pool.Add(obj.name, new Queue<GameObject>());
            pool[obj.name].Enqueue(obj);
        }
    }

}